using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Model
{
  public  class DigitalResourceMasterModel
    {
        public DigitalResourceMasterModel()
        {
            this.DigitalResourceClasses = new List<DigitalResourceClassModel>();
            this.DigitalResourceFiles = new List<DigitalResourceFileModel>();
            Selecteditems = new List<int>();
            LessonPlanClassModellist = new List<LessonPlanClassModel>();
        }
        public int Id { get; set; }
        public string title { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public int? CategoryId { get; set; }
        public int? FinalCategoryId { get; set; }
        public string Category { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Fromstring { get; set; }
        public string Tostring { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsPermanentDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public List<DigitalResourceClassModel> DigitalResourceClasses { get; set; }
        public List<DigitalResourceFileModel> DigitalResourceFiles { get; set; }
        public List<LessonPlanClassModel> LessonPlanClassModellist { get; set; }
        public List<int> Selecteditems { get; set; }
        public string AdminRemarks { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ApprovedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
