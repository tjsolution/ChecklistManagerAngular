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
        private IDbSet<ChecklistDefinition> checklistDefinitions = new FakeChecklistDefinitionSet();
        private IDbSet<CheckItemDefinition> checkItemDefinitions = new FakeCheckItemDefinitionSet();

        private IDbSet<Checklist> checklists = new FakeChecklistSet();
        private IDbSet<CheckItem> checkItems = new FakeCheckItemSet();

        private IDbSet<User> users = new FakeUserSet();
        private IDbSet<Organisation> organisations = new FakeOrganisationSet();

        public IDbSet<ChecklistDefinition> ChecklistDefinitions { get { return this.checklistDefinitions; } }
        public IDbSet<CheckItemDefinition> CheckItemDefinitions { get { return this.checkItemDefinitions; } }

        public IDbSet<Checklist> Checklists { get { return this.checklists; } }
        public IDbSet<CheckItem> CheckItems { get { return this.checkItems; } }

        public IDbSet<User> Users { get { return this.users; } }
        public IDbSet<Organisation> Organisations { get { return this.organisations; } }

        public ChecklistMemoryRepository()
        {
            var list1 = new ChecklistDefinition { Id = 1, Title = "First List", ManagerUsername = "Me", Items = GetListItems("FirstList") };
            var list2 = new ChecklistDefinition { Id = 2, Title = "Second List", ManagerUsername = "Myself", Items = GetListItems("SecondList") };

            this.checklistDefinitions.Add(list1);
            this.checklistDefinitions.Add(list2);
        }

        private List<CheckItemDefinition> GetListItems(string listTitle)
        {
            return new[] { new CheckItemDefinition { Id = 1, Title = listTitle + "Item1" }, 
                new CheckItemDefinition { Id = 2, Title = listTitle + "Item2" } }.ToList();
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

        public IEnumerable<ChecklistDefinition> GetFilteredChecklistDefinitions(string query, string sort, bool desc, int? limit, int offset)
        {
            var list = checklistDefinitions.AsQueryable<ChecklistDefinition>();

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

        private static IQueryable<ChecklistDefinition> GetSort(string sort, bool desc, IQueryable<ChecklistDefinition> list)
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