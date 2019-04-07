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
    public class NewStaffController : Controller
    {
        Nahda_AttendanceEntities db;
        IUnitOfWork unitofwork;
        
        public NewStaffController()
        {
            db = new Nahda_AttendanceEntities();
            unitofwork = new UnitOfWork();
        }
        // GET: NewStaff
        public ActionResult Index()
        {
            //model.NewStaffJoiningList = unitofwork.NewStaffJoiningRepository.Get().ToList();
            return View();
        }
        public ActionResult NewStaf()
        {
            return PartialView();
        }
        public ActionResult loadnewstaff()
        {
            //var x=from m in unitofwork.NewStaffJoiningRepository.Get() join q in db.md_type on m.Gender equals q. 
            var load = unitofwork.NewStaffJoiningRepository.Get().Select(x=> new NewStaffJoiningModel(){
                Id=x.Id,
                FullName=x.FullName,
                PassportCountryIssue=x.PassportCountryIssue,
                Gender=x.Gender,
                HRDesignation=x.HRDesignation,
                HrDepartment= department(x.HrDepartment),
                CellNo=x.CellNo,
                EmaratedIdNo=x.EmaratedIdNo,
                Religion=x.Religion
            });
            return Json(new { data = load }, JsonRequestBehavior.AllowGet);
        }

        public string department(int? id)
        {
            return db.hr_department.Where(x => x.Id ==id).FirstOrDefault().dept_NameEn;
        }
        public ActionResult AddEdit(int? id)
        {
            NewStaffJoiningViewModel model=new NewStaffJoiningViewModel();
            model.GenderListItems = Genders(db.md_type.Where(x => x.type.Equals("Gender")).ToList());
            model.HrDepartmentListItems = HrDepartments(db.hr_department.ToList());
            model.ReligionListItems = Religions(db.md_type.Where(x => x.type.Equals("Religion")).ToList());
            model.HrDesignationListItems = HrDesignations();
            model.IsAirTicket = IsAirticket();
            model.PassportCountryList = PassportCountry(db.md_type.Where(x => x.type.Equals("Country")).ToList());
            if(id>0)
            {
                model.NewStaffJoining = db.NewStaffJoinings.Where(x => x.Id == id).FirstOrDefault();
            }
            return View(model);
        }
        public List<SelectListItem> IsAirticket()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Yes", Value = "Yes"});
            list.Add(new SelectListItem() { Text = "No", Value = "No" });
            return list;
        }
        public ActionResult Post(NewStaffJoiningViewModel model)
        {
            model.NewStaffJoining.CertifiedOn = DateTime.Now;
            model.NewStaffJoining.CertifiedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
            //int gndrid = Convert.ToInt32(model.NewStaffJoining.Gender);
            //model.NewStaffJoining.Gender = db.md_type.Where(x => x.Id == gndrid).FirstOrDefault().Name;
            //int countryid = Convert.ToInt32(model.NewStaffJoining.PassportCountryIssue);
            //model.NewStaffJoining.PassportCountryIssue = db.md_type.Where(x => x.Id == countryid).FirstOrDefault().Name;
            //int deignationid = Convert.ToInt32(model.NewStaffJoining.HRDesignation);
            //model.NewStaffJoining.HRDesignation = db.hr_designation.Where(x => x.Id == deignationid).FirstOrDefault().des_NameEn;
            if (model.NewStaffJoining.Id>0)
            {
                unitofwork.NewStaffJoiningRepository.Update(model.NewStaffJoining);
                unitofwork.Save();
            }
            else
            {
                db.NewStaffJoinings.Add(model.NewStaffJoining);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public List<SelectListItem> HrDesignations()
        {
            List<SelectListItem> list = db.hr_designation.Select(x => new SelectListItem() { Text = x.des_NameEn, Value = x.des_NameEn.ToString() }).ToList();
            return list;
        }
        public List<SelectListItem> HrDepartments(List<hr_department> lists)
        {
            List<SelectListItem> list = lists.Select(x => new SelectListItem() { Text = x.dept_NameEn, Value = x.Id.ToString() }).ToList();
            return list;
        }
        public List<SelectListItem> Religions(List<md_type> data)
        {
            List<SelectListItem> list = data.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name.ToString() }).ToList();
            return list;
        }
        public List<SelectListItem> Genders(List<md_type> data )
        {
            List<SelectListItem> list = data.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name.ToString() }).ToList();
            return list;
        }
        public List<SelectListItem> PassportCountry(List<md_type> data)
        {
            List<SelectListItem> list = data.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name.ToString() }).ToList();
            return list;
        }
        public string passsportcountryissue(int id)
        {
            return db.md_type.Where(x => x.Id == id).FirstOrDefault().Name;
        }
    }
}