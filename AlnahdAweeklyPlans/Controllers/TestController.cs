using AlnahdAweeklyPlans.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers
{
    public class TestController : Controller
    {
        Nahda_AttendanceEntities db;
        public TestController()
        {
            db = new Nahda_AttendanceEntities();
        }
        // GET: Test
        public ActionResult Index()
        {
            MultiSelect model = new MultiSelect();
            model.List = db.Exam_Subjects.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(model);
        }
        public ActionResult Test()
        {
            MultiSelect model = new MultiSelect();
            model.List = db.Exam_Subjects.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(model);
        }

    }
}