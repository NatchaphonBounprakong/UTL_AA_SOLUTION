using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Model
{
    public class USER_PROFILE_APP_MODEL
    {
        public int APP_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }

        public string APP_NAME { get; set; }
        public bool ALLOWED { get; set; }
    }
}
