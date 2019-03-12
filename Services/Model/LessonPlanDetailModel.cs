using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
   public class LessonPlanDetailModel
    {
        public int Id { get; set; }
        public Nullable<int> MasterId { get; set; }
        public string Reference { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Category { get; set; }
        public string FileExtnsion { get; set; }
        public string ShowFileName { get; set; }
        public string URL { get; set; }
        public int CategoryId { get; set; }
        public string Sunday { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string SundayComment { get; set; }
        public string MondayComment { get; set; }
        public string TuesdayComent { get; set; }
        public string WednesdayComment { get; set; }
        public string ThursdayComment { get; set; }
        public string Teacher { get; set; }
        public string TeacherRemarks { get; set; }
        public string Day { get; set; }
        public string Clas { get; set; }
        public string Subject { get; set; }
        public string HOSRemarks { get; set; }
        public string Datefor { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public virtual LessonsPlanMasterModel LessonsPlanMaster { get; set; }
    }
}
