using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TokenHandler.Export
{
    public class DynamicContext : DbContext
    {

        public DynamicContext(DbContextOptions<DynamicContext> options) : base(options)
        {
        }

        public DynamicContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
