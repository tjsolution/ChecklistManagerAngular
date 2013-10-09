using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    class ChecklistMapping : BaseMapping<Checklist>
    {
        public ChecklistMapping()
        {
            HasKey(m => m.Id);

            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(m => m.ChecklistDefinitionId)
                .IsRequired();

            HasRequired(m => m.ChecklistDefinition)
                .WithMany()
                .HasForeignKey(m => m.ChecklistDefinitionId);

            HasMany(m => m.Items)
                .WithRequired(m => m.Checklist)
                .HasForeignKey(m => m.ChecklistId);

            Property(m => m.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(m => m.Notes)
                .HasColumnType("nvarchar")
                .HasMaxLength(null);
        }
    }
}
