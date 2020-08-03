namespace MVIOperationsSystem.Migrations.LocalStorage
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class LsConfiguration : DbMigrationsConfiguration<MVIOperationsSystem.Data.LocalStorageContext>
    {
        public LsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\LocalStorage";
        }

        protected override void Seed(MVIOperationsSystem.Data.LocalStorageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
