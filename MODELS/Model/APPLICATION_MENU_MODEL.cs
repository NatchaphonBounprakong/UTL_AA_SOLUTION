using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class APPLICATION_MENU_MODEL
    {
        public int MENU_ID { get; set; }
        public Nullable<int> PARENT_ID { get; set; }
        public string PARENT_NAME { get; set; }
        public int APP_ID { get; set; }
        public string MENU_NAME { get; set; }
        public Nullable<int> MENU_STATUS { get; set; }
        public Nullable<int> MENU_LEVEL { get; set; }
        public Nullable<int> MENU_SEQ { get; set; }
        public List<APPLICATION_MENU_MODEL> COMPONENT { get; set; }
    }
}
