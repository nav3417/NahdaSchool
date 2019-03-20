using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
   public class DigitalResourceFileModel
    {
        public int Id { get; set; }
        public Nullable<int> MasterId { get; set; }
        public string FileExtnsion { get; set; }
        public string ShowFileName { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }

        public virtual DigitalResourceMasterModel DigitalResourceMaster { get; set; }
    }
}
