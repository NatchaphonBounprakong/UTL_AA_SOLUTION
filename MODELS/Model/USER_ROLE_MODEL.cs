using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class USER_ROLE_MODEL
    {
        public int USER_ROLE_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }
        public Nullable<int> ROLE_ID { get; set; }
        public string USER_ROLE_ALLOWED { get; set; }
    }
}
