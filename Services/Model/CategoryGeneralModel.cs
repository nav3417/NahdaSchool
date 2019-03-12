using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
  public  class CategoryGeneralModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int parentId { get; set; }
        public int TotalSubList { get; set; }
    }
}
