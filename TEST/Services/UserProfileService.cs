using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEST.Models;

namespace TEST.Services
{
    public class UserProfileService
    {
        private UTL_AAEntities1 ctx = new UTL_AAEntities1();
        private RESPONSE_MODEL resp = new RESPONSE_MODEL();
        private CONTAINER_MODEL container = new CONTAINER_MODEL();

        public RESPONSE_MODEL ErrorCollection(Exception ex)
        {
            RESPONSE_MODEL resp = new RESPONSE_MODEL();
            resp.STATUS = false;
            resp.ERROR_STACK = ex.StackTrace.ToString();
            resp.MESSAGE = ex.Message.ToString();
            resp.INNER_EXCEPTION = ex.InnerException.ToString();
            return resp;
        }

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
    }
}