using System.Data;

namespace OnionBaseArchitecture.Application.Abstractions
{
    public interface IConnectionManager
    {
        IDbConnection Connection { get; }
    }
}
