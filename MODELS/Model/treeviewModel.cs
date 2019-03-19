using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Model
{
    public class treeviewModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }

        public treeviewModelOption state { get; set; }

    }

    public class treeviewModelOption
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }

        public treeviewModelOption()
        {
            opened = true;
            disabled = false;
            selected = false;
        }
    }
}
