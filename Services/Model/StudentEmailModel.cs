using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
  public  class StudentEmailModel
    {
        public StudentEmailModel()
        {
            Details = new List<StudentEmailDetailModel>();
            Files = new List<StudentEmailFileModel>();
                
        }
        public int Id { get; set; }
        public string MailContent { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Subject { get; set; }
        public string School { get; set; }
        public string AcademicYear { get; set; }
        public Nullable<bool> IsTemplate { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsFile { get; set; }
        public Nullable<int> NoFiles { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public virtual List<StudentEmailDetailModel> Details { get; set; }
        public virtual List<StudentEmailFileModel> Files { get; set; }
    }
}
