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
    public class CleaningStaffLocationController : Controller
    {
        // GET: CleaningStaffLocation
        Nahda_AttendanceEntities db;
        IUnitOfWork unitofwork;

        public CleaningStaffLocationController()
        {
            db = new Nahda_AttendanceEntities();
            unitofwork = new UnitOfWork();
        }
        // GET: StudentFeedBackReport
        public ActionResult Index()
        {
            var load = unitofwork.CleaningStaffByLocationRepository.Get().ToList();
            var a = (from m in load
                     join q in db.Accounts on m.StaffId equals q.Id
                     join w in db.md_type on m.StaffType equals w.Id
                     select new CleaningStaffByLocationViewModel()
                     {
                        Id=m.Id,
                        StaffType=w.Name,
                        Location=m.Location,
                        Staff=q.AccountName
                     }).ToList();
            return View(a);
        }
        // GET: StudentFeedBackReport/Create
        public ActionResult Create(int? id)
        {
            CleaningStaffByLocationViewModel model = new CleaningStaffByLocationViewModel();
            model.StaffTypeListItems = LoadStafftype();
            var t = (from m in db.HREmployeeMsts join q in db.Accounts on m.AccountId equals q.Id select q);
            model.StaffListItems = t.Select(x => new SelectListItem() { Text = x.AccountName, Value = x.Id.ToString() }).ToList();
            if (id > 0)
            {
                model.CleaningStaffByLocation = db.CleaningStaffByLocations.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(model);
        }
        public List<SelectListItem> LoadStafftype()
        {
            return db.md_type.Where(x=>x.type.Equals("CleaningStaff_type")).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }
        public List<SelectListItem> Exam_Subjects()
        {
            var a = db.Exam_Subjects.ToList();
            return a.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
        }
        // POST: StudentFeedBackReport/Create
        [HttpPost]
        public ActionResult Create(CleaningStaffByLocationViewModel collection)
        {
            try
            {
              //  collection.CleaningStaffByLocation.ApprovedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                if (collection.CleaningStaffByLocation.Id > 0)
                {
                    //collection.StudentFeedBackReport.ModifiedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.ModifiedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.CleaningStaffByLocationRepository.Update(collection.CleaningStaffByLocation);
                }
                else
                {
                    //collection.StudentFeedBackReport.CreatedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.CleaningStaffByLocationRepository.Insert(collection.CleaningStaffByLocation);
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