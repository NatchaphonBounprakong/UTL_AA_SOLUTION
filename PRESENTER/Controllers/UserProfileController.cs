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
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        UserProfileService service = new UserProfileService();
        RESPONSE_MODEL resp = new RESPONSE_MODEL();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListUserProfile()
        {
            resp = service.GetListUserProfile();
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserProfile(int employee_id)
        {
            resp = service.GetUserProfile(employee_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUserProfile(EMPLOYEES source)
        {
            resp = service.UpdateUserProfile(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserApplication(int employee_id)
        {
            resp = service.GetUserApplication(employee_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUserApplication(int employee_id, List<int> application_id)
        {
            resp = service.UpdateUserApplication(employee_id, application_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUserRole(int employee_id, int application_id, List<int> role_id)
        {
            resp = service.UpdateUserRole(employee_id, application_id, role_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserRole(int employee_id, int application_id)
        {
            resp = service.GetUserRole(employee_id, application_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }


    }
}