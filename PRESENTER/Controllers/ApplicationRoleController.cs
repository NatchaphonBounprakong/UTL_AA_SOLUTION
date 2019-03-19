using DAL;
using DAL.Services;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRESENTER.Controllers
{
    public class ApplicationRoleController : Controller
    {
        // GET: ApplicationRole
        RESPONSE_MODEL resp = new RESPONSE_MODEL();
        RoleService roleService = new RoleService();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListApplicationRole(int application_id)
        {
            resp = roleService.GeListApplicationRole(application_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddApplicationRole(ROLE source)
        {
            resp = roleService.AddApplicationRole(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateApplicaionRole(ROLE source)
        {
            resp = roleService.UpdateApplicationRole(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteApplicationRole(int role_id)
        {
            resp = roleService.DeleteApplicationRole(role_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApplicationRole(int role_id)
        {
            resp = roleService.GetApplicationRole(role_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        
    }
}