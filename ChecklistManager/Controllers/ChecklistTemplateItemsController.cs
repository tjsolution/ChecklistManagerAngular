using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ChecklistManager.Model;
using ChecklistManager.Repository;

namespace ChecklistManager.Controllers
{
    public class ChecklistTemplateItemsController : ApiController
    {
        private IChecklistRepository repository;

        public ChecklistTemplateItemsController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        // GET api/ChecklistTemplateItems?templateId
        public IEnumerable<CheckItemTemplate> GetChecklistTemplateItems(int templateId = 0)
        {
            return this.repository.CheckItemTemplates
                .Where(i => i.ChecklistTemplateId == templateId)
                .AsEnumerable();
        }


        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}