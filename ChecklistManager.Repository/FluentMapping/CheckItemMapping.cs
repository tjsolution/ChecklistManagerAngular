using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    class CheckItemMapping : BaseMapping<CheckItem>
    {
        public CheckItemMapping()
        {
            HasKey(m => m.Id);

            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(m => m.Checklist)
                .WithMany(m => m.Items)
                .HasForeignKey(m => m.ChecklistId);

            Property(m => m.IsDone)
                .IsRequired();

            Property(m => m.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(m => m.Description)
                .HasMaxLength(null)
                .HasColumnType("nvarchar");

            Property(m => m.Notes)
                .HasColumnType("nvarchar")
                .HasMaxLength(null);
        }
    }
}
