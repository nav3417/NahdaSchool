using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
  public  class DigitalResourceClassModel
    {
        public int Id { get; set; }
        public Nullable<int> MasterId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual DigitalResourceMasterModel DigitalResourceMaster { get; set; }
        public virtual LessonPlanClassModel LessonPlanClass { get; set; }
    }
}
