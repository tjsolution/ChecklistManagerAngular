using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class ChecklistDefinition : BaseEntity
    {
        public int Id { get; set; }

        public string ManagerUsername { get; set; }
        public User Manager { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CheckItemDefinition> Items { get; set; }

        public Checklist CreateChecklist()
        {
            return new Checklist
            {
                ChecklistDefinitionId = Id,
                Title = Title
            };
        }
    }
}