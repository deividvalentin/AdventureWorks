using System;
using System.Data;
using Data.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DbConfiguration _dbConfigurations;

        public DbConnectionFactory(IOptionsSnapshot<DbConfiguration> dbConfigurations)
        {
            _dbConfigurations = dbConfigurations.Value;
        }

        public IDbConnection CreateDbConnection()
        {
             if (string.IsNullOrEmpty(_dbConfigurations.ConnectionString))
               throw new ArgumentException($"Connection string is  null or empty!");

            var connection = GetDbProviderFactory(_dbConfigurations.ProviderType);;
           
             if (connection == null)
                    throw new ArgumentNullException($"Could not get DBConnection");

            // Return the connection.
            return GetDbProviderFactory(_dbConfigurations.ProviderType);
        }
        
        //Factory method for creating new connection
        private IDbConnection GetDbProviderFactory(string providerType) =>
            providerType.ToLower() switch
            {
                "system.data.sqlclient" => new SqlConnection(_dbConfigurations.ConnectionString),                
                _ => throw new NotSupportedException($"Unsupported Provider Factory: {providerType}")
            };
    }
}