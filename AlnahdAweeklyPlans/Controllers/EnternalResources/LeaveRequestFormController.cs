using AlnahdAweeklyPlans.Models;
using Database;
using Database.DataBaseCrud;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers.EnternalResources
{
    public class LeaveRequestFormController : Controller
    {
        IUnitOfWork unitOfWork;
        Nahda_AttendanceEntities db;
        public LeaveRequestFormController()
        {
            unitOfWork = new UnitOfWork();
            db = new Nahda_AttendanceEntities();
        }
        // GET: LeaveRequestForm
        public ActionResult Index()
        {
            LeaveRequestFormViewModel obj = new LeaveRequestFormViewModel();
            obj.StudentSelectListItems = LoadStudents();
            obj.ParentApprovalSelectListItems = ParentApproval();
            return View(obj);
        }
       
        public List<SelectListItem> LoadStudents()
        {
            return (from m in db.Accounts join q in db.Student_StudentDtl on m.Id equals q.AccountID select m).Select(x => new SelectListItem() { Text = x.AccountName, Value = x.Id.ToString() }).ToList();
        }
        public ActionResult Post(LeaveRequestFormViewModel data)
        {
            data.LeaveRequestForm.ParentApproval = data.Parentapproval=="1"?true:false;
            data.LeaveRequestForm.Date = DateTime.Now;
            unitOfWork.LeaveRequestFormRepository.Insert(data.LeaveRequestForm);
            unitOfWork.Save();
            return Redirect("/LeaveRequestForm/Index");
        }
        //public List<SelectListItem> LoadClasses()
        //{
        //    return (from m in db.Accounts join q in db.Student_StudentDtl on m.Id equals q.AccountID select m).Where(x => x.InActive == true).Select(x => new SelectListItem() { Text = x.AccountName, Value = x.Id.ToString() }).ToList();
        //}
        public List<SelectListItem> ParentApproval()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Yes", Value = 1.ToString() });
            list.Add(new SelectListItem() { Text = "No", Value = 0.ToString() });
            return list;
        }
        public ActionResult Leaverequests()
        {

            return PartialView();
        }
        public ActionResult datatableload()
        {
            var list = Mapping.ToList(unitOfWork.LeaveRequestFormRepository.Get());
            var load = (from m in unitOfWork.LeaveRequestFormRepository.Get()
                        join q in db.Accounts on m.StudentId equals q.Id
                        select new LeaveRequestFormModel()
{
    AdminRemarks = m.AdminRemarks,
    CellNo = m.CellNo,
    Class = m.Class,
    Date = Convert.ToDateTime(m.Date).ToShortDateString(),
    From = Convert.ToDateTime(m.From).ToShortDateString(),
    To = Convert.ToDateTime(m.To).ToShortDateString(),
    Id = m.Id,
    ParentApproval = m.ParentApproval == true ? "Yes" : "No",
    Reason = m.Reason,
    StudentCode = q.AccountCode,
    StudentName = q.AccountName,
    Term = m.Term
}).ToList();
            return Json(new { data = load }, JsonRequestBehavior.AllowGet);
        }
        public Account info(int id)
        {    
            var d = db.Accounts.Where(x => x.Id == id).FirstOrDefault();
            return d;
        }

        
    }
}