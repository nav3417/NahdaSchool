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
    
    public partial class DigitalResourceClass
    {
        public int Id { get; set; }
        public Nullable<int> MasterId { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual LessonPlanClass LessonPlanClass { get; set; }
        public virtual DigitalResourceMaster DigitalResourceMaster { get; set; }
    }
}
