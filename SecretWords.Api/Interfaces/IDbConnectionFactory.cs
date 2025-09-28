using System.Data;

namespace SensitiveWords.Api.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
