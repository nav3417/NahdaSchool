using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
  public  class detailsearchmodel
    {
        public int Id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int subject { get; set; }
        public int clas { get; set; }
        public int[] categories { get; set; }
        public string Cat { get; set; }
        public string[] stcategories { get; set; }
        public List<int> ilcategories { get; set; }
        public List<string> slcategories { get; set; }
    }
}
