using ChecklistManager.Model;
using System.Data.Entity.ModelConfiguration;

namespace ChecklistManager.Repository.FluentMapping
{
    public class BaseMapping<T> : EntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public BaseMapping()
        {
            Property(m => m.RecordCreated)
                .HasColumnType("datetime2");

            Property(m => m.RecordModified)
                .HasColumnType("datetime2");

            Property(m => m.RecordModifiedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
        }
    }
}
