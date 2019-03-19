using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Services;

namespace TEST.Controllers
{
    public class UserProfileController : Controller
    {
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
    }
}