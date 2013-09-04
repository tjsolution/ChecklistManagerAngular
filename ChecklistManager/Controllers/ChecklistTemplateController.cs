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
    public class ChecklistTemplateController : ApiController
    {
        private IChecklistRepository repository;

        public ChecklistTemplateController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        // GET api/ChecklistTemplate?organisation={organisation}
        public PageResult<ChecklistTemplate> GetTemplatesQuery(string organisation, ODataQueryOptions<ChecklistTemplate> queryOptions)
        {
            var query = repository.ChecklistTemplates
                .Where(i => i.Manager.OrganisationName == organisation)
                .Where(i => !i.IsObsolete)
                .Include(i => i.Items);
            IQueryable results = queryOptions.ApplyTo(query);

            return new PageResult<ChecklistTemplate>(results as IEnumerable<ChecklistTemplate>,
                Request.GetNextPageLink(),
                Request.GetInlineCount());
        }

        // GET api/ChecklistTemplate/5
        public ChecklistTemplate GetChecklistTemplate(int id)
        {
            var checklisttemplate = this.repository.ChecklistTemplates.Find(id);
            if (checklisttemplate == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return checklisttemplate;
        }

        // PUT api/ChecklistTemplate/5
        public HttpResponseMessage PutChecklistTemplate(int id, ChecklistTemplate checklistTemplate)
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
        public HttpResponseMessage PostChecklistTemplate(ChecklistTemplate checklisttemplate)
        {
           if (ModelState.IsValid)
            {
                repository.ChecklistTemplates.Add(checklisttemplate);
                repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, checklisttemplate);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = checklisttemplate.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ChecklistTemplate/5
        public HttpResponseMessage DeleteChecklistTemplate(int id)
        {
            var checklisttemplate = repository.ChecklistTemplates.Find(id);
            if (checklisttemplate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.ChecklistTemplates.Remove(checklisttemplate);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, checklisttemplate);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}