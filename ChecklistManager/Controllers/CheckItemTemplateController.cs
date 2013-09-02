﻿using System;
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

        // GET api/CheckItemTemplate?templateId
        public IEnumerable<CheckItemTemplate> GetChecklistTemplateItems(int templateId)
        {
            return this.repository.CheckItemTemplates
                .Where(i => !i.IsObsolete)
                .Where(i => i.ChecklistTemplateId == templateId);
        }

        // GET api/CheckItemTemplate/5
        public CheckItemTemplate GetCheckItemTemplate(int id)
        {
            var itemTemplate = this.repository.CheckItemTemplates.Find(id);
            if (itemTemplate == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return itemTemplate;
        }

        // PUT api/CheckItemTemplate/5
        public HttpResponseMessage PutCheckItemTemplate(int id, CheckItemTemplate itemTemplate)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != itemTemplate.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(itemTemplate);

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

        // POST api/CheckItemTemplate
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

        // DELETE api/CheckItemTemplate/5
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