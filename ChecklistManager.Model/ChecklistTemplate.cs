using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class ChecklistTemplate : BaseEntity
    {
        public int Id { get; set; }

        public string ManagerUsername { get; set; }
        public User Manager { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CheckItemTemplate> Items { get; set; }
    }
}