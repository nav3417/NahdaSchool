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
    
    public partial class Attendance_Sheet
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> CheckTime { get; set; }
        public string CheckType { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> MachineId { get; set; }
    }
}
