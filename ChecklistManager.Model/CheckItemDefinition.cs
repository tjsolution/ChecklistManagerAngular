using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChecklistManager.Model
{
    public class CheckItemDefinition : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int ChecklistDefinitionId { get; set; }
        public virtual ChecklistDefinition ChecklistDefinition { get; set; }

        public CheckItem CreateCheckItem()
        {
            return new CheckItem
                {
                    Title = Title,
                    Description = Description,
                };
        }
    }
}