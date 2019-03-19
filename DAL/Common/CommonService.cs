using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common
{
    public class CommonService
    {
        public RESPONSE_MODEL ErrorCollection(Exception ex)
        {
            RESPONSE_MODEL resp = new RESPONSE_MODEL();
            resp.STATUS = false;
            resp.ERROR_STACK = ex.StackTrace.ToString();
            resp.MESSAGE = ex.Message.ToString();
            resp.INNER_EXCEPTION = ex.InnerException.ToString();
            return resp;
        }
    }
}
