using System.Data;

namespace Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection();
    }
}