using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }

        public string Name { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string ManagerUsername { get; set; }
        public virtual User Manager { get; set; }

        public string OrganisationName { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}