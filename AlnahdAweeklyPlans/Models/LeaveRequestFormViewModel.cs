using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class LeaveRequestFormViewModel
    {

        public LeaveRequestFormViewModel()
        {
            StudentSelectListItems = new List<SelectListItem>();
            ClassSelectListItems = new List<SelectListItem>();
            ParentApprovalSelectListItems = new List<SelectListItem>();

        }
      public  List<SelectListItem> StudentSelectListItems { get; set; }
      public  List<SelectListItem> ClassSelectListItems { get; set; }
      public  List<SelectListItem> ParentApprovalSelectListItems { get; set; }
        public int Id { get; set; }
        public LeaveRequestForm LeaveRequestForm { get; set; }
        public string Parentapproval { get; set; }

    }
}