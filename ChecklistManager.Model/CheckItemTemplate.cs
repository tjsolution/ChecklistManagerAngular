using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class CheckItemTemplate : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int ChecklistTemplateId { get; set; }
        public virtual ChecklistTemplate ChecklistTemplate { get; set; }
    }
}