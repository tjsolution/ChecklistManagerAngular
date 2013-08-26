using ChecklistManager.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChecklistManager.Repository.FluentMapping
{
    class OrganisationMapping : BaseMapping<Organisation>
    {
        public OrganisationMapping()
        {
            HasKey(m => m.Name);

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(150)
                .IsRequired();

            HasMany(m => m.Staff)
                .WithRequired(m => m.Organisation)
                .HasForeignKey(m => m.OrganisationName);
        }
    }
}
