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
    
    public partial class EmailBlasterTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailBlasterTemplate()
        {
            this.BlasterLogs = new HashSet<BlasterLog>();
        }
    
        public int Id { get; set; }
        public string EmailCategory { get; set; }
        public string RecipientsCategory { get; set; }
        public string EmailTitle { get; set; }
        public string EmailBody { get; set; }
        public string ContentPicture { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlasterLog> BlasterLogs { get; set; }
    }
}
