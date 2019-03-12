using Database;
using Database.DataBaseCrud;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public  class LessonPlanMapping
    {

        IUnitOfWork unitOfWork;
        public LessonPlanMapping()
        {
            unitOfWork = new UnitOfWork();
        }
        public List<LessonsPlanMasterModel> ToList(IEnumerable<LessonsPlanMaster> LessonsPlans)
        {
            List<LessonsPlanMasterModel> list = new List<LessonsPlanMasterModel>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public List<CategoryModel> ToList(IEnumerable<LessonPlanCategory> LessonsPlans)
        {
            List<CategoryModel> list = new List<CategoryModel>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public LessonsPlanMasterModel ToObj(LessonsPlanMaster LessonsPlan)
        {
            if (LessonsPlan != null)
            {
                LessonsPlanMasterModel obj = new LessonsPlanMasterModel()
                {
                    Id = LessonsPlan.Id,
                    ApprovedBy=LessonsPlan.ApprovedBy,
                    Class=LessonsPlan.LessonPlanClass.Name,
                    CreatedBy= LessonsPlan.CreatedBy,
                    CreatedOn= LessonsPlan.CreatedOn,
                    HOSRemarks= LessonsPlan.HOSRemarks,
                    IsActive=LessonsPlan.IsActive,
                    IsApproved= LessonsPlan.IsApproved,
                    LessonPlanFrom= LessonsPlan.LessonPlanFrom,
                    LessonPlanTo= LessonsPlan.LessonPlanTo,
                    ModefiedBy= LessonsPlan.ModefiedBy,
                    ModefiedOn= LessonsPlan.ModefiedOn,
                    Subject= LessonsPlan.LessonPlanSubject.Name,
                    Category= LessonsPlan.LessonPlanCategory.Name,
                    Datefor=Convert.ToDateTime(LessonsPlan.CreatedFor).ToShortDateString(),
                    Day=Convert.ToString(Convert.ToDateTime(LessonsPlan.CreatedFor).DayOfWeek),
                    Teacher= LessonsPlan.CreatedBy,
                    TeacherRemarks= LessonsPlan.AssignmentComments,
                    LessonPlanDetailList= ToList(LessonsPlan.LessonPlanDetails)
                };
                return obj;
            }
            return null;
        }
        public List<LessonPlanClassModel> ToList(IEnumerable<LessonPlanClass> LessonsPlans)
        {
            List<LessonPlanClassModel> list = new List<LessonPlanClassModel>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public LessonPlanClassModel ToObj(LessonPlanClass LessonsPlan)
        {
            if (LessonsPlan != null)
            {
                LessonPlanClassModel obj = new LessonPlanClassModel()
                {
                    Id = LessonsPlan.Id,
                    Name= LessonsPlan.Name
                };
                return obj;
            }
            return null;
        }
        public CategoryModel ToObj(LessonPlanCategory LessonsPlan)
        {
            if (LessonsPlan != null)
            {
                CategoryModel obj = new CategoryModel()
                {
                    Id = LessonsPlan.Id,
                  Name= LessonsPlan.Name,
                  URL= LessonsPlan.ImageURL,
                 controller= LessonsPlan.ControllerName
                };
                return obj;
            }
            return null;
        }
        public LessonsPlanMaster ToObj(LessonsPlanMasterModel LessonsPlan)
        {
            if (LessonsPlan != null)
            {
                LessonsPlanMaster obj = new LessonsPlanMaster()
                {
                    Id = LessonsPlan.Id,
                    ApprovedBy = LessonsPlan.ApprovedBy,
                    Class = LessonsPlan.Class,
                    CreatedBy = LessonsPlan.CreatedBy,
                    CreatedOn = LessonsPlan.CreatedOn,
                    HOSRemarks = LessonsPlan.HOSRemarks,
                    IsActive = LessonsPlan.IsActive,
                    IsApproved = LessonsPlan.IsApproved,
                    LessonPlanFrom = LessonsPlan.LessonPlanFrom,
                    LessonPlanTo = LessonsPlan.LessonPlanTo,
                    ModefiedBy = LessonsPlan.ModefiedBy,
                    ModefiedOn = LessonsPlan.ModefiedOn,
                    Subject = LessonsPlan.Subject
                };
                return obj;
            }
            return null;
        }
        public LessonsPlanMaster ToObj(LessonsPlanMaster master, LessonsPlanMasterModel LessonsPlan)
        {
            if (master != null)
            {
                master.Id = LessonsPlan.Id;
                master.ApprovedBy = LessonsPlan.ApprovedBy;
                master.Class = LessonsPlan.Class;
                master.CreatedBy = LessonsPlan.CreatedBy;
                master.CreatedOn = LessonsPlan.CreatedOn;
                master.HOSRemarks = LessonsPlan.HOSRemarks;
                master.IsActive = LessonsPlan.IsActive;
                master.IsApproved = LessonsPlan.IsApproved;
                master.LessonPlanFrom = LessonsPlan.LessonPlanFrom;
                master.LessonPlanTo = LessonsPlan.LessonPlanTo;
                master.ModefiedBy = LessonsPlan.ModefiedBy;
                master.ModefiedOn = LessonsPlan.ModefiedOn;
                master.Subject = LessonsPlan.Subject;
                return master;
            }
            return null;
        }
        public List<LessonPlanDetail> ToList(IEnumerable<LessonPlanDetailModel> LessonsPlans)
        {
            List<LessonPlanDetail> list = new List<LessonPlanDetail>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public LessonPlanDetail ToObj(LessonPlanDetailModel LessonsPlan)
        {
            if (LessonsPlan != null)
            {
                LessonPlanDetail obj = new LessonPlanDetail()
                {
                    Id = LessonsPlan.Id,
                CreatedBy= LessonsPlan.CreatedBy,
                CreatedOn=Convert.ToDateTime(LessonsPlan.CreatedOn),
                FileExtnsion= LessonsPlan.FileExtnsion,
                URL= LessonsPlan.URL,
              //  LessonsPlanMaster= LessonsPlan.LessonsPlanMaster,
                Master_Id= LessonsPlan.MasterId,
                ModifiedBy= LessonsPlan.ModifiedBy,
                ModifiedOn= LessonsPlan.ModifiedOn,
                Reference= LessonsPlan.Reference,
                ShowFileName= LessonsPlan.ShowFileName,
                CategoryId= LessonsPlan.CategoryId
                };
                return obj;
            }
            return null;
        }

        public List<LessonPlanDetailModel> ToList(IEnumerable<LessonPlanDetail> LessonsPlans)
        {
            List<LessonPlanDetailModel> list = new List<LessonPlanDetailModel>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public LessonPlanDetailModel ToObj(LessonPlanDetail LessonsPlan)
        {
            if (LessonsPlan != null)
            {
                LessonPlanDetailModel obj = new LessonPlanDetailModel()
                {
                    Id = LessonsPlan.Id,
                    CreatedBy = LessonsPlan.CreatedBy,
                    CreatedOn =Convert.ToDateTime(LessonsPlan.CreatedOn).ToShortDateString(),
                    FileExtnsion = LessonsPlan.FileExtnsion,
                    URL = LessonsPlan.URL,
                    //  LessonsPlanMaster= LessonsPlan.LessonsPlanMaster,
                    ModifiedBy = LessonsPlan.ModifiedBy,
                    ModifiedOn = LessonsPlan.ModifiedOn,
                    Reference = LessonsPlan.Reference,
                    ShowFileName = LessonsPlan.ShowFileName,
                    Category= LessonsPlan.Category,
                    TeacherRemarks= LessonsPlan.TeacherRemarks,
                    Day=Convert.ToDateTime(LessonsPlan.CreatedFor).DayOfWeek.ToString(),
                    Subject= LessonsPlan.Subject,
                    Clas= LessonsPlan.Class,
                    MasterId= LessonsPlan.Master_Id,
                    HOSRemarks= LessonsPlan.HOSRemarks,
                    Teacher= LessonsPlan.CreatedBy,
                    IsApproved= LessonsPlan.IsApproved,
                    IsActive= LessonsPlan.IsActive,
                    Datefor =Convert.ToDateTime(LessonsPlan.CreatedFor).ToShortDateString()

                };
                return obj;
            }
            return null;
        }
        public LessonPlanDetail ToObj(LessonPlanDetail LessonPlan, LessonPlanDetailModel LessonsPlanmodel)
        {
            if (LessonPlan != null)
               {
                LessonPlan.Id = LessonsPlanmodel.Id;
                LessonPlan.CreatedBy = LessonsPlanmodel.CreatedBy;
                LessonPlan.CreatedOn =Convert.ToDateTime(LessonsPlanmodel.CreatedOn);
                LessonPlan.FileExtnsion = LessonsPlanmodel.FileExtnsion;
                LessonPlan.URL = LessonsPlanmodel.URL;
                //  LessonsPlanMaster= LessonsPlan.LessonsPlanMaster,
                LessonPlan.Master_Id = LessonsPlanmodel.MasterId;
                LessonPlan.ModifiedBy = LessonsPlanmodel.ModifiedBy;
                LessonPlan.ModifiedOn = LessonsPlanmodel.ModifiedOn;
                LessonPlan.Reference = LessonsPlanmodel.Reference;
                LessonPlan.ShowFileName = LessonsPlanmodel.ShowFileName;
                LessonPlan.CategoryId = LessonsPlanmodel.CategoryId;
                return LessonPlan;
                }
                return null;
        }
    }
}
