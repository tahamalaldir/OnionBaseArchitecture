namespace OnionBaseArchitecture.Domain.Entities.Common
{
    public class CacheConfigs
    {
        public string Connection { get; set; }
        public bool EnableCache { get; set; }
        public int RedisConnectionPoolSize { get; set; }
        public string CacheStartKey { get; set; }
    }
}
