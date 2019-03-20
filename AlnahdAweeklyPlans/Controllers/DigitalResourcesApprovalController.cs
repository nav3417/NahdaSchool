using Database;
using Database.DataBaseCrud;
using Services;
using Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers
{
    public class DigitalResourcesApprovalController : Controller
    {
        IUnitOfWork unitOfWork;
        LessonPlanService service;
        LessonPlanMapping mapping;
        DigitalResourcesMapping dmapping;
        public DigitalResourcesApprovalController()
        {
            unitOfWork = new UnitOfWork();
            service = new LessonPlanService();
            mapping = new LessonPlanMapping();
            dmapping = new DigitalResourcesMapping();
        }
        // GET: DigitalResourcesApproval

        public ActionResult Index()
        {
            List<SelectListItem> Category = unitOfWork.LessonPlanCategoryRepository.Get(x=>x.HasParent==false && x.Name!= "LessonPlans").Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.Category = Category;
            return View();
        }

        public ActionResult DetailView(DigitalResourceMasterModel model)
        {
            DateTime from = Convert.ToDateTime(model.Fromstring);
            DateTime to = Convert.ToDateTime(model.Tostring);
            var DRM = dmapping.ToList(unitOfWork.DigitalResourceMasterRepository.Get(x => x.CategoryIdMajorParent == model.FinalCategoryId && x.IsActive == true && x.IsDeleted != true));
            ViewBag.count = DRM.Count;
           // ViewBag.id = id;
            return PartialView(DRM);
        }
        public ActionResult uprove(int id)
        {
            var data = unitOfWork.DigitalResourceMasterRepository.GetByID(id);
            data.IsApproved = true;
            unitOfWork.Save();
            WebUtil.sentmailDigital("Approved", Convert.ToDateTime(data.CreatedOn).ToShortDateString(), data.LessonPlanCategory.Name, Convert.ToDateTime(data.From).ToShortDateString(), Convert.ToDateTime(data.To).ToShortDateString(), null);
            return Json("done", JsonRequestBehavior.AllowGet);
        }
        public ActionResult remove(int id)
        {
            var data = unitOfWork.DigitalResourceMasterRepository.GetByID(id);
            data.IsDeleted = true;
            unitOfWork.Save();
            WebUtil.sentmailDigital("Removed", Convert.ToDateTime(data.CreatedOn).ToShortDateString(), data.LessonPlanCategory.Name, Convert.ToDateTime(data.From).ToShortDateString(), Convert.ToDateTime(data.To).ToShortDateString(),null);
            return Json("done", JsonRequestBehavior.AllowGet);
        }
        public ActionResult BulkApprove(List<DigitalResourceMasterModel> model)
        {

            foreach (var i in model)
            {
                var data = unitOfWork.DigitalResourceMasterRepository.GetByID(i.Id);
                if (data.IsApproved != i.IsApproved)
                {
                    data.IsApproved = i.IsApproved;
                    unitOfWork.Save();
                    string status = data.IsApproved == true ? "Approved" : "Rejected";
                    WebUtil.sentmailDigital(status, Convert.ToDateTime(data.CreatedOn).ToShortDateString(), data.LessonPlanCategory.Name, Convert.ToDateTime(data.From).ToShortDateString(), Convert.ToDateTime(data.To).ToShortDateString(), null);
                }

            }
            var FinalCategoryId = model.FirstOrDefault().FinalCategoryId;
            return RedirectToAction("Index",new {id= FinalCategoryId});
        }
        public ActionResult Editdetail(int id)
        {
            var data = dmapping.ToObj(unitOfWork.DigitalResourceMasterRepository.GetByID(id));
            ViewBag.count = data.DigitalResourceFiles.Count - 1;
            return View(data);
        }
        public ActionResult EditPost(DigitalResourceMasterModel model)
        {
            var data = unitOfWork.DigitalResourceMasterRepository.GetByID(model.Id);
            foreach (var item in model.DigitalResourceFiles)
            {
                var data2 = unitOfWork.DigitalResourceFileRepository.GetByID(item.Id);
                data2.IsActive = item.IsActive;
                unitOfWork.Save();
            }
            data.AdminRemarks = model.AdminRemarks;
            data.IsApproved = model.IsApproved;
            data.DescriptionShort = model.DescriptionShort;
            string category = unitOfWork.LessonPlanCategoryRepository.GetByID(data.CategoryId).Name;
               HttpFileCollectionBase files = Request.Files;
            for (var i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    DigitalResourceFile filedetail = new DigitalResourceFile();
                    //string[] nameandextension = file.FileName.Split('.');
                    filedetail.ShowFileName = file.FileName;
                    filedetail.Category = category;
                    filedetail.FileExtnsion = file.ContentType;
                    filedetail.URL = DateTime.Now.Ticks.ToString();
                    string path = "/Images/" + category + "/" + file.FileName;
                    string createpath = "/Images/" + category;
                    string dicrectory = Request.MapPath(createpath);
                    Directory.CreateDirectory(dicrectory);
                    string dicrectory2 = Request.MapPath(path);
                    file.SaveAs(dicrectory2);
                    data.DigitalResourceFiles.Add(filedetail);
                }

            }
            unitOfWork.Save();
            return RedirectToAction("Index",new {id=model.FinalCategoryId});
        }
    }
}