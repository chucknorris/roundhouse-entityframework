using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using __NAME__.Domain;
using __NAME__.Infrastructure.App;
using __NAME__.Infrastructure.App.Persistence;

namespace __NAME__.Migrations
{
    /// <summary>
    /// This is the database configuration for migrations
    /// </summary>
    public sealed class DbConfiguration : DbMigrationsConfiguration<DatabaseContext>
    {
        public DbConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            SeedDatabase(context);
        }

        public static void SeedDatabase(DatabaseContext context)
        {
           
        }

    }
}