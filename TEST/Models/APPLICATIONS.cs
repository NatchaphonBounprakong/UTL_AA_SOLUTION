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
    
    public partial class APPLICATIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public APPLICATIONS()
        {
            this.APPLICATION_MENUS = new HashSet<APPLICATION_MENUS>();
            this.APPLICATION_PERMISISONS = new HashSet<APPLICATION_PERMISISONS>();
        }
    
        public int APP_ID { get; set; }
        public string APP_NAME { get; set; }
        public string APP_URL { get; set; }
        public string APP_DESCRIPTION { get; set; }
        public Nullable<int> APP_STATUS { get; set; }
        public string APP_CODE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION_MENUS> APPLICATION_MENUS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION_PERMISISONS> APPLICATION_PERMISISONS { get; set; }
    }
}
