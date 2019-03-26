using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlnahdAweeklyPlans.Models
{
    public class ResponceModel
    {
        public int Id { get; set; }
        public int Inboxcount { get; set; }
        public int Draftcount { get; set; }
        public int Sentcount { get; set; }
    }
}