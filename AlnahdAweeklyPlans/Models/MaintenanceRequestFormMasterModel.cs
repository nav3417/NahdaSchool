using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class MaintenanceRequestFormMasterModel
    {
        public MaintenanceRequestFormMasterModel()
        {
            Maintenance_Approvals = new List<Maintenance_Approval>();
            Maintenance_IssuesDetails = new List<Maintenance_IssuesDetail>();
            Maintenance_TechnicianRequesteds = new List<Maintenance_TechnicianRequested>();
            Maintenance_TaskProcesss = new List<Maintenance_TaskProcess>();
            Maintenance_Approvals = new List<Maintenance_Approval>();
            RequestByListItems = new List<SelectListItem>();
            SchoolListItems = new List<SelectListItem>();
            LocationListItems = new List<SelectListItem>();
            issuesdetaiilist = new List<Maintenance_IssuesDetailModel>()
            { new Maintenance_IssuesDetailModel() {Description="Description",Equipment="Equipment"} };
            InsideRoomLocationSelectList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string RequestBy { get; set; }
        public string RequestOn { get; set; }
        public string CellNo { get; set; }
        public string School { get; set; }
        public string Location { get; set; }
        public string FlatNo { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public Nullable<int> RecievedBy { get; set; }
        public string RecievedOn { get; set; }
        public string ComplitionDate { get; set; }
        public string TechnicianName { get; set; }
        public string PDdate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public string detailist { get; set; }
        public string technicianlist { get; set; }
        public string approvallist { get; set; }
        public string taskslist { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsDeletedParmanent { get; set; }
        public List<SelectListItem> RequestByListItems { get; set; }
        public List<SelectListItem> SchoolListItems { get; set; }
        public List<SelectListItem> LocationListItems { get; set; }
        public int detailLoopcount { get; set; }
        public MaintenanceRequestFormMaster MaintenanceRequestFormMaster { get; set; }
        public List<Maintenance_IssuesDetailModel> issuesdetaiilist { get; set; }
        public virtual List<Maintenance_Approval> Maintenance_Approvals { get; set; }
        public virtual List<Maintenance_IssuesDetail> Maintenance_IssuesDetails { get; set; }
        public virtual List<Maintenance_TechnicianRequested> Maintenance_TechnicianRequesteds { get; set; }
        public virtual List<Maintenance_TaskProcess> Maintenance_TaskProcesss { get; set; }
        public List<SelectListItem> InsideRoomLocationSelectList { get; set; }
    }
}