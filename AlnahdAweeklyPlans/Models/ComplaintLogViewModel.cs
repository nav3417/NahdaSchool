using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class ComplaintLogViewModel
    {
        public ComplaintLogViewModel()
        {
            StusentListItems = new List<SelectListItem>();
        }
        public List<SelectListItem> StusentListItems { get; set; }
        public ComplainLog ComplainLog { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string ContactBy { get; set; }
        public string Date { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string CellNo { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string ActionTaken { get; set; }
        public string InformPerson { get; set; }
        public string Consulted { get; set; }
        public DateTime InformDate { get; set; }
        public string ParentsComments { get; set; }
        public string FollowUp { get; set; }
    }
}