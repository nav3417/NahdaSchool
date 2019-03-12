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
  public  class LessonPlanService
    {

        IUnitOfWork UnitOfWork;
        LessonPlanMapping LessonPlanMapping;
        public LessonPlanService()
        {
            LessonPlanMapping = new LessonPlanMapping();
            UnitOfWork = new UnitOfWork();
        }
        public List<LessonsPlanMasterModel> GetLessonsPlanMaster()
        {
            return LessonPlanMapping.ToList(UnitOfWork.LessonsPlanMasterRepository.Get().ToList());
        }
        public LessonsPlanMasterModel GetById(int id)
        {
            return LessonPlanMapping.ToObj(UnitOfWork.LessonsPlanMasterRepository.GetByID(id));
        }
        public bool AddUser(LessonsPlanMasterModel LessonsPlan)
        {
            try
            {
                if (LessonsPlan.Id > 0)
                {
                    var get = UnitOfWork.LessonsPlanMasterRepository.GetByID(LessonsPlan.Id);
                    UnitOfWork.LessonsPlanMasterRepository.Update(LessonPlanMapping.ToObj(get, LessonsPlan));
                    UnitOfWork.Save();
                    return true;
                }
                else
                {
                    UnitOfWork.LessonsPlanMasterRepository.Insert(LessonPlanMapping.ToObj(LessonsPlan));
                    UnitOfWork.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteUser(int id)
        {
            try
            {
                UnitOfWork.LessonsPlanMasterRepository.Delete(id);
                UnitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
