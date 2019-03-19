using DAL.Common;
using MODELS;
using MODELS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ApplicationMenuService : CommonService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();
        public RESPONSE_MODEL GetListApplicationMenu(int application_id)
        {
            try
            {
                var listdata = ctx.APPLICATION_MENUS
                                  .Where(o => o.APP_ID == application_id
                                                       && o.PARENT_ID == null).ToList();
                var listMenu = (from item in listdata
                                select new APPLICATION_MENU_MODEL
                                {
                                    APP_ID = item.APP_ID,
                                    MENU_NAME = item.MENU_NAME,
                                    MENU_ID = item.MENU_ID,
                                    MENU_LEVEL = item.MENU_LEVEL,
                                    MENU_SEQ = item.MENU_SEQ,
                                    PARENT_ID = item.PARENT_ID,
                                    MENU_STATUS = item.MENU_STATUS,
                                    COMPONENT = (from component in ctx.APPLICATION_MENUS.Where(o => o.PARENT_ID == item.MENU_ID)
                                                 select new APPLICATION_MENU_MODEL
                                                 {
                                                     APP_ID = component.APP_ID,
                                                     MENU_NAME = component.MENU_NAME,
                                                     MENU_ID = component.MENU_ID,
                                                     MENU_LEVEL = component.MENU_LEVEL,
                                                     MENU_SEQ = component.MENU_SEQ,
                                                     PARENT_ID = component.PARENT_ID,
                                                     MENU_STATUS = component.MENU_STATUS,

                                                 }).OrderBy(o => o.MENU_SEQ).ToList()

                                }).OrderBy(o => o.MENU_SEQ).ToList();

                container.LIST_MENU_MODEL = listMenu;
                resp.OUTPUT_DATA = container;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }

            return resp;
        }
        public RESPONSE_MODEL GetApplicationMenu(int menu_id)
        {
            try
            {
                var data = ctx.APPLICATION_MENUS
                                 .Where(o => o.MENU_ID == menu_id).FirstOrDefault();

                var menu = new APPLICATION_MENU_MODEL();
                menu.MENU_ID = data.MENU_ID;
                menu.PARENT_ID = data.PARENT_ID;
                menu.PARENT_NAME = ctx.APPLICATION_MENUS
                                      .Where(o => o.MENU_ID == data.PARENT_ID)
                                      .FirstOrDefault()
                                      .MENU_NAME;
                menu.APP_ID = data.APP_ID;
                menu.MENU_NAME = data.MENU_NAME;
                menu.MENU_LEVEL = data.MENU_LEVEL;
                menu.MENU_SEQ = data.MENU_SEQ;
                menu.MENU_STATUS = data.MENU_STATUS;
                resp.OUTPUT_DATA = menu;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL AddApplicationMenu(APPLICATION_MENU_MODEL source)
        {
            try
            {
                var menu = new APPLICATION_MENUS();
                menu.MENU_NAME = source.MENU_NAME;
                menu.MENU_STATUS = source.MENU_STATUS;
                menu.MENU_SEQ = source.MENU_SEQ;
                menu.MENU_LEVEL = source.MENU_LEVEL;
                menu.PARENT_ID = source.PARENT_ID;
                menu.APP_ID = source.APP_ID;

                ctx.APPLICATION_MENUS.Add(menu);
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL GetComboApplication()
        {
            try
            {
                var listData = ctx.APPLICATIONS.ToList();
                var listApplication = (from item in listData
                                       select new APPLICATION_MODEL
                                       {
                                           APP_ID = item.APP_ID,
                                           APP_NAME = item.APP_NAME

                                       }).ToList();

                resp.OUTPUT_DATA = listApplication;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL UpdateApplicationMenu(APPLICATION_MENU_MODEL source)
        {
            try
            {
                var menu = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == source.MENU_ID).FirstOrDefault();

                menu.MENU_NAME = source.MENU_NAME;
                menu.MENU_STATUS = source.MENU_STATUS;
                menu.MENU_SEQ = source.MENU_SEQ;

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL DeleteApplicationMenu(int menu_id)
        {
            try
            {
                var menu = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == menu_id).FirstOrDefault();
                ctx.APPLICATION_MENUS.Remove(menu);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;

        }
        public RESPONSE_MODEL GetApplicationMenuTreeview(int application_id)
        {
            try
            {
                var listdata = ctx.APPLICATION_MENUS
                                  .Where(o => o.APP_ID == application_id).OrderBy(o=>o.MENU_LEVEL)
                                                                         .ThenBy(o=>o.MENU_SEQ).ToList();
                treeviewModelOption state = new treeviewModelOption();

                var listMenu = (from item in listdata
                                select new treeviewModel
                                {
                                    id = item.MENU_ID.ToString(),
                                    parent = item.PARENT_ID.ToString() == "" ? "#" : item.PARENT_ID.ToString(),
                                    text = item.MENU_NAME,
                                    state = state,

                                }).ToList();

                resp.OUTPUT_DATA = listMenu;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }

            return resp;
        }
    }
}
