using ChecklistManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistManager.Repository
{
    public interface IChecklistRepository : IDisposable
    {
        IDbSet<ChecklistTemplate> ChecklistTemplates { get; }
        IDbSet<CheckItemTemplate> CheckItemTemplates { get; }

        IDbSet<Checklist> Checklists { get; }
        IDbSet<CheckItem> CheckItems { get; }

        IDbSet<User> Users { get; }
        IDbSet<Organisation> Organisations { get; }

        void SetModified(object item);
        void SetModifiedList(object[] items);
        void SaveChanges();

        IEnumerable<ChecklistTemplate> GetFilteredChecklistTemplates(string q, string sort, bool desc, int? limit, int offset);
    }
}
