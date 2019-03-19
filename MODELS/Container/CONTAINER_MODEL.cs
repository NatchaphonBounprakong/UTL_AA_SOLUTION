using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class CONTAINER_MODEL
    {
        public List<APPLICATION_MODEL> LIST_APPLICATION_MODEL { get; set; }
        public List<APPLICATION_MENU_MODEL> LIST_MENU_MODEL { get; set; }
        public List<ROLE_MODEL> LIST_ROLE_MODEL { get; set; }
        public List<ROLE_PERMISSION_ALLOWED_MODEL> LIST_ROLE_PERMISSION_ALLOWED_MODEL { get; set; }
       
    }
}
