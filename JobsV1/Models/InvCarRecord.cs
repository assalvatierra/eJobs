//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobsV1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvCarRecord
    {
        public int Id { get; set; }
        public int InvItemId { get; set; }
        public int InvCarRecordTypeId { get; set; }
        public int Odometer { get; set; }
        public System.DateTime dtDone { get; set; }
        public int NextOdometer { get; set; }
        public System.DateTime NextSched { get; set; }
        public string Remarks { get; set; }
    
        public virtual InvCarRecordType InvCarRecordType { get; set; }
        public virtual InvItem InvItem { get; set; }
    }
}
