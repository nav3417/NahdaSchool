using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
   public class LessonPlanModel
    {
        public LessonPlanModel()
        {
            this.CategoryList = new List<CategoryModel>();
            LessonPlanDetailList = new List<LessonPlanDetailModel>();
            LessonsPlanMasterModelList = new List<LessonsPlanMasterModel>();
        }
        public int Id { get; set; }
        public LessonsPlanMasterModel LessonsPlanMasterModel { get; set; }
        public List< LessonsPlanMasterModel > LessonsPlanMasterModelList { get; set; }
        public virtual List<CategoryModel> CategoryList { get; set; }
        public virtual List<LessonPlanDetailModel> LessonPlanDetailList { get; set; }
    }
}
