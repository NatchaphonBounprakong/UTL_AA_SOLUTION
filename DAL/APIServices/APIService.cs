using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.APIServices
{
    public class APIService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();

        public RESPONSE_MODEL GetUserApplication(int employee_id)
        {
            var listData = (from a in ctx.APPLICATION_PERMISISONS.Where(o => o.EMPLOYEE_ID == employee_id)
                            select a).ToList();

            resp.OUTPUT_DATA = listData;
            return resp;
        }
    }
}
