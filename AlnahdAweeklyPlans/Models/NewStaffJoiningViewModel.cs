using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class NewStaffJoiningViewModel
    {
        public NewStaffJoiningViewModel()
        {
            HrDesignationListItems = new List<SelectListItem>();
            HrDepartmentListItems = new List<SelectListItem>();
            ReligionListItems = new List<SelectListItem>();
            GenderListItems = new List<SelectListItem>();
            NewStaffJoiningList = new List<NewStaffJoining>();
            IsAirTicket = new List<SelectListItem>();
            PassportCountryList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public NewStaffJoining NewStaffJoining { get; set; }
        public List<NewStaffJoining> NewStaffJoiningList { get; set; }
        public List<SelectListItem> HrDesignationListItems { get; set; }
        public List<SelectListItem> HrDepartmentListItems { get; set; }
        public List<SelectListItem> ReligionListItems { get; set; }
        public List<SelectListItem> GenderListItems { get; set; }
        public List<SelectListItem> IsAirTicket { get; set; }
        public List<SelectListItem> PassportCountryList { get; set; }
    }
}