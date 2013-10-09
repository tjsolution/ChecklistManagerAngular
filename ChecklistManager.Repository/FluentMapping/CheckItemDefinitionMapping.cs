using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    class CheckItemDefinitionMapping : BaseMapping<CheckItemDefinition>
    {
        public CheckItemDefinitionMapping()
        {
            HasKey(m => m.Id);

            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.ChecklistDefinitionId)
                .IsRequired();

            HasRequired(m => m.ChecklistDefinition)
                .WithMany(m => m.Items)
                .HasForeignKey(m => m.ChecklistDefinitionId);

            Property(m => m.ChecklistDefinitionId)
                .IsRequired();

            Property(m => m.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(m => m.Description)
                .HasMaxLength(null)
                .HasColumnType("nvarchar");

        }
    }
}
