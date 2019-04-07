using Database;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public   class Mapping
    {
        Nahda_AttendanceEntities db;
      
        public static List<LeaveRequestFormModel> ToList(IEnumerable<LeaveRequestForm> LessonsPlans)
        {
            List<LeaveRequestFormModel> list = new List<LeaveRequestFormModel>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public static LeaveRequestFormModel ToObj(LeaveRequestForm objdb)
        {
                LeaveRequestFormModel obj = new LeaveRequestFormModel()
                {
                   AdminRemarks=objdb.AdminRemarks,
                   CellNo= objdb.CellNo,
                   Class= objdb.Class,
                   Date=Convert.ToDateTime(objdb.Date).ToShortDateString(),
                   From= Convert.ToDateTime(objdb.From).ToShortDateString(),
                   To= Convert.ToDateTime(objdb.To).ToShortDateString(),
                   Id= objdb.Id,
                   ParentApproval= objdb.ParentApproval==true?"Yes" : "No",
                   Reason= objdb.Reason,
                   StudentCode= objdb.StudentCode,
                   StudentName= objdb.StudentCode,
                   Term= objdb.Term,
                   StudentId=(int)objdb.StudentId
                };
                return obj;
        }
    }
}
