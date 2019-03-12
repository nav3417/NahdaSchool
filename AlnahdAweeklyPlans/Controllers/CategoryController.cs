using Database;
using Database.DataBaseCrud;
using Services;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers
{
    public class CategoryController : Controller
    {
        IUnitOfWork unitOfWork;
        LessonPlanService service;
        LessonPlanMapping mapping;
        DigitalResourcesMapping dmapping;
        LessonPlanModel model;
        public CategoryController()
        {
            unitOfWork = new UnitOfWork();
            service = new LessonPlanService();
            mapping = new LessonPlanMapping();
            model = new LessonPlanModel();
            dmapping = new DigitalResourcesMapping();
        }
        // GET: Category
        public ActionResult Index()
        {
            model.CategoryList = mapping.ToList(unitOfWork.LessonPlanCategoryRepository.Get(x => x.HasParent == false));
            return View(model);
        }
        public ActionResult DisplayContent(int id)
        {
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
            return PartialView(general);
        }
        public ActionResult AddCategory(CategoryModel model)
        {
            Session[WebUtil.CategoryId] = model;
            int s = 10;
            bool n = model.Id == s ? true : false;
            List<SelectListItem> Categories;
            if (model.ParentId==0)
            {
                 Categories = unitOfWork.LessonPlanCategoryRepository.Get(x =>x.ParentId==null).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

            }
            else
            {
                 Categories = unitOfWork.LessonPlanCategoryRepository.Get(x => x.Id == model.Id).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
                int a = 2;

            }
            ViewBag.categories = Categories;
            LessonPlanModel md = new LessonPlanModel();
            md.Id = model.ParentId;
            model.ParentId = model.Id;
           // model.Id=
            return PartialView(model);
        }
        public bool check(int id,int parentid)
        {
            return id == parentid ? true : false;
        }
        [HttpPost]
        public ActionResult AddCategor(CategoryModel model)
        {
            LessonPlanCategory cat = new LessonPlanCategory();
            cat.Name = model.Name;
            cat.ParentId = model.ParentId;
            cat.HasParent = true;
            cat.CreatedBy = "Naveed";
            cat.CreatedOn = DateTime.Now;
            cat.IsActive = true;
            unitOfWork.LessonPlanCategoryRepository.Insert(cat);
            unitOfWork.Save();
            return RedirectToAction("Index",model.controller,new {Id=model.ParentId});
        }
        public ActionResult LoadSubCategories(int id)
        {
            DigitalResourceModel model = new DigitalResourceModel();
            var data = LoadcateGoriesByParent(id).Select(x=> new CategoryGeneralModel {Id=x.Id,Name=x.Name,parentId=x.ParentId,TotalSubList= count (x.Id)}).ToList();
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