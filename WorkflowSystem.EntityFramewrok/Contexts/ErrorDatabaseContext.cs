using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowSystem.EntityFramework
{
    public class ErrorDatabaseContext : DbContext
    {
     
        public ErrorDatabaseContext()
            : base()
        {
        }
        public ErrorDatabaseContext(DbContextOptions<ErrorDatabaseContext> options)
         : base(options)
        {

        }
        public static DbContext Create()
        {
            return (new ErrorDatabaseContextFactory()).CreateDbContext(new string[0]);
        }

        //Add-Migration -Context ErrorDatabaseContext -o ErrorDatabaseMigrations <Nazwa migracji>
        //Update-Database -Context ErrorDatabaseContext
        //Remove-Migration -Context ErrorDatabaseContext

        public DbSet<LogEntry> LogEntries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
