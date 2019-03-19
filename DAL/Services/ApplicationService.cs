using DAL;
using DAL.Common;
using MODELS;
using MODELS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ApplicationService : CommonService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();

        public RESPONSE_MODEL GetListApplication(FILTER_APPLICATION_MODEL filter)
        {
            try
            {
                var listData = new List<APPLICATIONS>();
                if (filter != null)
                {
                    listData = ctx.APPLICATIONS.
                                  Where(o => filter.APP_NAME.ToString() != "" ?
                                        o.APP_NAME.ToUpper() == filter.APP_NAME.ToUpper() :
                                        o.APP_NAME.Contains("")).ToList();
                }
                else
                {
                    listData = ctx.APPLICATIONS.ToList();
                }

                var listApplication = new List<APPLICATION_MODEL>();
                listApplication = (from item in listData
                                   select new APPLICATION_MODEL
                                   {
                                       APP_ID = item.APP_ID,
                                       APP_NAME = item.APP_NAME,
                                       APP_DESCRIPTION = item.APP_DESCRIPTION,
                                       APP_STATUS = item.APP_STATUS == 1 ? "Active" : "Inactive",
                                       APP_URL = item.APP_URL,
                                       APP_CODE = item.APP_CODE

                                   }).OrderBy(o => o.APP_NAME).ToList();

                container.LIST_APPLICATION_MODEL = listApplication;
                resp.OUTPUT_DATA = container;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }

            return resp;

        }

        public RESPONSE_MODEL GetApplication(int application_id)
        {
            try
            {
                var data = ctx.APPLICATIONS.Where(o => o.APP_ID == application_id).FirstOrDefault();
                var application = new APPLICATION_MODEL();
                application.APP_ID = data.APP_ID;
                application.APP_NAME = data.APP_NAME;
                application.APP_CODE = data.APP_CODE;
                application.APP_DESCRIPTION = data.APP_DESCRIPTION;
                application.APP_URL = data.APP_URL;
                application.APP_STATUS = data.APP_STATUS.ToString();

                resp.OUTPUT_DATA = application;
                
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }

            return resp;

        }
        public RESPONSE_MODEL AddApplication(APPLICATIONS source)
        {
            try
            {
                APPLICATIONS application = new APPLICATIONS();
                application.APP_NAME = source.APP_NAME;
                application.APP_CODE = source.APP_CODE;
                application.APP_DESCRIPTION = source.APP_DESCRIPTION;
                application.APP_STATUS = source.APP_STATUS;
                application.APP_URL = source.APP_URL;

                ctx.APPLICATIONS.Add(application);

                APPLICATION_MENUS menu = new APPLICATION_MENUS();
                menu.APP_ID = application.APP_ID;
                menu.MENU_LEVEL = 0;
                menu.MENU_SEQ = 0;
                menu.MENU_STATUS = 1;
                menu.MENU_NAME = "Home";

                ctx.APPLICATION_MENUS.Add(menu);

                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;

        }

        public RESPONSE_MODEL UpdateApplication(APPLICATIONS source)
        {
            try
            {
                var application = ctx.APPLICATIONS.Where(o => o.APP_ID == source.APP_ID).FirstOrDefault();
                application.APP_NAME = source.APP_NAME;
                application.APP_CODE = source.APP_CODE;
                application.APP_DESCRIPTION = source.APP_DESCRIPTION;
                application.APP_STATUS = source.APP_STATUS;
                application.APP_URL = source.APP_URL;
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;

        }
    }
}
