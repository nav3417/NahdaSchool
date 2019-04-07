using AlnahdAweeklyPlans.Models;
using Database;
using Database.DataBaseCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers.EnternalResources
{
    public class WeeklyMeetingScheduleController : Controller
    {
        // GET: WeeklyMeetingSchedule
        Nahda_AttendanceEntities db;
        IUnitOfWork unitofwork;

        public WeeklyMeetingScheduleController()
        {
            db = new Nahda_AttendanceEntities();
            unitofwork = new UnitOfWork();
        }
        // GET: StudentFeedBackReport
        public ActionResult Index()
        {
            var load = unitofwork.WeeklyMeetingScheduleRepository.Get().ToList();
            var a = (from m in load
                     join q in db.Exam_Subjects on m.SubjectId equals q.Id
                     join w in db.Student_ClassMaster on m.ClassId equals w.Id
                     select new WeeklyMeetingScheduleViewModel()
                     {
                         Id = m.Id,
                         Class=w.Class,
                         Period=m.Period,
                         Day=m.Day,
                         From=Convert.ToDateTime(m.From).ToShortTimeString(),
                         To= Convert.ToDateTime(m.To).ToShortTimeString(),
                         Subject=q.Title,
                         TeacherName=m.TeacherName
                     }).ToList();
            return View(a);
        }
        // GET: StudentFeedBackReport/Create
        public ActionResult Create(int? id)
        {
            WeeklyMeetingScheduleViewModel model = new WeeklyMeetingScheduleViewModel();
            model.SubjectsSelectList = Exam_Subjects();
            model.ClassSelectList = LoadClases();
            model.DaysSelectList = LoadDays();
            if (id > 0)
            {
                model.WeeklyMeetingSchedule = db.WeeklyMeetingSchedules.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(model);
        }
        public List<SelectListItem> LoadClases()
        {
            return (from m in db.Student_ClassMaster  select m).Select(x => new SelectListItem() { Text = x.Class, Value = x.Id.ToString() }).ToList();
        }
        public List<SelectListItem> LoadDays()
        {
            return (from m in db.md_type where m.type== "Days" select m).Select(x => new SelectListItem() { Text = x.Name, Value = x.Name}).ToList();
        }
        public List<SelectListItem> Exam_Subjects()
        {
            var a = db.Exam_Subjects.ToList();
            return a.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
        }
        // POST: StudentFeedBackReport/Create
        [HttpPost]
        public ActionResult Create(WeeklyMeetingScheduleViewModel collection)
        {
            try
            {
                //  collection.CleaningStaffByLocation.ApprovedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                if (collection.WeeklyMeetingSchedule.Id > 0)
                {
                    //collection.StudentFeedBackReport.ModifiedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.ModifiedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.WeeklyMeetingScheduleRepository.Update(collection.WeeklyMeetingSchedule);
                }
                else
                {
                    //collection.StudentFeedBackReport.CreatedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.WeeklyMeetingScheduleRepository.Insert(collection.WeeklyMeetingSchedule);
                }
                unitofwork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}