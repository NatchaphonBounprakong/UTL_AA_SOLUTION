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
    
    public partial class APPLICATION_MENUS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public APPLICATION_MENUS()
        {
            this.APPLICATION_MENUS1 = new HashSet<APPLICATION_MENUS>();
            this.EMPLOYEE_DENIED_PERMISSION = new HashSet<EMPLOYEE_DENIED_PERMISSION>();
            this.ROLE_PERMISSION_ALLOWED = new HashSet<ROLE_PERMISSION_ALLOWED>();
        }
    
        public int MENU_ID { get; set; }
        public Nullable<int> PARENT_ID { get; set; }
        public int APP_ID { get; set; }
        public string MENU_NAME { get; set; }
        public Nullable<int> MENU_STATUS { get; set; }
        public Nullable<int> MENU_LEVEL { get; set; }
        public Nullable<int> MENU_SEQ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION_MENUS> APPLICATION_MENUS1 { get; set; }
        public virtual APPLICATION_MENUS APPLICATION_MENUS2 { get; set; }
        public virtual APPLICATIONS APPLICATIONS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE_DENIED_PERMISSION> EMPLOYEE_DENIED_PERMISSION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE_PERMISSION_ALLOWED> ROLE_PERMISSION_ALLOWED { get; set; }
    }
}
