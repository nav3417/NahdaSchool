using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlnahdAweeklyPlans.Models
{
    public class LeaveRequestFormModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Class { get; set; }
        public string StudentCode { get; set; }
        public string CellNo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Reason { get; set; }
        public string ParentApproval { get; set; }
        public string AdminRemarks { get; set; }
        public string Date { get; set; }
        public string Term { get; set; }
    }
}