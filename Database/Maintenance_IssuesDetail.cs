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
    
    public partial class Maintenance_IssuesDetail
    {
        public int Id { get; set; }
        public Nullable<int> InsideRoomLocation { get; set; }
        public string Description { get; set; }
        public string Equipment { get; set; }
        public Nullable<int> MasterId { get; set; }
    
        public virtual MaintenanceRequestFormMaster MaintenanceRequestFormMaster { get; set; }
    }
}
