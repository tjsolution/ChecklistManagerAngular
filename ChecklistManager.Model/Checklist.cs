using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class Checklist : BaseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<CheckItem> Items { get; set; }

        public int ChecklistTemplateId { get; set; }
        public virtual ChecklistTemplate ChecklistTemplate { get; set; }
    }
}