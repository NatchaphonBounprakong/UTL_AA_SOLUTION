//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ROLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ROLE()
        {
            this.ROLE_PERMISSION_ALLOWED = new HashSet<ROLE_PERMISSION_ALLOWED>();
            this.USER_ROLES = new HashSet<USER_ROLES>();
        }
    
        public int ROLE_ID { get; set; }
        public int APP_ID { get; set; }
        public string ROLE_NAME { get; set; }
        public string ROLE_DESCRIPTION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE_PERMISSION_ALLOWED> ROLE_PERMISSION_ALLOWED { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_ROLES> USER_ROLES { get; set; }
    }
}