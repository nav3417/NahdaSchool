using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
 public   class LessonsPlanMasterModel
    {
        public LessonsPlanMasterModel()
        {
            LessonPlanModelList = new List<LessonPlanModel>();
            LessonPlanDetailList = new List<LessonPlanDetailModel>();
        }
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Day { get; set; }
        public string Class { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModefiedOn { get; set; }
        public string ModefiedBy { get; set; }
        public Nullable<System.DateTime> LessonPlanFrom { get; set; }
        public Nullable<System.DateTime> LessonPlanTo { get; set; }
        public string HOSRemarks { get; set; }
        public string ApprovedBy { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
        public string Datefor { get; set; }
        public string TeacherRemarks { get; set; }
        public string Teacher { get; set; }
        public List<LessonPlanModel> LessonPlanModelList { get; set; }
        public virtual List<LessonPlanDetailModel> LessonPlanDetailList { get; set; }
    }
}
