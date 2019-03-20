using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class CommunicationModel
    {
        public CommunicationModel()
        {
            SelectListItemSChool = new List<SelectListItem>();
            SelectListItemDepartment = new List<SelectListItem>();
            SelectListItemSubjects = new List<SelectListItem>();
            HREmployeeMstList = new List<HREmployeeMst>();
            HREmployeeMstslctList = new List<SelectListItem>();
            sendemailList = new List<sendemail>();
            generalslctlst = new List<SelectListItem>();
            listitems = new List<int>();
            selecteditems = new List<string>();
            LixtBoxModelList = new List<LixtBoxModel>();
        }
        public List<SelectListItem> SelectListItemSChool { get; set; }
        public List<SelectListItem> SelectListItemDepartment { get; set; }
        public List<SelectListItem> SelectListItemSubjects { get; set; }
        public string SchlsId { get; set; }
        public int DpmtId { get; set; }
        public int SbjtId { get; set; }
        public List<HREmployeeMst> HREmployeeMstList { get; set; }
        public List<int> listitems { get; set; }
        public List<string> selecteditems { get; set; }
        public string intlist { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public List<SelectListItem> HREmployeeMstslctList { get; set; }
        public List<SelectListItem> generalslctlst { get; set; }
        public List<sendemail> sendemailList { get; set; }
        public List<LixtBoxModel> LixtBoxModelList { get; set; }
        public string strinfyvalues { get; set; }

    }
}