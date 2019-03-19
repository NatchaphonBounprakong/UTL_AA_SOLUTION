using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class RESPONSE_MODEL
    {
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public string ERROR_STACK { get; set; }
        public dynamic OUTPUT_DATA { get; set; }

        public string INNER_EXCEPTION {get;set;}

        public RESPONSE_MODEL()
        {
            STATUS = true;
        }
    }
}
