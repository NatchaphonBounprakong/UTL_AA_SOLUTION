using DAL.Common;
using MODELS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class RolePermissionService : CommonService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();

        public RESPONSE_MODEL GetRolePermissionByApplication(int application_id, int role_id)
        {
            try
            {
                var listRole = new List<ROLE_PERMISSION_ALLOWED_MODEL>();
                var listMenu = ctx.APPLICATION_MENUS.Where(o => o.APP_ID == application_id).OrderBy(o => o.MENU_LEVEL).ThenBy(o => o.MENU_SEQ).ToList();

                foreach (var data in listMenu)
                {
                    var IsRolePermissionExist = ctx.ROLE_PERMISSION_ALLOWED
                                                   .Any(o => o.MENU_ID == data.MENU_ID && o.ROLE_ID == role_id);
                    if (IsRolePermissionExist)
                    {
                        var rolePermission = ctx.ROLE_PERMISSION_ALLOWED
                                                .Where(o => o.MENU_ID == data.MENU_ID && o.ROLE_ID == role_id).FirstOrDefault();

                        ROLE_PERMISSION_ALLOWED_MODEL obj = new ROLE_PERMISSION_ALLOWED_MODEL();
                        obj.MENU_NAME = data.MENU_NAME;
                        obj.ROLE_PERMISSION_ID = rolePermission.ROLE_PERMISSION_ID;
                        obj.PARENT_ID = data.PARENT_ID;
                        obj.MENU_ID = rolePermission.MENU_ID;
                        obj.ROLE_ID = rolePermission.ROLE_ID;
                        obj.GRANT_PERMISSION = rolePermission.GRANT_PERMISSION;
                        obj.VIEW_PERMISSION = rolePermission.VIEW_PERMISSION;
                        obj.INSERT_PERMISSION = rolePermission.INSERT_PERMISSION;
                        obj.UPDATE_PERMISSION = rolePermission.UPDATE_PERMISSION;
                        obj.DELETE_PERMISSION = rolePermission.DELETE_PERMISSION;
                        listRole.Add(obj);
                    }
                    else
                    {
                        ROLE_PERMISSION_ALLOWED_MODEL obj = new ROLE_PERMISSION_ALLOWED_MODEL();
                        obj.MENU_ID = data.MENU_ID;
                        obj.MENU_NAME = data.MENU_NAME;
                        obj.PARENT_ID = data.PARENT_ID;
                        obj.ROLE_ID = role_id;
                        obj.GRANT_PERMISSION = "N";
                        obj.VIEW_PERMISSION = "N";
                        obj.INSERT_PERMISSION = "N";
                        obj.UPDATE_PERMISSION = "N";
                        obj.DELETE_PERMISSION = "N";
                        listRole.Add(obj);
                    }
                }
                var minParentID = listRole.Where(o => o.PARENT_ID == null).Select(o => o.MENU_ID).FirstOrDefault();
                List<ROLE_PERMISSION_ALLOWED_MODEL> ListRoleOrder = new List<ROLE_PERMISSION_ALLOWED_MODEL>();

                listRole.ForEach(o =>
                {
                    if (o.PARENT_ID == minParentID)
                    {

                        o.VIEW_PERMISSION = null;
                        o.INSERT_PERMISSION = null;
                        o.UPDATE_PERMISSION = null;
                        o.DELETE_PERMISSION = null;

                        ListRoleOrder.Add(o);
                        ListRoleOrder.AddRange(listRole.Where(p => p.PARENT_ID == o.MENU_ID));
                    }
                });

                container.LIST_ROLE_PERMISSION_ALLOWED_MODEL = ListRoleOrder;
                resp.OUTPUT_DATA = container;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL UpdateRolePermission(int menu_id, int role_id, string list_permission)
        {
            var listRolePermission = JsonConvert.DeserializeObject<List<ROLE_PERMISSION_ALLOWED>>(list_permission);
            var listRole = ctx.ROLE_PERMISSION_ALLOWED.ToList();

            try
            {
                foreach (var item in listRolePermission)
                {
                    var mainChild = (from a in ctx.APPLICATION_MENUS.Where(o=>o.MENU_ID == menu_id)
                                     join b in ctx.APPLICATION_MENUS on a.PARENT_ID equals b.MENU_ID
                                     select b).FirstOrDefault();

                    var IsRolePermissionExist = listRole.Any(o => o.ROLE_ID == item.ROLE_ID && o.MENU_ID == item.MENU_ID);
                    if (IsRolePermissionExist)
                    {
                        var rolePermission = listRole.Where(o => o.ROLE_ID == item.ROLE_ID && o.MENU_ID == item.MENU_ID).FirstOrDefault();
                        if(mainChild.PARENT_ID == null)
                        {
                            rolePermission.GRANT_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.VIEW_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.INSERT_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.UPDATE_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.DELETE_PERMISSION = item.GRANT_PERMISSION;
                        }
                        else
                        {
                            rolePermission.GRANT_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.VIEW_PERMISSION = item.VIEW_PERMISSION;
                            rolePermission.INSERT_PERMISSION = item.INSERT_PERMISSION;
                            rolePermission.UPDATE_PERMISSION = item.UPDATE_PERMISSION;
                            rolePermission.DELETE_PERMISSION = item.DELETE_PERMISSION;
                        }
                       
                    }
                    else
                    {

                        var rolePermission = new ROLE_PERMISSION_ALLOWED();
                        if (mainChild.PARENT_ID == null)
                        {
                            rolePermission.MENU_ID = menu_id;
                            rolePermission.ROLE_ID = role_id;
                            rolePermission.GRANT_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.VIEW_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.INSERT_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.UPDATE_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.DELETE_PERMISSION = item.GRANT_PERMISSION;
                        }
                        else
                        {
                            rolePermission.MENU_ID = menu_id;
                            rolePermission.ROLE_ID = role_id;
                            rolePermission.GRANT_PERMISSION = item.GRANT_PERMISSION;
                            rolePermission.VIEW_PERMISSION = item.VIEW_PERMISSION;
                            rolePermission.INSERT_PERMISSION = item.INSERT_PERMISSION;
                            rolePermission.UPDATE_PERMISSION = item.UPDATE_PERMISSION;
                            rolePermission.DELETE_PERMISSION = item.DELETE_PERMISSION;
                        }

                        ctx.ROLE_PERMISSION_ALLOWED.Add(rolePermission);
                    }

                }


                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }
        public RESPONSE_MODEL GetRolePermission(int menu_id, int role_id)
        {
            try
            {
                var IsHeaderMenu = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == menu_id).Select(o => o.PARENT_ID);
                var IsPermissionExist = ctx.ROLE_PERMISSION_ALLOWED.Any(o => o.ROLE_ID == role_id && o.MENU_ID == menu_id);

                if (IsPermissionExist)
                {
                    var permission = ctx.ROLE_PERMISSION_ALLOWED.Where(o => o.ROLE_ID == role_id && o.MENU_ID == menu_id).FirstOrDefault();
                    ROLE_PERMISSION_ALLOWED_MODEL data = new ROLE_PERMISSION_ALLOWED_MODEL();
                    data.ROLE_PERMISSION_ID = permission.ROLE_PERMISSION_ID;
                    data.MENU_NAME = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == menu_id).Select(x => x.MENU_NAME).FirstOrDefault();
                    data.MENU_LEVEL = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == menu_id).Select(o => o.MENU_LEVEL).FirstOrDefault();
                    data.ROLE_ID = permission.ROLE_ID;
                    data.MENU_ID = permission.MENU_ID;
                    data.GRANT_PERMISSION = permission.GRANT_PERMISSION;
                    data.VIEW_PERMISSION = permission.VIEW_PERMISSION;
                    data.INSERT_PERMISSION = permission.INSERT_PERMISSION;
                    data.UPDATE_PERMISSION = permission.UPDATE_PERMISSION;
                    data.DELETE_PERMISSION = permission.DELETE_PERMISSION;
                    resp.OUTPUT_DATA = data;
                }
                else
                {
                    ROLE_PERMISSION_ALLOWED_MODEL data = new ROLE_PERMISSION_ALLOWED_MODEL();
                    data.MENU_NAME = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == menu_id).Select(x => x.MENU_NAME).FirstOrDefault();
                    data.MENU_LEVEL = ctx.APPLICATION_MENUS.Where(o => o.MENU_ID == menu_id).Select(o => o.MENU_LEVEL).FirstOrDefault();
                    data.ROLE_ID = role_id;
                    data.MENU_ID = menu_id;
                    data.GRANT_PERMISSION = "N";
                    data.VIEW_PERMISSION = "N";
                    data.INSERT_PERMISSION = "N";
                    data.UPDATE_PERMISSION = "N";
                    data.DELETE_PERMISSION = "N";
                    resp.OUTPUT_DATA = data;
                }

            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }



    }
}
