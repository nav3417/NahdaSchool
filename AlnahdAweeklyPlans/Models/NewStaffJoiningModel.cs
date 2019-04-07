using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlnahdAweeklyPlans.Models
{
    public class NewStaffJoiningModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CertifiedBy { get; set; }
        public string CertifiedOn { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string HRDesignation { get; set; }
        public string CellNo { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankAccountNo { get; set; }
        public string LabourCard { get; set; }
        public string LabourCardExpiry { get; set; }
        public string EmaratedIdNo { get; set; }
        public string EmaratedIdIssueDate { get; set; }
        public string HrDepartment { get; set; }
        public string FatherName { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string PlaceOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public int NoOfChildren { get; set; }
        public Nullable<bool> IsAirTicket { get; set; }
        public string UAEAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public string PassportNo { get; set; }
        public string PassportCountryIssue { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
    }
}