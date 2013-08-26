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
    public class CheckItemTemplateController : ApiController
    {
        private IChecklistRepository repository;

        public CheckItemTemplateController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        // GET api/ChecklistTemplate/5
        public CheckItemTemplate GetCheckItemTemplate(int id)
        {
            var itemTemplate = this.repository.CheckItemTemplates.Find(id);
            if (itemTemplate == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return itemTemplate;
        }

        // PUT api/ChecklistTemplate/5
        public HttpResponseMessage PutCheckItemTemplate(int id, CheckItemTemplate checklistTemplate)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != checklistTemplate.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(checklistTemplate);

            try
            {
                this.repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/ChecklistTemplate
        public HttpResponseMessage PostCheckItemTemplate(CheckItemTemplate itemTemplate)
        {
           if (ModelState.IsValid)
            {
                repository.CheckItemTemplates.Add(itemTemplate);
                repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, itemTemplate);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = itemTemplate.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ChecklistTemplate/5
        public HttpResponseMessage DeleteCheckItemTemplate(int id)
        {
            var itemTemplate = repository.CheckItemTemplates.Find(id);
            if (itemTemplate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.CheckItemTemplates.Remove(itemTemplate);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, itemTemplate);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}