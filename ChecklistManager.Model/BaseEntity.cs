using System;

namespace ChecklistManager.Model
{
    public class BaseEntity
    {
        public DateTime RecordCreated { get; set; }
        public DateTime RecordModified { get; set; }
        public string RecordModifiedBy { get; set; }

        public bool IsObsolete { get; set; }

        public BaseEntity()
        {
            Modified();
            UnObsolete();
        }

        public void Modified()
        {
            RecordModified = DateTime.Now;
        }

        public void Obsolete()
        {
            IsObsolete = true;
        }

        public void UnObsolete()
        {
            IsObsolete = false;
        }

        public void SetCreated()
        {
            RecordCreated = DateTime.Now;
            RecordModified = DateTime.Now;
        }
    }
}
