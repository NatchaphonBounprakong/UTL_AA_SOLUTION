using DAL.Services;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRESENTER.Controllers
{
    public class RolePermissionController : Controller
    {
        // GET: RolePermission
        RESPONSE_MODEL resp = new RESPONSE_MODEL();
        RolePermissionService rolePermissionService = new RolePermissionService();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRolePermissionByApplication(int application_id,int role_id)
        {
            resp = rolePermissionService.GetRolePermissionByApplication(application_id,role_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRolePermission(int menu_id,int role_id)
        {
            resp = rolePermissionService.GetRolePermission(menu_id, role_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateRolePermission(int menu_id, int role_id,string list_permission)
        {
            resp = rolePermissionService.UpdateRolePermission(menu_id,role_id, list_permission);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

    }
}