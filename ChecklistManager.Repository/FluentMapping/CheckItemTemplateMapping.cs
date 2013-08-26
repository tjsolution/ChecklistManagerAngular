using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    class CheckItemTemplateMapping : BaseMapping<CheckItemTemplate>
    {
        public CheckItemTemplateMapping()
        {
            HasKey(m => m.Id);

            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.ChecklistTemplateId)
                .IsRequired();

            HasRequired(m => m.ChecklistTemplate)
                .WithMany(m => m.Items)
                .HasForeignKey(m => m.ChecklistTemplateId);

            Property(m => m.ChecklistTemplateId)
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
