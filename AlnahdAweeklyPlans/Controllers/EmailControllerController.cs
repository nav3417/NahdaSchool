using AlnahdAweeklyPlans.Models;
using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers
{
    public class EmailControllerController : Controller
    {
        // GET: EmailController
        Nahda_AttendanceEntities db;
        public EmailControllerController()
        {
            db = new Nahda_AttendanceEntities();
        }
        public ActionResult Index()
        {

            SendEmailViewModel obj = new SendEmailViewModel();
            obj.CommunicationMstSelectList = db.CommunicationGroupMasters.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(obj);
        }
        public ActionResult loaddetail(string ids)
        {
            List<int> a = JsonConvert.DeserializeObject<List<int>>(ids);
            SendEmailViewModel obj = new SendEmailViewModel();
            obj.CommunicationDtlSelectList = (from m in db.CommunicationGroupDetails join q in a on m.MastId equals q select m).ToList().Select(x=> new SelectListItem() {Text=x.Email,Value=x.Id.ToString()+"/"+x.Email}).ToList();
            return PartialView(obj);
        }
        [ValidateInput(false)]
        public ActionResult Sendemails(SendEmailViewModel data)
        {
            StudentEmailMaster sem = new StudentEmailMaster();
            sem.AcademicYear = "not defined yet";//(db.AcademicYears.FirstOrDefault().Academic_Year_En==null)? "Not defined yet": db.AcademicYears.FirstOrDefault(x => x.isCurrent == true).Academic_Year_En;
            sem.MailContent = HttpUtility.HtmlDecode(data.EmailBody);
            sem.Subject = data.Subject;
            string s = Regex.Replace(data.EmailBody, "<.*?>", String.Empty);
            sem.IsTemplate =false;
            sem.Type = "Sent";
            sem.CreatedOn = DateTime.Now;
            sem.CreatedBy = "Naveed";
            HttpFileCollectionBase files = Request.Files;
            sem.IsFile = files.Count > 0 ? true : false;
            sem.NoFiles = files.Count;
            List<string> allfiles = new List<string>();
            for (var i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    StudentEmailFile filedetail = new StudentEmailFile();
                    filedetail.ShowFileName = file.FileName;
                    filedetail.FileExtnsion = file.ContentType;
                    filedetail.URL = DateTime.Now.Ticks.ToString();
                    filedetail.CreatedBy = "Naveed";
                    filedetail.CreatedOn = DateTime.Now;
                    filedetail.IsActive = true;
                    string path = "/Images/" + "Mailes/" + filedetail.URL;
                    string createpath = "/Images/" + "Mailes/";
                    string dicrectory = Request.MapPath(createpath);
                    Directory.CreateDirectory(dicrectory);
                    string dicrectory2 = Request.MapPath(path);
                    allfiles.Add(dicrectory2);
                    file.SaveAs(dicrectory2);
                    sem.StudentEmailFiles.Add(filedetail);
                }

            }
            foreach (var i in data.Indivisuald)
            {
                StudentEmailDetail sed = new StudentEmailDetail();
                string[] a = i.Split('/');
                string id = a[0];
                string email = a[1];
                sed.Response= WebUtil.SchoolToParent(email, data.EmailBody, data.Subject, allfiles)?"Sent":"Failed";
                sed.Email = email;
                sed.Date = DateTime.Now;
                sed.CreatedOn = DateTime.Now;
                sed.CreatedBy = "Naveed";
                sem.StudentEmailDetails.Add(sed);
            }
            db.StudentEmailMasters.Add(sem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}