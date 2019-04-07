using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class CleaningStaffByLocationViewModel
    {
      public  CleaningStaffByLocationViewModel()
        {
            StaffTypeListItems = new List<SelectListItem>();
            StaffListItems = new List<SelectListItem>();
        }
        public CleaningStaffByLocation CleaningStaffByLocation { get; set; }
        public List<SelectListItem> StaffTypeListItems { get; set; }
        public List<SelectListItem> StaffListItems { get; set; }
        public int Id { get; set; }
        public string Staff { get; set; }

        public string Location { get; set; }
        public string StaffType { get; set; }
    }
}