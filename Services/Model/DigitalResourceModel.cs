using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
   public class DigitalResourceModel
    {
        public DigitalResourceModel()
        {
            this.MasterModelList = new List<DigitalResourceMasterModel>();
            CategoryGeneralModelList = new List<CategoryGeneralModel>();
            //LessonsPlanMasterModelList = new List<LessonsPlanMasterModel>();
        }
        public int Id { get; set; }
        public virtual List<DigitalResourceMasterModel> MasterModelList { get; set; }
        public virtual List<CategoryGeneralModel> CategoryGeneralModelList { get; set; }
        public CategoryModel CategoryModel { get; set; }
    }
}
