using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class MultiSelect
    {
        public MultiSelect()
        {
            List = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public List<int> Ids { get; set; }
        public List<SelectListItem>List { get; set; }
    }
}