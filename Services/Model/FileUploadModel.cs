using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Model
{
  public  class FileUploadModel
    {
        public string Id { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}
