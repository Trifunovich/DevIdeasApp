using System;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Sql.Helpers;

namespace DataAccess.Sql.Context
{
  internal class SqlConnectionFactory : ISqlConnectionFactory
  {
    public IDbConnection CreateSqlDbConnection()
    {
      // Connection string setting
      var connectionStringValue = ConnectionHelper.SqlConnectionString;

      // Assume failure.
      IDbConnection connection;

      // Null connection string cannot be accepted
      if (connectionStringValue == null) return null;

      // Create the DbProviderFactory and DbConnection.
      try
      {
        // Create Connection
        connection = new SqlConnection(connectionStringValue) {ConnectionString = connectionStringValue};
      }
      catch (Exception ex)
      {
        connection = null;
      }

      // Return the connection.
      return connection;
    }
  }
}

