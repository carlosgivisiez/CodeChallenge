using System;
using System.Reflection;
using Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.Configuration;
using SqlMigrationAssembly = CodeChallenge.Auth.Admin.EntityFramework.SqlServer.Helpers.MigrationAssembly;
using MySqlMigrationAssembly = CodeChallenge.Auth.Admin.EntityFramework.MySql.Helpers.MigrationAssembly;
using PostgreSQLMigrationAssembly = CodeChallenge.Auth.Admin.EntityFramework.PostgreSQL.Helpers.MigrationAssembly;

namespace CodeChallenge.Auth.Admin.Configuration.Database
{
    public static class MigrationAssemblyConfiguration
    {
        public static string GetMigrationAssemblyByProvider(DatabaseProviderConfiguration databaseProvider)
        {
            return databaseProvider.ProviderType switch
            {
                DatabaseProviderType.SqlServer => typeof(SqlMigrationAssembly).GetTypeInfo().Assembly.GetName().Name,
                DatabaseProviderType.PostgreSQL => typeof(PostgreSQLMigrationAssembly).GetTypeInfo()
                    .Assembly.GetName()
                    .Name,
                DatabaseProviderType.MySql => typeof(MySqlMigrationAssembly).GetTypeInfo().Assembly.GetName().Name,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}







