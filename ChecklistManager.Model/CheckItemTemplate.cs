using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class CheckItemTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public int ChecklistTemplateId { get; set; }
        public virtual ChecklistTemplate ChecklistTemplate { get; set; }
    }
}