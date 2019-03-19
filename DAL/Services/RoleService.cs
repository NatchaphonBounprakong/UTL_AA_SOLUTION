using DAL.Common;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class RoleService : CommonService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();

        public RESPONSE_MODEL GeListApplicationRole(int application_id)
        {
            try
            {
                var listdata = ctx.ROLE.Where(o => o.APP_ID == application_id).ToList();
                var listRole = (from item in listdata
                                select new ROLE_MODEL
                                {
                                    APP_ID = item.APP_ID,
                                    ROLE_NAME = item.ROLE_NAME,
                                    ROLE_ID = item.ROLE_ID,
                                    ROLE_DESCRIPTION = item.ROLE_DESCRIPTION

                                }).ToList();

                container.LIST_ROLE_MODEL = listRole;
                resp.OUTPUT_DATA = container;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }

            return resp;
        }
        public RESPONSE_MODEL AddApplicationRole(ROLE source)
        {
            try
            {
                var role = new ROLE();
                role.ROLE_NAME = source.ROLE_NAME;
                role.ROLE_DESCRIPTION = source.ROLE_DESCRIPTION;
                role.APP_ID = source.APP_ID;

                ctx.ROLE.Add(role);
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL UpdateApplicationRole(ROLE source)
        {
            try
            {
                var role = ctx.ROLE.Where(o => o.ROLE_ID == source.ROLE_ID).FirstOrDefault();

                role.ROLE_NAME = source.ROLE_NAME;
                role.ROLE_DESCRIPTION = source.ROLE_DESCRIPTION;


                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL DeleteApplicationRole(int role_id)
        {
            try
            {
                var role = ctx.ROLE.Where(o => o.ROLE_ID == role_id).FirstOrDefault();
                ctx.ROLE.Remove(role);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;

        }
        public RESPONSE_MODEL GetApplicationRole(int role_id)
        {
            try
            {
                var role = ctx.ROLE.Where(o => o.ROLE_ID == role_id).FirstOrDefault();
                var data = new ROLE_MODEL();
                data.ROLE_ID = role.ROLE_ID;
                data.ROLE_NAME = role.ROLE_NAME;
                data.ROLE_DESCRIPTION = role.ROLE_DESCRIPTION;

                resp.OUTPUT_DATA = data;

            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;

        }
    }
}
