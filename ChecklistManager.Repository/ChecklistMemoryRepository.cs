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
        private IDbSet<ChecklistTemplate> checklistTemplates = new FakeChecklistSet();

        public IDbSet<ChecklistTemplate> ChecklistTemplates { get { return this.checklistTemplates; } }

        public ChecklistMemoryRepository()
        {
            var list1 = new ChecklistTemplate { ChecklistTemplateId = 1, Title = "First List", UserId = "Me", ChecklistItems = GetListItems("FirstList") };
            var list2 = new ChecklistTemplate { ChecklistTemplateId = 2, Title = "Second List", UserId = "Myself", ChecklistItems = GetListItems("SecondList") };

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
                items = items.Where(t => t.Title.Contains(query) || t.UserId.Contains(query));
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
                return list.OrderBy(o => o.ChecklistTemplateId);
            }

            if (desc)
            {
                return list.OrderByDescending(sort);
            }
            return list.OrderBy(sort);
        }
    }
}