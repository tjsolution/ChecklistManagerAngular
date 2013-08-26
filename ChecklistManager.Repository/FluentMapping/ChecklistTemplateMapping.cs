using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    class ChecklistTemplateMapping : BaseMapping<ChecklistTemplate>
    {
        public ChecklistTemplateMapping()
        {
            HasKey(m => m.Id);

            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            HasMany(m => m.Items)
                .WithRequired(m => m.ChecklistTemplate)
                .HasForeignKey(m => m.ChecklistTemplateId);

            HasRequired(m => m.Manager)
                .WithMany()
                .HasForeignKey(m => m.ManagerUsername);

            Property(m => m.ManagerUsername)
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .IsRequired();

            Property(m => m.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
