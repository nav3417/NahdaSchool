using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
  public  class CategoryModel
    {
        public CategoryModel()
        {
            LessonPlanDetailModelList = new List<LessonPlanDetailModel>();
        }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string controller { get; set; }
        public List<LessonPlanDetailModel> LessonPlanDetailModelList { get; set; }
        public string URL { get; set; }
    }
}
