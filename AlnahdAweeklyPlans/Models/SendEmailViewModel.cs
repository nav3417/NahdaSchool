using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlnahdAweeklyPlans.Models
{
    public class SendEmailViewModel
    {
        public SendEmailViewModel()
        {
            CommunicationMstSelectList=new List<SelectListItem>();
            multiselectedemaildetl = new List<int>();
            emailInuerable = new List<sendemail>();
            emaildetail = new List<sendemail>();
            CommunicationDtlSelectList = new List<SelectListItem>();
            StudentEmailMasterList = new List<StudentEmailMaster>();
        }
        public int Id { get; set; }
        public List<SelectListItem> CommunicationMstSelectList { get; set; }
        public List<SelectListItem> CommunicationDtlSelectList { get; set; }
        public List<sendemail> emailInuerable { get; set; }
        public List<sendemail> emaildetail { get; set; }
        public List<int> multiselectedemailmstr { get; set; }
        public List<int> multiselectedemaildetl { get; set; }
        public string ids { get; set; }
        public List<string> Indivisuald { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public ResponceModel responce { get; set; }
        public List<StudentEmailMaster> StudentEmailMasterList { get; set; }
        public string SaveType { get; set; }

    }
}