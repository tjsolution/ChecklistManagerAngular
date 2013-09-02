using ChecklistManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace ChecklistManager.Repository
{
    public class ChecklistDbRepository : IChecklistRepository
    {
        private ChecklistContext db = new ChecklistContext();

        public IDbSet<ChecklistTemplate> ChecklistTemplates { get { return db.ChecklistTemplates; } }
        public IDbSet<CheckItemTemplate> CheckItemTemplates { get { return db.CheckItemTemplates; } }
        public IDbSet<Checklist> Checklists { get { return db.Checklists; } }
        public IDbSet<CheckItem> CheckItems { get { return db.CheckItems; } }
        public IDbSet<User> Users { get { return db.Users; } }
        public IDbSet<Organisation> Organisations { get { return db.Organisations; } }

        public void SetModified(object item)
        {
            db.Entry(item).State = EntityState.Modified;
        }        
        
        public void SetModifiedList(object[] items)
        {
            if (items == null)
            {
                return;
            }
            Array.ForEach(items, item => db.Entry(item).State = EntityState.Modified);
        }

        public void SaveChanges()
        {
            SaveDbChanges();
        }

        private bool SaveDbChanges()
        {
            try
            {
                int count = this.db.SaveChanges();
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (DbEntityValidationException databaseEx)
            {
                var validationErrorString = RepositoryHelper.GetValidationErrorMessageForEntity(databaseEx);
                throw RepositoryException.Create(validationErrorString.ToString());
            }
            catch (DbUpdateConcurrencyException ocex)
            {
                var objContext = ((IObjectContextAdapter)this.db).ObjectContext;
                foreach (var failedEntry in ocex.Entries)
                {
                    objContext.Refresh(RefreshMode.ClientWins, failedEntry.Entity);
                }
                return objContext.SaveChanges() > 0;
            }
            catch (DbUpdateException upEx)
            {
                var exception = upEx.GetBaseException();
                throw RepositoryException.Create(exception.Message, upEx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

            IQueryable<ChecklistTemplate> items = string.IsNullOrEmpty(sort) ? list.OrderBy(o => o.Id)
                : list.OrderBy(string.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

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