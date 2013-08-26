using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<User> Staff { get; set; }
    }
}