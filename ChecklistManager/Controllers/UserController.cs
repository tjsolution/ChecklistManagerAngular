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
    public class UserController : ApiController
    {
        private IChecklistRepository repository;

        public UserController(IChecklistRepository repository)
        {
            this.repository = repository;
        }

        // GET api/User/organisation
        public IEnumerable<User> GetUsers(string organisation)
        {
            return this.repository.Users
                .Where(u => u.OrganisationName == organisation)
                .Include(u => u.Manager)
                .AsEnumerable();
        }

        // GET api/User/5
        public User GetUser(string username)
        {
            User User = this.repository.Users.Find(username);
            if (User == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return User;
        }

        // PUT api/User/username
        public HttpResponseMessage PutUser(string username, User user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (username != user.Username)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.repository.SetModified(User);

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

        // POST api/User
        public HttpResponseMessage PostUser(User user)
        {
           if (ModelState.IsValid)
            {
                repository.Users.Add(user);
                repository.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { username = user.Username }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/User/username
        public HttpResponseMessage DeleteUser(string username)
        {
            User User = repository.Users.Find(username);
            if (User == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            repository.Users.Remove(User);

            try
            {
                repository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, User);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}