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
using System.Collections.ObjectModel;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace ChecklistManager.Controllers
{
    public class ChecklistController : ApiController
    {
        private IChecklistRepository repository;

        
        public ChecklistController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        //[Queryable]
        //public IQueryable<Checklist> GetChecklist(string organisation)
        //{
        //    return repository.Checklists
        //        .Where(i => i.ChecklistTemplate.Manager.OrganisationName == organisation)
        //        .Where(i => !i.IsObsolete)
        //        .Include(i => i.Items);
        //}

        public PageResult<Checklist> GetChecklist(string organisation, ODataQueryOptions<Checklist> queryOptions)
        {
            var query = repository.Checklists
                .Where(i => i.ChecklistTemplate.Manager.OrganisationName == organisation)
                .Where(i => !i.IsObsolete)
                .Include(i => i.Items);
             IQueryable results = queryOptions.ApplyTo(query);

             return new PageResult<Checklist>(results as IEnumerable<Checklist>, 
                 Request.GetNextPageLink(), 
                 Request.GetInlineCount());
        }

        // GET api/Checklist?templateId
        public Checklist GetNewChecklist(int templateId)
        {
            var newChecklist = CreateChecklist(templateId);
            if (newChecklist == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return newChecklist;
        }

        private Checklist CreateChecklist(int templateId)
        {
            var template = repository.ChecklistTemplates.Find(templateId);
            var checklist = template.CreateChecklist();

            var checkItems = repository.CheckItemTemplates
                 .Where(t => t.ChecklistTemplateId == templateId)
                 .ToList()
                 .Select(t => t.CreateCheckItem())
                 .ToList();
            checklist.Items = new Collection<CheckItem>(checkItems);
            return checklist;
        }

        // GET api/Checklist/5
        public Checklist GetChecklist(int id)
        {
            var checklist = this.repository.Checklists.Find(id);
            if (checklist == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return checklist;
        }

        // PUT api/Checklist/5
        public HttpResponseMessage PutChecklist(int id, Checklist checklist)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != checklist.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(checklist);
            this.repository.SetModifiedList(checklist.Items.ToArray());

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

        // POST api/Checklist
        public HttpResponseMessage PostChecklist(Checklist checklist)
        {
           if (ModelState.IsValid)
            {
                repository.Checklists.Add(checklist);
                repository.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, checklist);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = checklist.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Checklist/5
        public HttpResponseMessage DeleteChecklist(int id)
        {
            var checklist = repository.Checklists.Find(id);
            if (checklist == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.Checklists.Remove(checklist);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, checklist);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}