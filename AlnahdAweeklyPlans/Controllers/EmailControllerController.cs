using AlnahdAweeklyPlans.Models;
using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            ResponceModel rm = new ResponceModel();
            var list = db.StudentEmailMasters.Where(x => x.Type.Equals("Inbox"));
            obj.StudentEmailMasterList = list.ToList();
            rm.Inboxcount = list.Count();
            rm.Draftcount = db.StudentEmailMasters.Where(c => c.Type.Equals("Draft")).Count();
            rm.Sentcount = db.StudentEmailMasters.Where(x => x.Type.Equals("Sent")).Count();
            obj.responce = rm;
            return View(obj);
        }
        public ActionResult loaddetail(string ids)
        {
            List<int> a = JsonConvert.DeserializeObject<List<int>>(ids);
            SendEmailViewModel obj = new SendEmailViewModel();
            obj.CommunicationDtlSelectList = (from m in db.CommunicationGroupDetails join q in a on m.MastId equals q select m).ToList().Select(x => new SelectListItem() { Text = x.Email, Value = x.Id.ToString() + "/" + x.Email }).ToList();
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
            sem.IsTemplate = false;
            sem.Type = "Sent";
            sem.CreatedOn = DateTime.Now;
            sem.CreatedBy = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;
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
                sed.Response = "Draft"; //WebUtil.SchoolToParent(email, data.EmailBody, data.Subject, allfiles) ? "Sent" : "Failed";
                sed.Email = email;
                sed.Date = DateTime.Now;
                sed.CreatedOn = DateTime.Now;
                sed.CreatedBy = "Naveed";
                sem.StudentEmailDetails.Add(sed);
            }
            db.StudentEmailMasters.Add(sem);
            db.SaveChanges();
            var detaillist=db.StudentEmailDetails.Where(x=>x.MastId==sem.Id).ToList();
            //deviding by
            int loop = 0;
            int skip = 0;
            int take = 2;
            string ToEmail = "rananaveedme@gmail.com";
            loop = detaillist.Count / 2;

            for (int i = 0; i < loop+1; i++)
            {
                var skiped = detaillist.Skip(skip).Take(take).Select(x=> new sendemail() {Id=x.Id,Email=x.Email}).ToList();
                if(skiped.Count>0)
                {
                    var Response = WebUtil.SchoolToParent(ToEmail, skiped, data.EmailBody, data.Subject, allfiles) ? "Success" : "Failed";
                    foreach (var j in detaillist.Skip(skip).Take(take))
                    {
                        j.Response = Response;
                        db.SaveChanges();
                    }
                }
                skip = skip + 2;
            }
            
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]
        public ActionResult draftemails(SendEmailViewModel data)
        {
            StudentEmailMaster sem;
            if (data.Id != 0)
            {
                sem = db.StudentEmailMasters.Find(data.Id);
            }
            else
            {
                sem = new StudentEmailMaster();
            }
            sem.AcademicYear = "not defined yet";//(db.AcademicYears.FirstOrDefault().Academic_Year_En==null)? "Not defined yet": db.AcademicYears.FirstOrDefault(x => x.isCurrent == true).Academic_Year_En;
            sem.MailContent = HttpUtility.HtmlDecode(data.EmailBody);
            sem.Subject = data.Subject;
            //  string s = Regex.Replace(data.EmailBody, "<.*?>", String.Empty);
            sem.IsTemplate = false;
            sem.Type = "Draft";
            sem.CreatedOn = DateTime.Now;
            sem.CreatedBy = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;
            //HttpFileCollectionBase files = Request.Files;
            //sem.IsFile = files.Count > 0 ? true : false;
            //sem.NoFiles = files.Count;
            //List<string> allfiles = new List<string>();
            //foreach (string fcName in Request.Files)
            //{
            //    HttpPostedFileBase file = files[fcName];
            //    if (!string.IsNullOrEmpty(file.FileName))
            //    {
            //        StudentEmailFile filedetail = new StudentEmailFile();
            //        filedetail.ShowFileName = file.FileName;
            //        filedetail.FileExtnsion = file.ContentType;
            //        filedetail.URL = DateTime.Now.Ticks.ToString();
            //        filedetail.CreatedBy = "Naveed";
            //        filedetail.CreatedOn = DateTime.Now;
            //        filedetail.IsActive = true;
            //        string path = "/Images/" + "Mailes/" + filedetail.URL;
            //        string createpath = "/Images/" + "Mailes/";
            //        string dicrectory = Request.MapPath(createpath);
            //        Directory.CreateDirectory(dicrectory);
            //        string dicrectory2 = Request.MapPath(path);
            //        allfiles.Add(dicrectory2);
            //        file.SaveAs(dicrectory2);
            //        sem.StudentEmailFiles.Add(filedetail);
            //    }

            //}
            //if (data.Indivisuald.Count > 0)
            //{
            //    foreach (var i in data.Indivisuald)
            //    {
            //        string[] a = i.Split('/');
            //        string id = a[0];
            //        string email = a[1];
            //        StudentEmailDetail sed = new StudentEmailDetail();
            //        // sed.Response = WebUtil.SchoolToParent(email, data.EmailBody, data.Subject, allfiles) ? "Sent" : "Failed";
            //        sed.Email = email;
            //        sed.Date = DateTime.Now;
            //        sed.CreatedOn = DateTime.Now;
            //        sed.CreatedBy = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;
            //        if (sem.StudentEmailDetails.Count > 0)
            //        {
            //            var check = sem.StudentEmailDetails.Where(x => x.Email.Equals(email)).FirstOrDefault();
            //            if (check == null)
            //            {
            //                sem.StudentEmailDetails.Add(sed);
            //                db.StudentEmailMasters.Add(sem);
            //                db.SaveChanges();
            //            }
            //        }
            //        else
            //        {
            //            sem.StudentEmailDetails.Add(sed);
            //            db.StudentEmailMasters.Add(sem);
            //            db.SaveChanges();
            //        }

            //    }

            //}
            if (data.Id == 0)
            {
                sem = db.StudentEmailMasters.Add(sem);
                db.SaveChanges();
            }
            else
            {
                db.SaveChanges();
            }
            ResponceModel rm = new ResponceModel();
            rm.Draftcount = db.StudentEmailMasters.Where(x => x.Type.Equals("Draft")).Count();
            rm.Id = sem.Id;
            return Json(rm, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult updateinboxcount()
        //{
        //    return int;
        //}
        //public ActionResult updateinboxcount()
        //{
        //    return int;
        //}
        public ActionResult GetMailsByType(string type)
        {
            SendEmailViewModel data = new SendEmailViewModel();
            string username = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;
            if (type.Equals("temp"))
            {
                data.StudentEmailMasterList = db.StudentEmailMasters.Where(x => x.IsTemplate==true && x.CreatedBy.Equals(username)).ToList();
            }
            else
            {
                data.StudentEmailMasterList = db.StudentEmailMasters.Where(x => x.Type.Equals(type) && x.CreatedBy.Equals(username)).ToList();
            }
            return PartialView(data);
        }
        public ActionResult emailformat(int? id)
        {
            SendEmailViewModel model = new SendEmailViewModel();
            if (id != null)
            {
                model = db.StudentEmailMasters.Where(x => x.Id == id).Select(c => new SendEmailViewModel() { Id = c.Id, Subject = c.Subject, EmailBody = c.MailContent }).FirstOrDefault();
            }
            model.CommunicationMstSelectList = db.CommunicationGroupMasters.Select(x => new SelectListItem() { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return PartialView(model);
        }
        [ValidateInput(false)]
        public ActionResult EmailTemplatePost(SendEmailViewModel data)
        {
            StudentEmailMaster sem;
            if (data.Id != 0)
            {
                sem = db.StudentEmailMasters.Find(data.Id);
            }
            else
            {
                sem = new StudentEmailMaster();
            }
            sem.AcademicYear = "not defined yet";//(db.AcademicYears.FirstOrDefault().Academic_Year_En==null)? "Not defined yet": db.AcademicYears.FirstOrDefault(x => x.isCurrent == true).Academic_Year_En;
            sem.MailContent = HttpUtility.HtmlDecode(data.EmailBody);
            sem.Subject = data.Subject;
            //  string s = Regex.Replace(data.EmailBody, "<.*?>", String.Empty);
            sem.IsTemplate = true;
            sem.Type = "Draft";
            sem.CreatedOn = DateTime.Now;
            sem.CreatedBy = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;

            if (data.Id == 0)
            {
                sem = db.StudentEmailMasters.Add(sem);
                db.SaveChanges();
            }
            else
            {
                db.SaveChanges();
            }
            ResponceModel rm = new ResponceModel();
            rm.Draftcount = db.StudentEmailMasters.Where(x => x.Type.Equals("Draft")).Count();
            rm.Id = sem.Id;
            return RedirectToAction("Index");
        }
        public ActionResult sendemailt(SendEmailViewModel data)
        {
            //StudentEmailMaster sem;
            //if (data.Id != 0)
            //{
            //    sem = db.StudentEmailMasters.Find(data.Id);
            //}
            //else
            //{
            //    sem = new StudentEmailMaster();
            //}
            //sem.AcademicYear = "not defined yet";//(db.AcademicYears.FirstOrDefault().Academic_Year_En==null)? "Not defined yet": db.AcademicYears.FirstOrDefault(x => x.isCurrent == true).Academic_Year_En;
            //sem.MailContent = HttpUtility.HtmlDecode(data.EmailBody);
            //sem.Subject = data.Subject;
            ////  string s = Regex.Replace(data.EmailBody, "<.*?>", String.Empty);
            //sem.IsTemplate = true;
            //sem.Type = "Draft";
            //sem.CreatedOn = DateTime.Now;
            //sem.CreatedBy = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;

            //if (data.Id == 0)
            //{
            //    sem = db.StudentEmailMasters.Add(sem);
            //    db.SaveChanges();
            //}
            //else
            //{
            //    db.SaveChanges();
            //}
            //ResponceModel rm = new ResponceModel();
            //rm.Draftcount = db.StudentEmailMasters.Where(x => x.Type.Equals("Draft")).Count();
            //rm.Id = sem.Id;
            return RedirectToAction("Index");
        }
    }
}