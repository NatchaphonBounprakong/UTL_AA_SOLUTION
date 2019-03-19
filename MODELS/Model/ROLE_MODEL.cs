using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class ROLE_MODEL
    {
        public int ROLE_ID { get; set; }
        public Nullable<int> APP_ID { get; set; }
        public string ROLE_NAME { get; set; }
        public string ROLE_DESCRIPTION { get; set; }
    }
}
