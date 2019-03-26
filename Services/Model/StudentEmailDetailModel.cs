using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
   public class StudentEmailDetailModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Response { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Target { get; set; }
        public Nullable<int> MastId { get; set; }
        public string Remarks { get; set; }
        public string datestring { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
