using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkflowSystem.EntityFramework
{
    public class ErrorDatabaseContextFactory : IDesignTimeDbContextFactory<ErrorDatabaseContext>
    {
        private static DbContextOptions<ErrorDatabaseContext> _options = null;
        public ErrorDatabaseContextFactory()
        {
            // A parameter-less constructor is required by the EF Core CLI tools.
            if (_options == null)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

                var builder = new DbContextOptionsBuilder<ErrorDatabaseContext>();
                var connectionString = configuration.GetConnectionString("ErrorDatabaseContext");

                builder.UseSqlServer(connectionString);
                builder.UseLazyLoadingProxies();
                _options = builder.Options;
            }
        }

        public ErrorDatabaseContext CreateDbContext(string[] args)
        {
            return new ErrorDatabaseContext(_options);
        }

    }
}
