using DAL;
using DAL.Services;
using MODELS;
using MODELS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRESENTER.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        RESPONSE_MODEL resp = new RESPONSE_MODEL();
        ApplicationService appService = new ApplicationService();
        public ActionResult Index()
        {
            return View();
        }
     
        public JsonResult GetListApplication(FILTER_APPLICATION_MODEL filter)
        {
            resp = appService.GetListApplication(filter);
            return Json(resp,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetApplication(int application_id)
        {
            resp = appService.GetApplication(application_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddApplication(APPLICATIONS source)
        {
            resp = appService.AddApplication(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateApplication(APPLICATIONS source)
        {
            resp = appService.UpdateApplication(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}