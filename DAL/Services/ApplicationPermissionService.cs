using DAL.Common;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ApplicationPermissionService : CommonService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();

        public RESPONSE_MODEL GetUserApplicationPermission(int employee_id, string application_code)
        {
            try
            {
                var application = ctx.APPLICATIONS.Where(o => o.APP_CODE == application_code).FirstOrDefault();
                var permission = ctx.APPLICATION_PERMISISONS.Where(o => o.APP_ID == application.APP_ID).FirstOrDefault();
                resp.OUTPUT_DATA = permission.APP_PERMISISON_ALLOWED;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }

    }
}
