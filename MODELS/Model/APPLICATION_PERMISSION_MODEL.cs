using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class APPLICATION_PERMISSION_MODEL
    {
        public int APP_PERMISSION_ID { get; set; }
        public int APP_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }
        public string APP_PERMISISON_ALLOWED { get; set; }
        public string APP_PERMISSION_MAX_SESSION { get; set; }
        public string APP_PERMISSION_DESCRIPTION { get; set; }

    }
}
