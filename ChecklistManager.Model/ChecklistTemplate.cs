using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class ChecklistTemplate
    {
        [Key]
        public int ChecklistTemplateId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public virtual List<CheckItemTemplate> ChecklistItems { get; set; }
    }
}