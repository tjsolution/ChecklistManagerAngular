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
    public class CheckItemDefinitionController : ApiController
    {
        private IChecklistRepository repository;

        public CheckItemDefinitionController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        // GET api/CheckItemDefinition?definitionId
        public IEnumerable<CheckItemDefinition> GetChecklistDefinitionItems(int definitionId)
        {
            return this.repository.CheckItemDefinitions
                .Where(i => !i.IsObsolete)
                .Where(i => i.ChecklistDefinitionId == definitionId);
        }

        // GET api/CheckItemDefinition/5
        public CheckItemDefinition GetCheckItemDefinition(int id)
        {
            var itemDefinition = this.repository.CheckItemDefinitions.Find(id);
            if (itemDefinition == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return itemDefinition;
        }

        // PUT api/CheckItemDefinition/5
        public HttpResponseMessage PutCheckItemDefinition(int id, CheckItemDefinition itemDefinition)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != itemDefinition.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(itemDefinition);

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

        // POST api/CheckItemDefinition
        public HttpResponseMessage PostCheckItemDefinition(CheckItemDefinition itemDefinition)
        {
           if (ModelState.IsValid)
            {
                repository.CheckItemDefinitions.Add(itemDefinition);
                repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, itemDefinition);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = itemDefinition.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CheckItemDefinition/5
        public HttpResponseMessage DeleteCheckItemDefinition(int id)
        {
            var itemDefinition = repository.CheckItemDefinitions.Find(id);
            if (itemDefinition == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.CheckItemDefinitions.Remove(itemDefinition);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, itemDefinition);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}