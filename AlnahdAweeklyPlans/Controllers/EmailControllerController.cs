using AlnahdAweeklyPlans.Models;
using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            obj.CommunicationDtlSelectList = (from m in db.CommunicationGroupDetails join q in a on m.MastId equals q select m).ToList().Select(x=> new SelectListItem() {Text=x.Email,Value=x.Id.ToString()}).ToList();
            return PartialView(obj);
        }
        public ActionResult Sendemails(SendEmailViewModel data)
        {
            return RedirectToAction("Index");
        }

        
    }
}