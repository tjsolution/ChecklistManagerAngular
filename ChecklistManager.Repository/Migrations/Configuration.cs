namespace ChecklistManager.Repository.Migrations
{
    using ChecklistManager.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : CreateDatabaseIfNotExists<ChecklistContext>//DbMigrationsConfiguration<ChecklistContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ChecklistContext context)
        {
            base.Seed(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var twentyThreeSoftware = new Organisation { Name = "23 Software" };

            context.Organisations.AddOrUpdate(
              p => p.Name,
              twentyThreeSoftware,
               new Organisation { Name = "Black Bamboo Software" },
               new Organisation { Name = "Perfect Blue Software" }
            );
            context.Users.AddOrUpdate(
              p => p.Username,
              new User { Username = @"tajed@outlook.com", 
                  FirstName = @"Tallis", 
                  LastName = @"Jed", 
                  OrganisationName = twentyThreeSoftware.Name,
                  IsAdmin = true
              }
            );

            context.SaveChanges();
        }
    }
}
