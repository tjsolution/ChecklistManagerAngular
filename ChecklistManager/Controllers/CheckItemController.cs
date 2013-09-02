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
    public class CheckItemController : ApiController
    {
        private IChecklistRepository repository;

        public CheckItemController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<CheckItem> GetChecklist(int checklistId)
        {
            return repository.CheckItems
                .Where(i => i.ChecklistId == checklistId)
                .Where(i => !i.IsObsolete);
        }

        // GET api/CheckItem/5
        public CheckItem GetCheckItem(int id)
        {
            var item = this.repository.CheckItems.Find(id);
            if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
        }

        // PUT api/CheckItem/5
        public HttpResponseMessage PutCheckItem(int id, CheckItem item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != item.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(item);

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

        // POST api/CheckItem
        public HttpResponseMessage PostCheckItem(CheckItem item)
        {
           if (ModelState.IsValid)
            {
                repository.CheckItems.Add(item);
                repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CheckItem/5
        public HttpResponseMessage DeleteCheckItem(int id)
        {
            var item = repository.CheckItems.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.CheckItems.Remove(item);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}