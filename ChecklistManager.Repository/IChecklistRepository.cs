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
        IDbSet<ChecklistDefinition> ChecklistDefinitions { get; }
        IDbSet<CheckItemDefinition> CheckItemDefinitions { get; }

        IDbSet<Checklist> Checklists { get; }
        IDbSet<CheckItem> CheckItems { get; }

        IDbSet<User> Users { get; }
        IDbSet<Organisation> Organisations { get; }

        void SetModified(object item);
        void SetModifiedList(object[] items);
        void SaveChanges();

        IEnumerable<ChecklistDefinition> GetFilteredChecklistDefinitions(string q, string sort, bool desc, int? limit, int offset);
    }
}
