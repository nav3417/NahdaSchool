using AlnahdAweeklyPlans.Models;
using Database;
using Database.DataBaseCrud;
using Newtonsoft.Json;
using Services;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers
{
    public class CommunicationController : Controller
    {

        IUnitOfWork unitOfWork;
        LessonPlanService service;
        LessonPlanMapping mapping;
        DigitalResourcesMapping dmapping;
        LessonPlanModel model;
        Nahda_AttendanceEntities db;
        public CommunicationController()
        {
            unitOfWork = new UnitOfWork();
            service = new LessonPlanService();
            mapping = new LessonPlanMapping();
            model = new LessonPlanModel();
            dmapping = new DigitalResourcesMapping();
            db = new Nahda_AttendanceEntities();
        }
        public ActionResult Index()
        {
            CommunicationModel lists = new CommunicationModel();
            lists.SelectListItemSChool = db.Schools.Select(x => new SelectListItem() { Text = x.SchoolNameEn, Value = x.Id.ToString() }).ToList();
            lists.SelectListItemDepartment = db.hr_department.Select(x => new SelectListItem() { Text = x.dept_NameEn, Value = x.Id.ToString() }).ToList();
            lists.SelectListItemSubjects = db.Exam_Subjects.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(lists);
        }
        //public ActionResult loaddepartments()
        //{

        //}(
        public ActionResult Detailview(CommunicationModel model)
        {
            CommunicationModel modl = new CommunicationModel();
            modl.HREmployeeMstslctList = db.HREmployeeMsts.Where(x=>x.SchoolId==model.SchlsId).Select(x=> new SelectListItem() {Value=x.Id.ToString(),Text=x.hr_name}).ToList();        
            return PartialView(modl);
        }
        public ActionResult emails(CommunicationModel idslist)
        {
            List<sendemail> list = new List<sendemail>();
            List<int> intlist = JsonConvert.DeserializeObject<List<int>>(idslist.intlist);
            list.Add(new sendemail { Id = 1, Name = "Naveed", Email = "naveed@gmail.com" });
            list.Add(new sendemail { Id = 2, Name = "Khalid", Email = "Khalid@2gmail.com" });
            list.Add(new sendemail { Id = 3, Name = "Fazal", Email = null });
            var list2 =(from m in list join q in intlist on m.Id equals q select m).ToList();
            //CommunicationModel mdl=new CommunicationModel(
            var selectlist = list2.Select(x => new SelectListItem { Text = (x.Email == null)?x.Name:x.Name+"("+ x.Email+")", Value = (x.Email == null) ?x.Id.ToString()+"":x.Id+"/"+ x.Email, Selected= (x.Email==null)?true:false}).ToList();
            ViewBag.count = list2.Count();
            CommunicationModel obj = new CommunicationModel();
            obj.sendemailList = list2;
            obj.HREmployeeMstslctList = selectlist;
            //obj.selecteditems = (from m in intlist join q in list on m equals q.Id where q.Email==null select m).ToList();
            return PartialView(obj);
        }
        public ActionResult sendemail(CommunicationModel emails)
        {
            Nahda_AttendanceEntities db = new Nahda_AttendanceEntities();
            CommunicationGroupMaster mster = new CommunicationGroupMaster();
            mster.Title = emails.Title;
            mster.Remarks = emails.Remarks;
            mster.CreatedBy = "Naveed";
            mster.CreatedOn = DateTime.Now;
            mster.School =Convert.ToInt32(emails.SchlsId);
            List<CommunicationGroupDetail> list = new List<CommunicationGroupDetail>();
            foreach (var i in emails.selecteditems)
            {
                string[] split = i.Split('/');
                CommunicationGroupDetail obj = new CommunicationGroupDetail();
                obj.CreatedBy = "Naveed";
                obj.CreatedOn = DateTime.Now;
                obj.Email = split[1];
                obj.StaffId =Convert.ToInt32(split[0]);
                list.Add(obj);
            }
            mster.CommunicationGroupDetails = list;
            db.CommunicationGroupMasters.Add(mster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult loaddpt(int id)
        {
            var model = new CommunicationModel();
            model.SelectListItemDepartment = db.hr_department.Where(x => x.SchoolId == id).Select(x=>new SelectListItem() {Text=x.dept_NameEn,Value=id.ToString()}).ToList();
            return PartialView(model);
        }
        public ActionResult loadsub(int id)
        {
            CommunicationModel model = new CommunicationModel();
             model.SelectListItemSubjects = (from emp in db.HREmployeeMsts
                        join subGrp in db.Exam_subjectGroup on emp.AccountId equals subGrp.StaffId
                        join examSub in db.Exam_Subjects on subGrp.SubjectId equals examSub.Id
                        where emp.hr_DeptId == id
                        select examSub
                        ).Distinct().Select(x=> new SelectListItem() {Text=x.Title,Value=x.Id.ToString()}).ToList();
            //subject group
            return PartialView(model);
        }

    }
}