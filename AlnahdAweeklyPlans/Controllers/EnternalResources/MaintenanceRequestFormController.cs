using AlnahdAweeklyPlans.Models;
using Database;
using Database.DataBaseCrud;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Controllers.EnternalResources
{
    public class MaintenanceRequestFormController : Controller
    {
        // GET: MaintenanceRequestForm
        Nahda_AttendanceEntities db;
        IUnitOfWork unitofwork;

        public MaintenanceRequestFormController()
        {
            db = new Nahda_AttendanceEntities();
            unitofwork = new UnitOfWork();
        }
        // GET: StudentFeedBackReport
        public ActionResult Index()
        {
            var load = unitofwork.MaintenanceRequestFormMasterRepository.Get().ToList();
            var a =(from m in  load join q in db.HREmployeeMsts on m.RequestBy equals q.Id 
                   join n in db.Accounts on q.AccountId equals n.Id
                   join o in db.md_type on m.Location equals o.Id
                    select new MaintenanceRequestFormMasterModel()
                     {
                         Id = m.Id,
                         ApprovedBy=m.ApprovedBy,
                         ApprovedOn=Convert.ToDateTime(m.ApprovedOn).ToShortDateString(),
                         CreatedOn= Convert.ToDateTime(m.CreatedOn).ToShortDateString(),
                         CellNo =m.CellNo,
                         CreatedBy=m.CreatedBy,
                         FlatNo=m.FlatNo,
                         Location=o.Name,
                         RequestBy=n.AccountName,
                         RequestOn= Convert.ToDateTime(m.RequestOn).ToShortDateString(),
                         School =m.School                         
                     }).ToList();
            
            return View(a);
        }
        // GET: StudentFeedBackReport/Create
        public ActionResult Create(int? id)
        {
            Session["id"] = id;
            MaintenanceRequestFormMasterModel model = new MaintenanceRequestFormMasterModel();
            model.RequestByListItems = LoadRequestBy();
            model.SchoolListItems = LoadSchools();
            model.LocationListItems = LoadLocations();
            int issuescountcount = 0;
            int approvalcount = 0;
            int techniancount = 0;
            int taskcount = 0;
            model.InsideRoomLocationSelectList = LoadInsideRoomLocations();
            if (id > 0)
            {
                 model.MaintenanceRequestFormMaster = db.MaintenanceRequestFormMasters.Where(x => x.Id == id).FirstOrDefault();
                 issuescountcount = model.MaintenanceRequestFormMaster.Maintenance_IssuesDetail.Count;
                 approvalcount = model.MaintenanceRequestFormMaster.Maintenance_Approval.Count;
                 techniancount = model.MaintenanceRequestFormMaster.Maintenance_TechnicianRequested.Count;
                 taskcount = model.MaintenanceRequestFormMaster.Maintenance_TaskProcess.Count;
            }
                Maintenance_IssuesDetail m = new Maintenance_IssuesDetail();
                m.Equipment = "Equipment";
                m.Description = "Description";
                //model.Maintenance_IssuesDetails.Add(m);

                Maintenance_Approval n = new Maintenance_Approval();
                n.TechnicianName = "Technician Name";
                n.Description = "Description";
                n.InspectedDate = DateTime.Now;
                //model.Maintenance_Approvals.Add(n);

                Maintenance_TechnicianRequested o = new Maintenance_TechnicianRequested();
                o.ItemDescription = "ItemDescription";
                o.Quantity = "Quantity";
                o.Material = "Material";
                //model.Maintenance_TechnicianRequesteds.Add(o);

                Maintenance_TaskProcess q = new Maintenance_TaskProcess();
                q.Employee = "Employee";
                q.Description = "Description";
                q.Date = DateTime.Now;

               List<Maintenance_Approval> mlist = new List<Maintenance_Approval>();
               mlist.Add(n);
               List<Maintenance_TechnicianRequested> tlist = new List<Maintenance_TechnicianRequested>();
               tlist.Add(o);
               List<Maintenance_IssuesDetail> isuelist = new List<Maintenance_IssuesDetail>();
               isuelist.Add(m);
               List<Maintenance_TaskProcess> qlist = new List<Maintenance_TaskProcess>();
               qlist.Add(q);


               model.Maintenance_IssuesDetails = issuescountcount == 0 ? isuelist : model.MaintenanceRequestFormMaster.Maintenance_IssuesDetail.ToList();
               model.Maintenance_Approvals = approvalcount == 0 ? mlist : model.MaintenanceRequestFormMaster.Maintenance_Approval.ToList();
               model.Maintenance_TechnicianRequesteds = techniancount == 0 ? tlist : model.MaintenanceRequestFormMaster.Maintenance_TechnicianRequested.ToList();
               model.Maintenance_TaskProcesss = taskcount == 0 ? qlist: model.MaintenanceRequestFormMaster.Maintenance_TaskProcess.ToList();
            return View(model);
        }
        public List<SelectListItem> LoadRequestBy()
        {
            return (from m in db.Accounts
                    join q in db.HREmployeeMsts on m.Id equals q.AccountId
                    select new SelectListItem() { Text = m.AccountName, Value = q.Id.ToString() }).ToList();
        }
        public List<SelectListItem> LoadLocations()
        {
            return db.md_type.Where(x=>x.type.Equals("Maintenance_Loc_Master")).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }
        public List<SelectListItem> LoadInsideRoomLocations()
        {
            return db.md_type.Where(x => x.type.Equals("Maintenance_Loc_Detail")).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }
        public List<SelectListItem> LoadSchools()
        {
            List<SelectListItem> school = new List<SelectListItem>();
            school.Add(new SelectListItem() { Text = "G", Value = "G" });
            school.Add(new SelectListItem() { Text = "B", Value = "B" });
            return school;
        }
        // POST: StudentFeedBackReport/Create
        [HttpPost]
        public ActionResult Create(MaintenanceRequestFormMasterModel collection)
        {
            if(collection.detailist!=null)
            {
                var IssuesDetail = JsonConvert.DeserializeObject<List<Maintenance_IssuesDetail>>(collection.detailist);
                collection.Maintenance_IssuesDetails = IssuesDetail;
            }
            if(collection.technicianlist!=null)
            {
                var TechnicianRequested = JsonConvert.DeserializeObject<List<Maintenance_TechnicianRequested>>(collection.technicianlist);
                collection.Maintenance_TechnicianRequesteds = TechnicianRequested;
            }
            if (collection.approvallist != null)
            {
                var Approvallist = JsonConvert.DeserializeObject<List<Maintenance_Approval>>(collection.approvallist);
                collection.Maintenance_Approvals = Approvallist;
            }
            if (collection.taskslist != null)
            {
                var taskslist = JsonConvert.DeserializeObject<List<Maintenance_TaskProcess>>(collection.taskslist);
                collection.Maintenance_TaskProcesss = taskslist;
            }
            try
            {
                //var empid = db.Accounts.Where(x => x.Id == collection.MaintenanceRequestFormMaster.RequestBy).FirstOrDefault();
                //collection.MaintenanceRequestFormMaster.RequestBy =unitofwork.HREmployeeMstRepository.Get(x=>x.AccountId==empid.Id).FirstOrDefault().Id;
               
                if (collection.MaintenanceRequestFormMaster.Id > 0)
                {
                    int itsid = collection.MaintenanceRequestFormMaster.Id;
                    var data = unitofwork.MaintenanceRequestFormMasterRepository.GetByID(itsid);
                    List<Maintenance_IssuesDetail> idl = new List<Maintenance_IssuesDetail>();
                    List<Maintenance_Approval> apl = new List<Maintenance_Approval>();
                    List<Maintenance_TaskProcess> tpl = new List<Maintenance_TaskProcess>();
                    List<Maintenance_TechnicianRequested> trl = new List<Maintenance_TechnicianRequested>();
                    foreach (var j in collection.Maintenance_IssuesDetails)
                    {

                        foreach (var i in data.Maintenance_IssuesDetail.ToList())
                        {
                            if(j.Id==i.Id)
                            {
                                var load = unitofwork.IssuesDetailRepository.GetByID(i.Id);
                                load.Equipment = j.Equipment;
                                load.Description = j.Description;
                                load.InsideRoomLocation = load.InsideRoomLocation;
                                unitofwork.Save();
                            }
                            if(j.Id==0)
                            {
                                idl.Add(j);
                            }
                        }
                     }
                    foreach (var j in collection.Maintenance_Approvals)
                    {

                        foreach (var i in data.Maintenance_Approval.ToList())
                        {
                            if (j.Id == i.Id)
                            {
                                var load = unitofwork.MaintenanceApprovalRepository.GetByID(i.Id);
                                load.InspectedDate = j.InspectedDate;
                                load.Description = j.Description;
                                load.TechnicianName = load.TechnicianName;
                                unitofwork.Save();
                            }
                            if (j.Id == 0)
                            {
                                apl.Add(j);
                            }
                        }
                    }
                    foreach (var j in collection.Maintenance_TaskProcesss)
                    {

                        foreach (var i in data.Maintenance_TaskProcess.ToList())
                        {
                            if (j.Id == i.Id)
                            {
                                var load = unitofwork.TaskProcessRepository.GetByID(i.Id);
                                load.Employee = j.Employee;
                                load.Description = j.Description;
                                load.Date = load.Date;
                                unitofwork.Save();
                            }
                            if (j.Id == 0)
                            {
                                tpl.Add(j);
                            }
                        }
                    }
                    foreach (var j in collection.Maintenance_TechnicianRequesteds)
                    {

                        foreach (var i in data.Maintenance_TechnicianRequested.ToList())
                        {
                            if (j.Id == i.Id)
                            {
                                var load = unitofwork.TechnicianRequestedRepository.GetByID(i.Id);
                                load.ItemDescription = j.ItemDescription;
                                load.Material = j.Material;
                                load.Quantity = load.Quantity;
                                unitofwork.Save();
                            }
                            if (j.Id == 0)
                            {
                                trl.Add(j);
                            }
                        }
                    }
                    data.Maintenance_Approval = apl;
                    data.Maintenance_IssuesDetail = idl;
                    data.Maintenance_TaskProcess = tpl;
                    data.Maintenance_TechnicianRequested= trl;
                    collection.MaintenanceRequestFormMaster.ModifiedOn = DateTime.Now;
                    collection.MaintenanceRequestFormMaster.ModifiedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.MaintenanceRequestFormMasterRepository.Update(data);
                }
                else
                {
                    collection.MaintenanceRequestFormMaster.CreatedOn = DateTime.Now;
                    collection.MaintenanceRequestFormMaster.RequestOn = DateTime.Now;
                    collection.MaintenanceRequestFormMaster.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    collection.MaintenanceRequestFormMaster.ApprovedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    //collection.StudentFeedBackReport.CreatedOn = DateTime.Now;
                    //collection.StudentFeedBackReport.CreatedBy = User.Identity.Name == "" ? "Naveed" : User.Identity.Name;
                    unitofwork.MaintenanceRequestFormMasterRepository.Insert(collection.MaintenanceRequestFormMaster);
                }
                unitofwork.Save();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                int id = (int)Session["id"];
                MaintenanceRequestFormMasterModel model = new MaintenanceRequestFormMasterModel();
                model.RequestByListItems = LoadRequestBy();
                model.SchoolListItems = LoadSchools();
                model.LocationListItems = LoadLocations();
                if (id > 0)
                {
                    model.MaintenanceRequestFormMaster = db.MaintenanceRequestFormMasters.Where(x => x.Id == id).FirstOrDefault();
                }
                return View(model);
            }
        }


    }
}