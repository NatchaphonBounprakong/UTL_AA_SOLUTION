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
    public class UserProfileService : CommonService
    {
        private UTL_AAEntities2 ctx = new UTL_AAEntities2();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();

        public RESPONSE_MODEL GetListUserProfile()
        {
            try
            {

                var employee = ctx.EMPLOYEES.ToList();
                resp.OUTPUT_DATA = employee;
                List<USER_PROFILE_MODEL> listUser = (from item in employee
                                                     select new USER_PROFILE_MODEL
                                                     {
                                                         EMPLOYEE_DEPARTMENT = item.EMPLOYEE_DEPARTMENT,
                                                         EMPLOYEE_DISPLAY_NAME = item.EMPLOYEE_DISPLAY_NAME,
                                                         EMPLOYEE_FIRST_NAME = item.EMPLOYEE_FIRST_NAME,
                                                         EMPLOYEE_LAST_NAME = item.EMPLOYEE_LAST_NAME,
                                                         EMPLOYEE_ID = item.EMPLOYEE_ID,
                                                         EMPLOYEE_STATUS = item.EMPLOYEE_STATUS
                                                     }).ToList();
                resp.OUTPUT_DATA = listUser;


            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }

        public RESPONSE_MODEL GetUserProfile(int employee_id)
        {
            try
            {
                var data = ctx.EMPLOYEES.Where(o => o.EMPLOYEE_ID == employee_id).FirstOrDefault();
                USER_PROFILE_MODEL employee = new USER_PROFILE_MODEL();
                employee.EMPLOYEE_DEPARTMENT = data.EMPLOYEE_DEPARTMENT;
                employee.EMPLOYEE_DISPLAY_NAME = data.EMPLOYEE_DISPLAY_NAME;
                employee.EMPLOYEE_FIRST_NAME = data.EMPLOYEE_FIRST_NAME;
                employee.EMPLOYEE_LAST_NAME = data.EMPLOYEE_LAST_NAME;
                employee.EMPLOYEE_STATUS = data.EMPLOYEE_STATUS;
                employee.EMPLOYEE_ID = data.EMPLOYEE_ID;
                resp.OUTPUT_DATA = employee;
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;

        }

        public RESPONSE_MODEL UpdateUserProfile(EMPLOYEES source)
        {
            try
            {
                ctx.EMPLOYEES.Add(source);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }

        public RESPONSE_MODEL GetUserRole(int employee_id, int application_id)
        {
            try
            {
                var appRole = ctx.ROLE.Where(o => o.APP_ID == application_id).ToList();
                List<USER_PROFILE_ROLE_MODEL> listData = new List<USER_PROFILE_ROLE_MODEL>();
                foreach (var item in appRole)
                {
                    USER_PROFILE_ROLE_MODEL userRole = new USER_PROFILE_ROLE_MODEL();
                    userRole.ROLE_ID = item.ROLE_ID;
                    userRole.ROLE_NAME = item.ROLE_NAME;
                    userRole.ALLOWED = ctx.USER_ROLES.Any(o => o.EMPLOYEE_ID == employee_id && o.ROLE_ID == item.ROLE_ID) == true
                                                                         ? (ctx.USER_ROLES.Where(o => o.EMPLOYEE_ID == employee_id
                                                                         && o.ROLE_ID == item.ROLE_ID).FirstOrDefault().USER_ROLE_ALLOWED == "Y" ? true : false) : false;
                    listData.Add(userRole);
                }
                resp.OUTPUT_DATA = listData;
            }
            catch (Exception ex)
            {

                resp = ErrorCollection(ex);
            }
            return resp;
        }

        public RESPONSE_MODEL GetUserApplication(int employee_id)
        {
            try
            {
                var listApp = ctx.APPLICATIONS.ToList();
                List<USER_PROFILE_APP_MODEL> listData = new List<USER_PROFILE_APP_MODEL>();
                foreach (var item in listApp)
                {
                    USER_PROFILE_APP_MODEL userApp = new USER_PROFILE_APP_MODEL();
                    userApp.APP_ID = item.APP_ID;
                    userApp.APP_NAME = item.APP_NAME;
                    userApp.ALLOWED = ctx.APPLICATION_PERMISISONS.Any(o => o.EMPLOYEE_ID == employee_id && o.APP_ID == item.APP_ID) == true
                                                                         ? (ctx.APPLICATION_PERMISISONS.Where(o => o.EMPLOYEE_ID == employee_id
                                                                         && o.APP_ID == item.APP_ID).FirstOrDefault().APP_PERMISISON_ALLOWED == "Y" ? true : false) : false;
                    listData.Add(userApp);
                }
                resp.OUTPUT_DATA = listData;
            }
            catch (Exception ex)
            {

                resp = ErrorCollection(ex);
            }
            return resp;
        }

        public RESPONSE_MODEL UpdateUserApplication(int employee_id, List<int> application_id)
        {
            try
            {

                var allApp = ctx.APPLICATIONS.ToList();
                if (application_id != null)
                {
                    var deleteOld = ctx.APPLICATION_PERMISISONS.Where(o => application_id.Contains(o.APP_ID) && o.EMPLOYEE_ID == employee_id).ToList();
                    ctx.APPLICATION_PERMISISONS.RemoveRange(deleteOld);

                    var allowedApp = allApp.Where(o => application_id.Contains(o.APP_ID)).ToList();
                    foreach (var item in allowedApp)
                    {
                        APPLICATION_PERMISISONS app = new APPLICATION_PERMISISONS();
                        app.APP_ID = item.APP_ID;
                        app.EMPLOYEE_ID = employee_id;
                        app.APP_PERMISISON_ALLOWED = "Y";
                        ctx.APPLICATION_PERMISISONS.Add(app);
                    }
                    var notAllowedApp = allApp.Where(o => !application_id.Contains(o.APP_ID)).ToList();
                    foreach (var item in notAllowedApp)
                    {
                        APPLICATION_PERMISISONS app = new APPLICATION_PERMISISONS();
                        app.APP_ID = item.APP_ID;
                        app.EMPLOYEE_ID = employee_id;
                        app.APP_PERMISISON_ALLOWED = "N";
                        ctx.APPLICATION_PERMISISONS.Add(app);
                    }


                }
                else
                {
                    var deleteOld = ctx.APPLICATION_PERMISISONS.Where(o => o.EMPLOYEE_ID == employee_id).ToList();
                    ctx.APPLICATION_PERMISISONS.RemoveRange(deleteOld);
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = ErrorCollection(ex);
            }
            return resp;
        }

        public RESPONSE_MODEL UpdateUserRole(int employee_id, int application_id, List<int> role_id)
        {
            try
            {

                var allRole = ctx.ROLE.Where(o => o.APP_ID == application_id).Select(o => o.ROLE_ID).ToList();
                if (role_id != null)
                {
                    var deleteOld = ctx.USER_ROLES.Where(o => allRole.Contains(o.ROLE_ID) && o.EMPLOYEE_ID == employee_id).ToList();
                    ctx.USER_ROLES.RemoveRange(deleteOld);

                    var allowedRole = allRole.Where(o => role_id.Contains(o)).ToList();
                    foreach (var item in allowedRole)
                    {
                        USER_ROLES role = new USER_ROLES();
                        role.ROLE_ID = item;
                        role.EMPLOYEE_ID = employee_id;
                        role.USER_ROLE_ALLOWED = "Y";
                        ctx.USER_ROLES.Add(role);
                    }
                    var notAllowedRole = allRole.Where(o => !role_id.Contains(o)).ToList();
                    foreach (var item in notAllowedRole)
                    {
                        USER_ROLES role = new USER_ROLES();
                        role.ROLE_ID = item;
                        role.EMPLOYEE_ID = employee_id;
                        role.USER_ROLE_ALLOWED = "N";
                        ctx.USER_ROLES.Add(role);
                    }

                }
                else
                {
                    var deleteOld = ctx.USER_ROLES.Where(o => allRole.Contains(o.ROLE_ID) && o.EMPLOYEE_ID == employee_id).ToList();
                    ctx.USER_ROLES.RemoveRange(deleteOld);
                }
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
