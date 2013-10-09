using ChecklistManager.Model;
using ChecklistManager.Repository.FluentMapping;
using ChecklistManager.Repository.Migrations;
using System.Data.Entity;
using TestLibrary;

namespace ChecklistManager.Repository
{
    public class ChecklistContext : DbContext
    {
        static ChecklistContext()
        {
            Database.SetInitializer(new Configuration());
        }   

        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<ChecklistManager.Models.ChecklistContext>());

        public ChecklistContext() : base("name=ChecklistContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<ChecklistDefinition> ChecklistDefinitions { get; set; }
        public IDbSet<CheckItemDefinition> CheckItemDefinitions { get; set; }

        public IDbSet<Checklist> Checklists { get; set; }
        public IDbSet<CheckItem> CheckItems { get; set; }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Organisation> Organisations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ChecklistDefinitionMapping());
            modelBuilder.Configurations.Add(new CheckItemDefinitionMapping());

            modelBuilder.Configurations.Add(new ChecklistMapping());
            modelBuilder.Configurations.Add(new CheckItemMapping());

            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new OrganisationMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
