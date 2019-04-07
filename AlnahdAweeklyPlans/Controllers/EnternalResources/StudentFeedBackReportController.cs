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
    public class StudentFeedBackReportController : Controller
    {
        Nahda_AttendanceEntities db;
        IUnitOfWork unitofwork;

        public StudentFeedBackReportController()
        {
            db = new Nahda_AttendanceEntities();
            unitofwork = new UnitOfWork();
        }
        // GET: StudentFeedBackReport
        public ActionResult Index()
        {
            var load = unitofwork.StudentFeedBackReportRepository.Get().ToList();
            var a = (from m in load
                     join q in db.Accounts on m.StudentId equals q.Id
                     join w in db.Exam_Subjects on m.SubjectId equals w.Id
                     select new StudentFeedBackViewModel()
                     {
                         Id = m.Id,
                         ApprovedBy = m.ApprovedBy,
                         Class = m.Class,
                         CreatedBy = m.CreatedBy,
                         Student = q.AccountName,
                         StudentCode = q.AccountCode,
                         Subject = w.Title,
                         Date= Convert.ToDateTime(m.Date).ToShortDateString(),
                         CreatedOn =Convert.ToDateTime(m.CreatedOn).ToShortDateString(),
                         Remarks=m.Remarks
                     }).ToList();
            return View(a);
        }
        // GET: StudentFeedBackReport/Create
        public ActionResult Create(int? id)
        {
            StudentFeedBackViewModel model=new StudentFeedBackViewModel();
            model.SubjectsSelectList = Exam_Subjects();
            model.StudentSelectList = LoadStudents();
            if (id>0)
            {
                model.StudentFeedBackReport = db.StudentFeedBackReports.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(model);
        }
        public List<SelectListItem> LoadStudents()
        {
            return (from m in db.Accounts join q in db.Student_StudentDtl on m.Id equals q.AccountID select m).Select(x => new SelectListItem() { Text = x.AccountName, Value = x.Id.ToString() }).ToList();
        }
        public List<SelectListItem> Exam_Subjects()
        {
            var a = db.Exam_Subjects.ToList();
            return a.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString()}).ToList();
        }
        // POST: StudentFeedBackReport/Create
        [HttpPost]
        public ActionResult Create(StudentFeedBackViewModel collection)
        {
            try
            {
                collection.StudentFeedBackReport.ApprovedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                if(collection.StudentFeedBackReport.Id>0)
                {
                    collection.StudentFeedBackReport.ModifiedOn = DateTime.Now;
                    collection.StudentFeedBackReport.ModifiedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.StudentFeedBackReportRepository.Update(collection.StudentFeedBackReport);
                }
                else
                {
                    collection.StudentFeedBackReport.CreatedOn = DateTime.Now;
                    collection.StudentFeedBackReport.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.StudentFeedBackReportRepository.Insert(collection.StudentFeedBackReport);
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
