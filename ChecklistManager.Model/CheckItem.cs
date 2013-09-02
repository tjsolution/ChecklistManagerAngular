using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class CheckItem : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public bool IsDone { get; set; }

        public int ChecklistId { get; set; }
        public virtual Checklist Checklist { get; set; }
    }
}