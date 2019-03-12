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
    public class GalleryController : Controller
    {

        IUnitOfWork unitOfWork;
        LessonPlanService service;
        LessonPlanMapping mapping;
        DigitalResourcesMapping dmapping;
        LessonPlanModel model;
        public GalleryController()
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
                var getpbj = unitOfWork.LessonPlanCategoryRepository.GetByID(i.Id);
                obj.Id = getpbj.Id;
                obj.Name = getpbj.Name;
                obj.TotalSubList = unitOfWork.DigitalResourceMasterRepository.Get(x => x.CategoryId == i.Id).Count();
                generalmodellist.Add(obj);
            }
            ViewBag.category = mapping.ToObj(unitOfWork.LessonPlanCategoryRepository.GetByID(id));
            general.CategoryGeneralModelList = generalmodellist;
            ViewBag.id = id;
            CategoryGeneralModel controller = new CategoryGeneralModel();
            CategoryModel md = new CategoryModel();
            md.Id = id;
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
            int category = (int)Session[WebUtil.SubParentId];
            DigitalResourceMaster master = null;
            if (model.Id == 0)
            {
                master = new DigitalResourceMaster();
            }
            else
            {
                master = unitOfWork.DigitalResourceMasterRepository.GetByID(model.Id);
            }
            master.title = model.title;
            master.DescriptionLong = model.DescriptionLong;
            master.DescriptionShort = model.DescriptionShort;
            master.CreatedOn = DateTime.Now;
            master.From = Convert.ToDateTime(model.Fromstring);
            master.To = Convert.ToDateTime(model.Tostring);
            master.CategoryId = category;
            foreach (string fcName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fcName];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    DigitalResourceFile filedetail = new DigitalResourceFile();
                    
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

            }
            if (model.Id != 0)
            {
                List<int> intlist = new List<int>();
                foreach (var i in master.DigitalResourceClasses)
                {
                    int find = model.Selecteditems.Find(x => x.Equals(i.ClassId));
                    if (find == 0)
                    {
                        intlist.Add(i.Id);
                    }
                }
                foreach (var i in intlist)
                {
                    var del = unitOfWork.DigitalResourceClassRepository.GetByID(i);
                    unitOfWork.DigitalResourceClassRepository.Delete(del);
                    unitOfWork.Save();
                }
                foreach (var i in model.Selecteditems)
                {
                    var find = master.DigitalResourceClasses.FirstOrDefault(x => x.ClassId == i);
                    if(find==null)
                    {
                        DigitalResourceClass clas = new DigitalResourceClass();
                        clas.ClassId = i;
                        clas.CreatedBy = "Naveed";
                        clas.CreatedOn = DateTime.Now;
                        clas.MasterId = master.Id;
                        master.DigitalResourceClasses.Add(clas);
                    }
                }
            }
            if (model.Id == 0)
            {
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
            }
            unitOfWork.Save();
            return RedirectToAction("Index", new { id = Session[WebUtil.ParentId] });
        }
        public ActionResult DetailPage(int id)
        {
            Session[WebUtil.SubParentId] = id;
            int category = (int)Session[WebUtil.SubParentId];
            Session.Timeout = 240;
            var detail = dmapping.ToList(unitOfWork.DigitalResourceMasterRepository.Get(x => x.CategoryId == id).ToList());
            DigitalResourceModel model = new DigitalResourceModel();
            model.MasterModelList = detail;
            CategoryModel md = new CategoryModel();
            var db = unitOfWork.LessonPlanCategoryRepository.GetByID(id);
            md.Id = id;
            md.ParentId = (int)db.ParentId;
            model.CategoryModel = md;
            return View(model);
        }
        public ActionResult LoadSubCategories(int id)
        {
            DigitalResourceModel model = new DigitalResourceModel();
            var data = LoadcateGoriesByParent(id).Select(x => new CategoryGeneralModel { Id = x.Id, Name = x.Name, parentId = x.ParentId, TotalSubList = count(x.Id) }).ToList();
            if (data.Count() > 0) model.CategoryGeneralModelList = data;
            return PartialView(model);
        }
        public List<CategoryModel> LoadcateGoriesByParent(int id)
        {
            return mapping.ToList(unitOfWork.LessonPlanCategoryRepository.Get(x => x.ParentId == id));
        }
        int count(int id)
        {
            return unitOfWork.DigitalResourceMasterRepository.Get(x => x.CategoryId == id).Count();
        }
    }
}