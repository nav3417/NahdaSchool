using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class Maintenance_IssuesDetailModel
    {
        public Maintenance_IssuesDetailModel()
        {
          
        }
        public int Id { get; set; }
        public Nullable<int> InsideRoomLocation { get; set; }

        public string Description { get; set; }
        public string Equipment { get; set; }
        public Nullable<int> MasterId { get; set; }
    }
}