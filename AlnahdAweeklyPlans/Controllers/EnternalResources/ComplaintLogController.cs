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
    public class ComplaintLogController : Controller
    {
        // GET: ComplaintLog
        Nahda_AttendanceEntities db;
        IUnitOfWork unitofwork;

        public ComplaintLogController()
        {
            db = new Nahda_AttendanceEntities();
            unitofwork = new UnitOfWork();
        }
        // GET: StudentFeedBackReport
        public ActionResult Index()
        {
            var load = unitofwork.ComplainLogRepository.Get().ToList();
            var a = (from m in load
                     join q in db.Accounts on m.StudentId equals q.Id
                     select new ComplaintLogViewModel()
                     {
                         Id = m.Id,
                         CellNo=m.CellNo,
                         ActionTaken=m.ActionTaken,
                         Class=m.Class,
                         ContactBy=m.ContactBy,
                         Date=Convert.ToDateTime(m.Date).ToShortDateString(),
                         Description=m.Description,
                         Name=m.Name,
                         InformPerson=m.InformPerson,
                         Consulted=m.IsConsulted==true?"Yes":"No",
                         Student=q.AccountName
                     }).ToList();            
            return View(a);
        }
        // GET: StudentFeedBackReport/Create
        public ActionResult Create(int? id)
        {
            ComplaintLogViewModel model = new ComplaintLogViewModel();
            model.StusentListItems = LoadStudents();
            if (id > 0)
            {
                model.ComplainLog = db.ComplainLogs.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(model);
        }
        public List<SelectListItem> LoadStudents()
        {
            return (from m in db.Accounts join q in db.Student_StudentDtl on m.Id equals q.AccountID select m).Select(x => new SelectListItem() { Text = x.AccountName, Value = x.Id.ToString() }).ToList();
        }
        // POST: StudentFeedBackReport/Create
        [HttpPost]
        public ActionResult Create(ComplaintLogViewModel collection)
        {
            try
            {
                //  collection.CleaningStaffByLocation.ApprovedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                if (collection.ComplainLog.Id > 0)
                {
                    //collection.StudentFeedBackReport.ModifiedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.ModifiedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.ComplainLogRepository.Update(collection.ComplainLog);
                }
                else
                {
                    //collection.StudentFeedBackReport.CreatedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.ComplainLogRepository.Insert(collection.ComplainLog);
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