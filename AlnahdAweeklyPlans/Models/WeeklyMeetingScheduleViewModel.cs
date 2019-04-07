using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class WeeklyMeetingScheduleViewModel
    {
        public WeeklyMeetingScheduleViewModel()
        {
            SubjectsSelectList = new List<SelectListItem>();
            ClassSelectList = new List<SelectListItem>();
            DaysSelectList = new List<SelectListItem>();
        }
        public List<SelectListItem> SubjectsSelectList { get; set; }
        public List<SelectListItem> ClassSelectList { get; set; }
        public List<SelectListItem> DaysSelectList { get; set; }
        public WeeklyMeetingSchedule WeeklyMeetingSchedule { get; set; }
        public int Id { get; set; }
        public string Day { get; set; }
        public string Period { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string TeacherName { get; set; }
        public string Class { get; set; }
    }
}