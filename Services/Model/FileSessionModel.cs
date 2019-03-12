using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
  public  class FileSessionModel
    {
      public FileSessionModel()
        {
            FileList = new List<FileDetail>();
        }
        //public string FileName { get; set; }
        //public string extension { get; set; }
        //public string URL { get; set; }
        public string date { get; set; }
        public string CategoryName { get; set; }
        public string categoryId { get; set; }
        public int? SubjectId { get; set; }
        public int? ClasId { get; set; }
        public string Sub { get; set; }
        public string Clas { get; set; }
        public List<FileDetail> FileList { get; set; }
    }
}
