using Database.DataBaseCrud;
using Services;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using System.IO;
using Database;

namespace AlnahdAweeklyPlans.Controllers
{
    public class ApprovalController : Controller
    {
        IUnitOfWork unitOfWork;
        LessonPlanService service;
        LessonPlanMapping mapping;
        public ApprovalController()
        {
            unitOfWork = new UnitOfWork();
            service = new LessonPlanService();
            mapping = new LessonPlanMapping();
        }
        // GET: Approval
        public ActionResult Index()
        {
            var Subjects = unitOfWork.LessonPlanSubjectRepository.Get().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            List<SelectListItem> Classes = unitOfWork.LessonPlanClassRepository.Get().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.subjects = Subjects;
            ViewBag.classes = Classes;
            LessonPlanModel model = new LessonPlanModel();
            return View(model);
        }
        public ActionResult load()
        {
            return PartialView();
        }
        public ActionResult loadsubjects(int id)
        {
            var Subjects = unitOfWork.LessonPlanSubjectRepository.Get(x => x.ClassId == id).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.subjects = Subjects;
            return PartialView();
        }
        public ActionResult DetailView(detailsearchmodel data)
        {
            LessonPlanModel model = new LessonPlanModel();
            var a = data.from.Replace("-", "/");
            DateTime from = Convert.ToDateTime(a);
            DateTime to = from.AddDays(4);
            var result= mapping.ToList(unitOfWork.LessonsPlanMasterRepository.Get((x => x.IsActive == false && x.ClassId == data.clas && x.SubjectId == data.subject && x.CreatedFor >= from && x.CreatedFor <= to)));
            model.LessonsPlanMasterModelList = result;
            ViewBag.count = model.LessonsPlanMasterModelList.Count;
            return PartialView(model);
        }
        public ActionResult uprove(int id)
        {
            var data=    unitOfWork.LessonsPlanMasterRepository.GetByID(id);
            data.IsApproved = true;
            unitOfWork.Save();
            WebUtil.sentmail("Approved", Convert.ToDateTime(data.CreatedOn).ToShortDateString(),data.LessonPlanCategory.Name,Convert.ToDateTime(data.CreatedFor).ToShortDateString(),data.Class,data.Subject);
            return Json("done", JsonRequestBehavior.AllowGet);
        }
        public ActionResult remove(int id)
        {
            var data = unitOfWork.LessonsPlanMasterRepository.GetByID(id);
            data.IsDeleted = true;
            unitOfWork.Save();
            WebUtil.sentmail("Removed", Convert.ToDateTime(data.CreatedOn).ToShortDateString(), data.LessonPlanCategory.Name, Convert.ToDateTime(data.CreatedFor).ToShortDateString(), data.Class, data.Subject);
            return Json("done", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Editdetail(int id)
        {
            var data = mapping.ToObj(unitOfWork.LessonsPlanMasterRepository.GetByID(id));
            // data.URL = WebUtil.BasePath  + data.Category + "/" + data.Clas + "_" + data.Subject + "/" + data.URL+data.FileExtnsion;
            ViewBag.count = data.LessonPlanDetailList.Count-1;
            return View(data);
        }
        public ActionResult EditPost(LessonsPlanMasterModel model)
        {
            var data = unitOfWork.LessonsPlanMasterRepository.GetByID(model.Id);
            foreach (var item in model.LessonPlanDetailList)
            {
                var data2= unitOfWork.LessonPlanDetailRepository.GetByID(item.Id);
                data2.IsActive = item.IsActive;
                unitOfWork.Save();
            }
            data.HOSRemarks = model.HOSRemarks;
            data.IsApproved = model.IsApproved;
            data.AssignmentComments = model.HOSRemarks;
            foreach (string fcName in Request.Files)
            {
                LessonPlanDetail filedetail = new LessonPlanDetail();
                HttpPostedFileBase file = Request.Files[fcName];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    filedetail.ShowFileName = file.FileName;
                    filedetail.FileExtnsion = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    filedetail.URL = DateTime.Now.Ticks.ToString();
                    filedetail.Master_Id = data.Id;
                    filedetail.CreatedBy = "Naveed";
                    filedetail.CreatedOn = DateTime.Now;
                    string dynamicpath = data.LessonPlanCategory.Name + "/" + data.LessonPlanClass.Name + "_" + data.LessonPlanSubject.Name;
                    string path = "/Images/LessonPlan/" + dynamicpath + "/" + filedetail.URL + filedetail.FileExtnsion;
                    string createpath = "/Images/LessonPlan/" + dynamicpath;
                    string dicrectory = Request.MapPath(createpath);
                    Directory.CreateDirectory(dicrectory);
                    string dicrectory2 = Request.MapPath(path);
                    file.SaveAs(dicrectory2);
                    data.LessonPlanDetails.Add(filedetail);
                }
                    //string[] nameandextension = file.FileName.Split('.');

            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public ActionResult BulkApprove(LessonPlanModel model)
        {
            foreach (var i in model.LessonPlanDetailList)
            {
                var data = unitOfWork.LessonsPlanMasterRepository.GetByID(i.Id);
                if(data.IsApproved!= i.IsApproved)
                {
                    data.IsApproved = i.IsApproved;
                    unitOfWork.Save();
                    string status = data.IsApproved == true ? "Approved" : "Rejected";
                    WebUtil.sentmail(status, Convert.ToDateTime(data.CreatedOn).ToShortDateString(), data.LessonPlanCategory.Name, Convert.ToDateTime(data.CreatedFor).ToShortDateString(), data.Class, data.Subject);
                }
              
            }
            
            return RedirectToAction("Index");
        }

        

    }
}