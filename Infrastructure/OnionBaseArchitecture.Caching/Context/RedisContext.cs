using OnionBaseArchitecture.Application.Common;
using StackExchange.Redis;

namespace OnionBaseArchitecture.Caching.Context
{
    public class RedisContext
    {
        private readonly Object lockPookRoundRobin = new Object();
        private Lazy<CachingFramework.Redis.RedisContext>[] lazyConnection = null;
        public CacheConfigs _cacheConfigs;

        public RedisContext(CacheConfigs cacheConfigs)
        {
            _cacheConfigs = cacheConfigs;
            if (_cacheConfigs.EnableCache)
                InitConnectionPool();
        }

        public IConnectionMultiplexer Connection
        {
            get
            {
                lock (lockPookRoundRobin)
                {
                    return GetLeastLoadedConnection();
                }
            }
        }

        public string GetConnection()
        {
            return _cacheConfigs.Connection;
        }

        private void InitConnectionPool()
        {
            lock (lockPookRoundRobin)
            {
                if (lazyConnection == null)
                {
                    lazyConnection = new Lazy<CachingFramework.Redis.RedisContext>[_cacheConfigs.RedisConnectionPoolSize];
                }
                for (int i = 0; i < _cacheConfigs.RedisConnectionPoolSize; i++)
                {
                    var config = ConfigurationOptions.Parse(GetConnection());
                    config.CommandMap = CommandMap.Create(new HashSet<string> { "SUBSCRIBE" }, false);
                    lazyConnection[i] = new Lazy<CachingFramework.Redis.RedisContext>(() => new CachingFramework.Redis.RedisContext(config, new CachingFramework.Redis.Serializers.JsonSerializer()), true);
                }
            }
        }

        private IConnectionMultiplexer GetLeastLoadedConnection()
        {
            Lazy<CachingFramework.Redis.RedisContext> lazyContext;
            var loadedLazys = lazyConnection.Where((lazy) => lazy.IsValueCreated);
            if (loadedLazys.Count() == lazyConnection.Count())
            {
                var minValue = loadedLazys.Min((lazy) => lazy.Value.GetConnectionMultiplexer().GetCounters().TotalOutstanding);
                lazyContext = loadedLazys.Where((lazy) => lazy.Value.GetConnectionMultiplexer().GetCounters().TotalOutstanding == minValue).First();
            }
            else
            {
                lazyContext = lazyConnection[loadedLazys.Count()];
            }
            return lazyContext.Value.GetConnectionMultiplexer();
        }
    }
}
