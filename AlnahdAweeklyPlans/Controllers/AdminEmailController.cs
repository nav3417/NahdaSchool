using AlnahdAweeklyPlans.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Model;
using System.Web.Mvc;
using System.IO;

namespace AlnahdAweeklyPlans.Controllers
{
    public class AdminEmailController : Controller
    {
        // GET: AdminEmail
        Nahda_AttendanceEntities db;
        public AdminEmailController()
        {
            db = new Nahda_AttendanceEntities();
        }
        public ActionResult Index()
        {
            var data = db.StudentEmailMasters.ToList();
            return View();
        }
        public ActionResult loaddata()
        {
            var data = db.StudentEmailMasters.Select(x=> new StudentEmailModel() {Subject=x.Subject,Id=x.Id,NoFiles=x.StudentEmailFiles.Count,CreatedBy=x.CreatedBy,Details=x.StudentEmailDetails.Select(y=> new StudentEmailDetailModel() {Id=y.Id,Email=y.Email,Response=y.Response,CreatedOn=y.CreatedOn}).ToList()}).ToList();
            //var check=   data.Select(x => x.Details.Select(y => y.datestring == Convert.ToDateTime(y.CreatedOn).ToShortDateString())).ToList();
            foreach (var i in data)
            {
                foreach (var j in i.Details)
                {
                    j.datestring = Convert.ToDateTime(j.CreatedOn).ToShortDateString();
                }
            }
            return Json(new { draw = data, recordsFiltered = data.Count, recordsTotal = data.Count, data = data },JsonRequestBehavior.AllowGet);
        }
        public ActionResult ResendemailMaster(int id)
        {
            var master = db.StudentEmailMasters.Where(x => x.Id == id).FirstOrDefault();
            StudentEmailMaster data = new StudentEmailMaster();
            //data = master;
            data.AcademicYear = "not defined yet";//(db.AcademicYears.FirstOrDefault().Academic_Year_En==null)? "Not defined yet": db.AcademicYears.FirstOrDefault(x => x.isCurrent == true).Academic_Year_En;
            data.MailContent = master.MailContent;
            data.Subject = data.Subject;
            data.IsTemplate = false;
            data.Type = "Sent";
            data.CreatedOn = DateTime.Now;
            data.IsFile = master.IsFile;
            data.NoFiles = master.NoFiles;
            data.School = master.School;
            data.AcademicYear = master.AcademicYear;
            data.IsTemplate = master.IsTemplate;
            data.CreatedBy = (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;
            data.CreatedOn = DateTime.Now;
            foreach (var x in master.StudentEmailDetails.ToList())
            {
                StudentEmailDetail detail = new StudentEmailDetail();
                detail.Response = "Draft";
                detail.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                detail.CreatedOn = DateTime.Now;
                detail.Email = x.Email;
                data.StudentEmailDetails.Add(detail);
            }
            foreach (var file in master.StudentEmailFiles.ToList())
            {
                StudentEmailFile i = new StudentEmailFile();
                i.CreatedBy= (User.Identity.Name == "") ? "Naveed" : User.Identity.Name;
                i.CreatedOn = DateTime.Now;
                i.URL = file.URL;
                i.ShowFileName = file.ShowFileName;
                i.IsActive = true;
                data.StudentEmailFiles.Add(i);
            }
            db.StudentEmailMasters.Add(data);
            db.SaveChanges();
            var detaillist = data.StudentEmailDetails;
            var files = data.StudentEmailFiles;
            //deviding by
            int loop = 0;
            int skip = 0;
            int take = 2;
            string ToEmail = "rananaveedme@gmail.com";
            List<string> allfiles = new List<string>();
            loop = detaillist.Count / 2;
            foreach (var i in files)
            {
                string path = "/Images/" + "Mailes/" + i.URL;
                string dicrectory2 = Request.MapPath(path);
                allfiles.Add(dicrectory2);
            }
            for (int i = 0; i < loop + 1; i++)
            {
                var skiped = detaillist.Skip(skip).Take(take).Select(x => new sendemail() { Id = x.Id, Email = x.Email }).ToList();
                if (skiped.Count > 0)
                {
                    var Response = WebUtil.SchoolToParent(ToEmail, skiped, data.MailContent, data.Subject, allfiles) ? "Success" : "Failed";
                    foreach (var j in detaillist.Skip(skip).Take(take))
                    {
                        j.Response = Response;
                        db.SaveChanges();
                    }
                }
                skip = skip + 2;
            }
            return Json("hs",JsonRequestBehavior.AllowGet);
        }
        public ActionResult resendandupdateemaildetail(sendemail data)
        {
            var detail = db.StudentEmailDetails.Where(x => x.Id == data.Id).FirstOrDefault();
            var master = db.StudentEmailMasters.Where(x => x.Id == detail.MastId).FirstOrDefault();
            detail.Email = data.Email;
            detail.Response = EmailResponce.Draft;
            db.SaveChanges();
            var files = master.StudentEmailFiles;
            List<string> allfiles = new List<string>();
            foreach (var i in files)
            {
                string path = "/Images/" + "Mailes/" + i.URL;
                string dicrectory2 = Request.MapPath(path);
                allfiles.Add(dicrectory2);
            }
            List<sendemail> list = new List<sendemail>();
            list.Add(new sendemail() { Id =detail.Id, Email = detail.Email });
            string ToEmail = "rananaveedme@gmail.com";
            var Response = WebUtil.SchoolToParent(ToEmail, list, master.MailContent, master.Subject, allfiles) ? "Sent" : "Failed";
            detail.Response = Response;
            detail.CreatedOn = DateTime.Now;
            db.SaveChanges();
            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult resendemaildetail(sendemail data)
        {
            var detail = db.StudentEmailDetails.Where(x => x.Id == data.Id).FirstOrDefault();
            var master = db.StudentEmailMasters.Where(x => x.Id == detail.MastId).FirstOrDefault();
            detail.Response = EmailResponce.Draft;
            var files = master.StudentEmailFiles;
            List<string> allfiles = new List<string>();
            foreach (var i in files)
            {
                string path = "/Images/" + "Mailes/" + i.URL;
                string dicrectory2 = Request.MapPath(path);
                allfiles.Add(dicrectory2);
            }
            List<sendemail> list = new List<sendemail>();
            list.Add(new sendemail() { Id = detail.Id, Email = detail.Email });
            string ToEmail = "rananaveedme@gmail.com";
            var Response = WebUtil.SchoolToParent(ToEmail, list, master.MailContent, master.Subject, allfiles) ? "Sent" : "Failed";
            detail.Response = Response;
            detail.CreatedOn = DateTime.Now;
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmailDetail(int id)
        {
            var master = db.StudentEmailMasters.Where(x => x.Id == id).Select(x=>new StudentEmailModel() {Id=x.Id,Subject=x.Subject,Details=x.StudentEmailDetails.Select(y=> new StudentEmailDetailModel() {Id=y.Id,MastId=y.MastId,Email=y.Email,Response=y.Response}).ToList()}).FirstOrDefault();
            return PartialView(master);
        }
    }
}