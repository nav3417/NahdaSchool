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
  public  class DigitalResourcesMapping
    {
        IUnitOfWork unitOfWork;
        LessonPlanMapping service;
        public DigitalResourcesMapping()
        {
            unitOfWork = new UnitOfWork();
            service = new LessonPlanMapping();
        }
        public List<DigitalResourceMasterModel> ToList(IEnumerable<DigitalResourceMaster> LessonsPlans)
        {
            List<DigitalResourceMasterModel> list = new List<DigitalResourceMasterModel>();
            if (LessonsPlans.Count() > 0)
            {
                foreach (var i in LessonsPlans)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public DigitalResourceMasterModel ToObj(DigitalResourceMaster DigitalResource)
        {
            if (DigitalResource != null)
            {
                DigitalResourceMasterModel obj = new DigitalResourceMasterModel()
                {
                    Id = DigitalResource.Id,
                    Category= DigitalResource.LessonPlanCategory.Name,
                    CategoryId= DigitalResource.Id,
                    CreatedBy= DigitalResource.CreatedBy,
                    CreatedOn= DigitalResource.CreatedOn,
                    DescriptionLong= DigitalResource.DescriptionLong,
                    DescriptionShort= DigitalResource.DescriptionShort,
                    DigitalResourceFiles= ToList(DigitalResource.DigitalResourceFiles),
                    DigitalResourceClasses=ToList(DigitalResource.DigitalResourceClasses),
                    Fromstring=Convert.ToDateTime(DigitalResource.From).ToShortDateString(),
                    LastModifiedBy= DigitalResource.LastModifiedBy,
                    LastModifiedOn= DigitalResource.LastModifiedOn,
                    From= DigitalResource.From,
                    To= DigitalResource.To,
                    Tostring = Convert.ToDateTime(DigitalResource.To).ToShortDateString(),
                    title= DigitalResource.title
                };
                return obj;
            }
            return null;
        }
        public List<DigitalResourceFileModel> ToList(IEnumerable<DigitalResourceFile> DigitalResource)
        {
            List<DigitalResourceFileModel> list = new List<DigitalResourceFileModel>();
            if (DigitalResource.Count() > 0)
            {
                foreach (var i in DigitalResource)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public DigitalResourceFileModel ToObj(DigitalResourceFile DigitalResource)
        {
            if (DigitalResource != null)
            {
                DigitalResourceFileModel obj = new DigitalResourceFileModel()
                {
                    Id = DigitalResource.Id,
                   CreatedBy= DigitalResource.CreatedBy,
                   CreatedOn= DigitalResource.CreatedOn,
                   LastModifiedOn= DigitalResource.LastModifiedOn,
                   FileExtnsion= DigitalResource.FileExtnsion,
                   LastModifiedBy= DigitalResource.LastModifiedBy,
                   MasterId= DigitalResource.MasterId,
                   ShowFileName= DigitalResource.ShowFileName,
                   URL= DigitalResource.URL
                };
                return obj;
            }
            return null;
        }
        public List<DigitalResourceClassModel> ToList(IEnumerable<DigitalResourceClass> DigitalResource)
        {
            List<DigitalResourceClassModel> list = new List<DigitalResourceClassModel>();
            if (DigitalResource.Count() > 0)
            {
                foreach (var i in DigitalResource)
                {
                    list.Add(ToObj(i));
                }
            }
            return list;
        }
        public DigitalResourceClassModel ToObj(DigitalResourceClass DigitalResource)
        {
            if (DigitalResource != null)
            {
                DigitalResourceClassModel obj = new DigitalResourceClassModel()
                {
                    Id = DigitalResource.Id,
                    ClassId= DigitalResource.ClassId,
                    MasterId= DigitalResource.MasterId,
                    CreatedBy= DigitalResource.CreatedBy,
                    CreatedOn= DigitalResource.CreatedOn,
                    LessonPlanClass= service.ToObj(DigitalResource.LessonPlanClass)
                };
                return obj;
            }
            return null;
        }
    }
}
