using ChecklistManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ChecklistManager.Repository
{
    public class ChecklistDbRepository : IChecklistRepository
    {
        private ChecklistContext db = new ChecklistContext();

        public IDbSet<ChecklistTemplate> ChecklistTemplates { get { return db.ChecklistTemplates; } }

        public void SetModified(object item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        public IEnumerable<ChecklistTemplate> GetFilteredChecklistTemplates(string q, string sort, bool desc, int? limit, int offset)
        {
            var list = ((IObjectContextAdapter)db).ObjectContext.CreateObjectSet<ChecklistTemplate>();

            IQueryable<ChecklistTemplate> items = string.IsNullOrEmpty(sort) ? list.OrderBy(o => o.ChecklistTemplateId)
                : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined")
            {
                items = items.Where(t => t.Title.Contains(q));
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
    }
}