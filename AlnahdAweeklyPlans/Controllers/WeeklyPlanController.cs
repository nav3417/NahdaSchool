using Database;
using Database.DataBaseCrud;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace AlnahdAweeklyPlans.Controllers
{
    public class WeeklyPlanController : Controller
    {
        IUnitOfWork unitOfWork;
        public WeeklyPlanController()
        {
            unitOfWork = new UnitOfWork();
        }
        // GET: WeeklyPlan
        public ActionResult Index(int Id)
        {
            Session[WebUtil.File] = null;
            /*List<SelectListItem>*/
            var Subjects = unitOfWork.LessonPlanSubjectRepository.Get().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            List<SelectListItem> Classes = unitOfWork.LessonPlanClassRepository.Get().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            List<SelectListItem> categories = unitOfWork.LessonPlanCategoryRepository.Get(x=>x.ParentId==Id).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.subjects = Subjects;
            ViewBag.classes = Classes;
            ViewBag.category = categories;
            LessonPlanModel model = new LessonPlanModel();
            return View(model);
        }
        public ActionResult DetailView(   detailsearchmodel model1)
        {
            List<int> i = JsonConvert.DeserializeObject<List<int>>(model1.Cat);
            LessonPlanModel model = new LessonPlanModel();
            if (model1.from != null)
            {
                var a = model1.from.Replace("-", "/");
                DateTime from = Convert.ToDateTime(a);
                var catlist = unitOfWork.LessonPlanCategoryRepository.Get();
                var list =
                    (from t in catlist
                     join q in i
                     on t.Id equals q
                     select t
                    ).ToList();
                model.LessonPlanDetailList = list.Select(x => new LessonPlanDetailModel() { CategoryId = x.Id, Category = x.Name, Id = x.Id, Sunday = from.ToShortDateString(), Monday = from.AddDays(1).ToShortDateString(), Tuesday = from.AddDays(2).ToShortDateString(), Wednesday = from.AddDays(3).ToShortDateString(), Thursday = from.AddDays(4).ToShortDateString() }).ToList();
                ViewBag.count = model.LessonPlanDetailList.Count - 1;
            }
            else
            {
                model.LessonPlanDetailList = unitOfWork.LessonPlanCategoryRepository.Get().Select(x => new LessonPlanDetailModel() { Category = x.Name, Id = x.Id }).ToList();

            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult PostLessonPlan(LessonPlanModel model)
        {
            var detaillist = model.LessonPlanDetailList;
            var master = model.LessonsPlanMasterModel;

            List<LessonsPlanMaster> masterlist = new List<LessonsPlanMaster>();
            List<FileSessionModel> list = ((List<FileSessionModel>)Session[WebUtil.File] == null) ? new List<FileSessionModel>() : (List<FileSessionModel>)Session[WebUtil.File];
            foreach (var i in detaillist)
            {
                foreach (var j in list)
                {
                    if (i.Id ==Convert.ToInt32(j.categoryId))
                    {

                        if(j.date.Equals(i.Sunday))
                        {
                          //  LessonPlanDetail ede = new LessonPlanDetail();
                            LessonsPlanMaster masterobj = new LessonsPlanMaster();
                            masterobj.CategoryId = Convert.ToInt32(j.categoryId);
                            masterobj.ClassId = j.ClasId;
                            masterobj.SubjectId = j.SubjectId;
                            masterobj.CreatedFor = Convert.ToDateTime(j.date);
                            masterobj.AssignmentComments = i.SundayComment;
                            masterobj.CreatedOn = DateTime.Now;
                            masterobj.CreatedBy = "Naveed";
                            foreach (var k in j.FileList)
                            {
                                LessonPlanDetail ede = new LessonPlanDetail();
                                ede.FileExtnsion = k.extension;
                                ede.ShowFileName = k.FileName;
                                ede.URL = k.URL;
                                ede.CreatedOn = DateTime.Now;
                                ede.CreatedBy = "Naveed";
                                masterobj.LessonPlanDetails.Add(ede);
                            }
                            masterlist.Add(masterobj);
                            unitOfWork.LessonsPlanMasterRepository.Insert(masterobj);
                            unitOfWork.Save();
                        }
                        if (j.date.Equals(i.Monday))
                        {
                            
                            LessonsPlanMaster masterobj = new LessonsPlanMaster();
                            masterobj.CategoryId = Convert.ToInt32(j.categoryId);
                            masterobj.ClassId = j.ClasId;
                            masterobj.SubjectId = j.SubjectId;
                            masterobj.CreatedFor = Convert.ToDateTime(j.date);
                            masterobj.AssignmentComments = i.MondayComment;
                            masterobj.CreatedOn = DateTime.Now;
                            masterobj.CreatedBy = "Naveed";
                            foreach (var k in j.FileList)
                            {
                                LessonPlanDetail ede = new LessonPlanDetail();
                                ede.FileExtnsion = k.extension;
                                ede.ShowFileName = k.FileName;
                                ede.URL = k.URL;
                                ede.CreatedOn = DateTime.Now;
                                ede.CreatedBy = "Naveed";
                                masterobj.LessonPlanDetails.Add(ede);
                            }
                            unitOfWork.LessonsPlanMasterRepository.Insert(masterobj);
                            unitOfWork.Save();
                        }
                        if (j.date.Equals(i.Tuesday))
                        {
                            
                            LessonsPlanMaster masterobj = new LessonsPlanMaster();
                            masterobj.CategoryId = Convert.ToInt32(j.categoryId);
                            masterobj.ClassId = j.ClasId;
                            masterobj.SubjectId = j.SubjectId;
                            masterobj.CreatedFor = Convert.ToDateTime(j.date);
                            masterobj.AssignmentComments = i.TuesdayComent;
                            masterobj.CreatedOn = DateTime.Now;
                            masterobj.CreatedBy = "Naveed";
                            foreach (var k in j.FileList)
                            {
                                LessonPlanDetail ede = new LessonPlanDetail();
                                ede.FileExtnsion = k.extension;
                                ede.ShowFileName = k.FileName;
                                ede.URL = k.URL;
                                ede.CreatedOn = DateTime.Now;
                                ede.CreatedBy = "Naveed";
                                masterobj.LessonPlanDetails.Add(ede);
                            }
                            masterlist.Add(masterobj);
                            unitOfWork.LessonsPlanMasterRepository.Insert(masterobj);
                            unitOfWork.Save();
                        }
                        if (j.date.Equals(i.Wednesday))
                        {
                            
                            LessonsPlanMaster masterobj = new LessonsPlanMaster();
                            masterobj.CategoryId = Convert.ToInt32(j.categoryId);
                            masterobj.ClassId = j.ClasId;
                            masterobj.SubjectId = j.SubjectId;
                            masterobj.CreatedFor = Convert.ToDateTime(j.date);
                            masterobj.AssignmentComments = i.WednesdayComment;
                            masterobj.CreatedOn = DateTime.Now;
                            masterobj.CreatedBy = "Naveed";
                            foreach (var k in j.FileList)
                            {
                                LessonPlanDetail ede = new LessonPlanDetail();
                                ede.FileExtnsion = k.extension;
                                ede.ShowFileName = k.FileName;
                                ede.URL = k.URL;
                                ede.CreatedOn = DateTime.Now;
                                ede.CreatedBy = "Naveed";
                                masterobj.LessonPlanDetails.Add(ede);
                            }
                            masterlist.Add(masterobj);
                            unitOfWork.LessonsPlanMasterRepository.Insert(masterobj);
                            unitOfWork.Save();
                        }
                        if (j.date.Equals(i.Thursday))
                        {
                            
                            LessonsPlanMaster masterobj = new LessonsPlanMaster();
                            masterobj.CategoryId = Convert.ToInt32(j.categoryId);
                            masterobj.ClassId = j.ClasId;
                            masterobj.SubjectId = j.SubjectId;
                            masterobj.CreatedFor = Convert.ToDateTime(j.date);
                            masterobj.AssignmentComments = i.ThursdayComment;
                            masterobj.CreatedOn = DateTime.Now;
                            masterobj.CreatedBy = "Naveed";
                            foreach (var k in j.FileList)
                            {
                                LessonPlanDetail ede = new LessonPlanDetail();
                                ede.FileExtnsion = k.extension;
                                ede.ShowFileName = k.FileName;
                                ede.URL = k.URL;
                                ede.CreatedOn = DateTime.Now;
                                ede.CreatedBy = "Naveed";
                                masterobj.LessonPlanDetails.Add(ede);
                            }
                            masterlist.Add(masterobj);
                            unitOfWork.LessonsPlanMasterRepository.Insert(masterobj);
                            unitOfWork.Save();
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult loadsubjects(int id)
        {
            var Subjects = unitOfWork.LessonPlanSubjectRepository.Get(x => x.ClassId == id).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.subjects = Subjects;
            return PartialView();
        }
        [HttpPost]
        public ActionResult fileupload()
        {

            string[] b = Request.Form.AllKeys;
            string Id = Request.Form.Get(b[0]);
            string clasId = Request.Form.Get(b[2]);
            string subjectId = Request.Form.Get(b[1]);
            string subject = unitOfWork.LessonPlanSubjectRepository.GetByID(Convert.ToInt32(subjectId)).Name;
            string Clas = unitOfWork.LessonPlanClassRepository.GetByID(Convert.ToInt32(clasId)).Name;
            string[] keys = Id.Split('_');
            string categoryId = keys[0];
            string category = keys[1];
            string date = keys[2];
            FileSessionModel obj = new FileSessionModel();
            obj.categoryId = categoryId;
            obj.CategoryName = category;
            obj.date = date;
            obj.Sub = subject;
            obj.SubjectId = Convert.ToInt32(subjectId);
            obj.ClasId = Convert.ToInt32(clasId);
            obj.Clas = Clas;
            List<FileSessionModel> list = ((List<FileSessionModel>)Session[WebUtil.File] == null) ? new List<FileSessionModel>() : (List<FileSessionModel>)Session[WebUtil.File];
            
            foreach (string fcName in Request.Files)
            {
                FileDetail filedetail = new FileDetail();
                HttpPostedFileBase file = Request.Files[fcName];
                //string[] nameandextension = file.FileName.Split('.');
                filedetail.FileName = file.FileName;
                filedetail.extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                filedetail.URL = DateTime.Now.Ticks.ToString();
                string path = "/Images/LessonPlan/" + category + "/" + Clas + "_" + subject + "/" + filedetail.URL + filedetail.extension;
                string createpath = "/Images/LessonPlan/" + category + "/" + Clas + "_" + subject;
                string dicrectory = Request.MapPath(createpath);
                Directory.CreateDirectory(dicrectory);
                string dicrectory2 = Request.MapPath(path);         
                file.SaveAs(dicrectory2);
                obj.FileList.Add(filedetail);
            }
            list.Add(obj);
            Session[WebUtil.File] = null;
            Session[WebUtil.File] = list;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult testing()
        {
            return View();
        }
    }
}