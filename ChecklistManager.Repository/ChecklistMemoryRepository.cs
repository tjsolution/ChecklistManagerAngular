using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestLibrary;
using Extensions;
using ChecklistManager.Model;

namespace ChecklistManager.Repository
{
    public class ChecklistMemoryRepository : IChecklistRepository
    {
        private IDbSet<ChecklistTemplate> checklistTemplates = new FakeChecklistTemplateSet();
        private IDbSet<CheckItemTemplate> checkItemTemplates = new FakeCheckItemTemplateSet();

        private IDbSet<Checklist> checklists = new FakeChecklistSet();
        private IDbSet<CheckItem> checkItems = new FakeCheckItemSet();

        private IDbSet<User> users = new FakeUserSet();
        private IDbSet<Organisation> organisations = new FakeOrganisationSet();

        public IDbSet<ChecklistTemplate> ChecklistTemplates { get { return this.checklistTemplates; } }
        public IDbSet<CheckItemTemplate> CheckItemTemplates { get { return this.checkItemTemplates; } }

        public IDbSet<Checklist> Checklists { get { return this.checklists; } }
        public IDbSet<CheckItem> CheckItems { get { return this.checkItems; } }

        public IDbSet<User> Users { get { return this.users; } }
        public IDbSet<Organisation> Organisations { get { return this.organisations; } }

        public ChecklistMemoryRepository()
        {
            var list1 = new ChecklistTemplate { Id = 1, Title = "First List", ManagerUsername = "Me", Items = GetListItems("FirstList") };
            var list2 = new ChecklistTemplate { Id = 2, Title = "Second List", ManagerUsername = "Myself", Items = GetListItems("SecondList") };

            this.checklistTemplates.Add(list1);
            this.checklistTemplates.Add(list2);
        }

        private List<CheckItemTemplate> GetListItems(string listTitle)
        {
            return new[] { new CheckItemTemplate { Id = 1, Title = listTitle + "Item1" }, 
                new CheckItemTemplate { Id = 2, Title = listTitle + "Item2" } }.ToList();
        }

        public void SetModified(object item)
        {
        }

        public void SetModifiedList(object[] items)
        {
        }

        public void SaveChanges()
        {
        }

        public void Dispose()
        {
        }

        public IEnumerable<ChecklistTemplate> GetFilteredChecklistTemplates(string query, string sort, bool desc, int? limit, int offset)
        {
            var list = checklistTemplates.AsQueryable<ChecklistTemplate>();

            var items = GetSort(sort, desc, list);

            if (!string.IsNullOrEmpty(query) && query != "undefined")
            {
                items = items.Where(t => t.Title.Contains(query) || t.Manager.Name.Contains(query));
            }

            if (offset > 0)
            {
                items = items.Skip(offset);
            }

            if (limit.HasValue)
            {
                items = items.Take(limit.Value);
            }

            return items;
        }

        private static IQueryable<ChecklistTemplate> GetSort(string sort, bool desc, IQueryable<ChecklistTemplate> list)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return list.OrderBy(o => o.Id);
            }

            if (desc)
            {
                return list.OrderByDescending(sort);
            }
            return list.OrderBy(sort);
        }
    }
}