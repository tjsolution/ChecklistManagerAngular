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
using System.Web.Http.OData.Query;
using System.Web.Http.OData;

namespace ChecklistManager.Controllers
{
    public class ChecklistDefinitionController : ApiController
    {
        private IChecklistRepository repository;

        public ChecklistDefinitionController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        // GET api/ChecklistDefinition?organisation={organisation}
        public PageResult<ChecklistDefinition> GetDefinitionsQuery(string organisation, ODataQueryOptions<ChecklistDefinition> queryOptions)
        {
            var query = repository.ChecklistDefinitions
                .Where(i => i.Manager.OrganisationName == organisation)
                .Where(i => !i.IsObsolete)
                .Include(i => i.Items);
            IQueryable results = queryOptions.ApplyTo(query);

            return new PageResult<ChecklistDefinition>(results as IEnumerable<ChecklistDefinition>,
                Request.GetNextPageLink(),
                Request.GetInlineCount());
        }

        // GET api/ChecklistDefinition/5
        public ChecklistDefinition GetChecklistDefinition(int id)
        {
            var checklistdefinition = this.repository.ChecklistDefinitions.Find(id);
            if (checklistdefinition == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return checklistdefinition;
        }

        // PUT api/ChecklistDefinition/5
        public HttpResponseMessage PutChecklistDefinition(int id, ChecklistDefinition checklistDefinition)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != checklistDefinition.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(checklistDefinition);

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

        // POST api/ChecklistDefinition
        public HttpResponseMessage PostChecklistDefinition(ChecklistDefinition checklistdefinition)
        {
           if (ModelState.IsValid)
            {
                repository.ChecklistDefinitions.Add(checklistdefinition);
                repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, checklistdefinition);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = checklistdefinition.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ChecklistDefinition/5
        public HttpResponseMessage DeleteChecklistDefinition(int id)
        {
            var checklistdefinition = repository.ChecklistDefinitions.Find(id);
            if (checklistdefinition == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.ChecklistDefinitions.Remove(checklistdefinition);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, checklistdefinition);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}