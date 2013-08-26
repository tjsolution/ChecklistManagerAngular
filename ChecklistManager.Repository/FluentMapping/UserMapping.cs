using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    public class UserMapping : BaseMapping<User>
    {
        public UserMapping()
        {
            HasKey(m => m.Username);

            Property(m => m.OrganisationName)
                .IsRequired();

            HasRequired(m => m.Organisation)
                .WithMany(m => m.Staff)
                .HasForeignKey(m => m.OrganisationName);

            HasOptional(m => m.Manager)
                .WithMany()
                .HasForeignKey(m => m.ManagerUsername);

            Property(m => m.Username)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(m => m.ManagerUsername)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            Property(m => m.FirstName)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(m => m.LastName)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(m => m.IsAdmin)
                .IsRequired();
        }
    }
}
