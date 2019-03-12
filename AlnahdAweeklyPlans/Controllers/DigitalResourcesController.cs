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
    public class DigitalResourcesController : Controller
    {
        // GET: DigitalResources

        IUnitOfWork unitOfWork;
        LessonPlanService service;
        LessonPlanMapping mapping;
        DigitalResourcesMapping dmapping;
        LessonPlanModel model;
        public DigitalResourcesController()
        {
            unitOfWork = new UnitOfWork();
            service = new LessonPlanService();
            mapping = new LessonPlanMapping();
            model = new LessonPlanModel();
            dmapping = new DigitalResourcesMapping();
        }
        // GET: Gallery
        public ActionResult Index(int id)
        {

            Session[WebUtil.ParentId] = null;
            Session[WebUtil.Controller] = null;
            var controler = unitOfWork.LessonPlanCategoryRepository.GetByID(id).ControllerName;
            Session[WebUtil.Controller] = controler;
            var list = mapping.ToList(unitOfWork.LessonPlanCategoryRepository.Get(x => x.ParentId == id));
            var general = new DigitalResourceModel();
            List<CategoryGeneralModel> generalmodellist = new List<CategoryGeneralModel>();
            foreach (var i in list)
            {
                CategoryGeneralModel obj = new CategoryGeneralModel();
                obj.Name = unitOfWork.LessonPlanCategoryRepository.GetByID(i.Id).Name;
                obj.TotalSubList = unitOfWork.DigitalResourceMasterRepository.Get(x => x.CategoryId == i.Id).Count();
                generalmodellist.Add(obj);
            }
            ViewBag.category = mapping.ToObj(unitOfWork.LessonPlanCategoryRepository.GetByID(id));
            general.CategoryGeneralModelList = generalmodellist;
            ViewBag.id = id;
            CategoryGeneralModel controller = new CategoryGeneralModel();
            CategoryModel md = new CategoryModel();
            md.ParentId = id;
            md.controller = (string)Session[WebUtil.Controller];
            general.CategoryModel = md;
            Session[WebUtil.ParentId] = id;
            return View(general);
        }
        public ActionResult Add(int? id)
        {
            DigitalResourceMasterModel model = new DigitalResourceMasterModel();
           
            if (id != null)
            {
                model = dmapping.ToObj(unitOfWork.DigitalResourceMasterRepository.GetByID(id));
                foreach (var item in model.DigitalResourceClasses)
                {
                    int clasid = (int)item.ClassId;
                    model.Selecteditems.Add(clasid);
                }
            }
            var clas = mapping.ToList(unitOfWork.LessonPlanClassRepository.Get().ToList());
            model.LessonPlanClassModellist = clas;
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(DigitalResourceMasterModel model)
        {
            int parentid = (int)Session[WebUtil.ParentId];
            List<SelectListItem> classes = unitOfWork.LessonPlanClassRepository.Get().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.list = classes;
            DigitalResourceMaster master = new DigitalResourceMaster();
            master.title = model.title;
            master.DescriptionLong = model.DescriptionLong;
            master.DescriptionShort = model.DescriptionShort;
            master.CreatedOn = DateTime.Now;
            master.From = model.From;
            master.To = model.To;
            master.CategoryId = (int)Session[WebUtil.ParentId];

            foreach (string fcName in Request.Files)
            {
                DigitalResourceFile filedetail = new DigitalResourceFile();
                HttpPostedFileBase file = Request.Files[fcName];
                //string[] nameandextension = file.FileName.Split('.');
                filedetail.ShowFileName = file.FileName;
                filedetail.FileExtnsion = file.FileName.Substring(file.FileName.LastIndexOf("."));
                filedetail.URL = DateTime.Now.Ticks.ToString();
                string path = "/Images/" + unitOfWork.LessonPlanCategoryRepository.GetByID(master.CategoryId).Name + file.FileName;
                string createpath = "/Images/" + unitOfWork.LessonPlanCategoryRepository.GetByID(master.CategoryId).Name;
                string dicrectory = Request.MapPath(createpath);
                Directory.CreateDirectory(dicrectory);
                string dicrectory2 = Request.MapPath(path);
                file.SaveAs(dicrectory2);
                master.DigitalResourceFiles.Add(filedetail);
            }
            foreach (var item in model.Selecteditems)
            {
                DigitalResourceClass clas = new DigitalResourceClass();
                clas.ClassId = item;
                clas.CreatedBy = "Naveed";
                clas.CreatedOn = DateTime.Now;
                clas.MasterId = master.Id;
                master.DigitalResourceClasses.Add(clas);
            }
            unitOfWork.DigitalResourceMasterRepository.Insert(master);
            unitOfWork.Save();
            return RedirectToAction("Index", new { id = Session[WebUtil.ParentId] });
        }
    }
}