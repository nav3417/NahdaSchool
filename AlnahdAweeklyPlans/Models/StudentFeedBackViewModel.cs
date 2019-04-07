using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class StudentFeedBackViewModel
    {
        public StudentFeedBackViewModel()
        {
            SubjectsSelectList = new List<SelectListItem>();
            StudentSelectList = new List<SelectListItem>();
        }
        public StudentFeedBackReport StudentFeedBackReport { get; set; }
        public List<SelectListItem> SubjectsSelectList { get; set; }
        public List<SelectListItem> StudentSelectList { get; set; }
        public int Id { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string StudentCode { get; set; }
        public string Date { get; set; }
        public string   Subject { get; set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
    }
}