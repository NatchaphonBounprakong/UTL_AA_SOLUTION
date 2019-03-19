using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class ROLE_PERMISSION_ALLOWED_MODEL
    {
        public int ROLE_PERMISSION_ID { get; set; }
        public int ROLE_ID { get; set; }
        public int MENU_ID { get; set; }
        public int? MENU_LEVEL { get; set; }
        public int? PARENT_ID { get; set; }
        public string MENU_NAME { get; set; }

        public string GRANT_PERMISSION { get; set; }
        public string VIEW_PERMISSION { get; set; }
        public string INSERT_PERMISSION { get; set; }
        public string UPDATE_PERMISSION { get; set; }
        public string DELETE_PERMISSION { get; set; }
    }

   
}
