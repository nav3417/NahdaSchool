//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class CommunicationGroupDetail
    {
        public int Id { get; set; }
        public Nullable<int> MastId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
    
        public virtual CommunicationGroupMaster CommunicationGroupMaster { get; set; }
    }
}
