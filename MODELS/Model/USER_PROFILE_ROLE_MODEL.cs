﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Model
{
    public class USER_PROFILE_ROLE_MODEL
    {
        public int ROLE_ID { get; set; }
        public int EMPLOYEE_ID { get; set; }

        public string ROLE_NAME { get; set; }
        public bool ALLOWED { get; set; }
    }
}
