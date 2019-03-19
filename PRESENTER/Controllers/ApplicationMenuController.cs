using DAL.Services;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRESENTER.Controllers
{
    public class ApplicationMenuController : Controller
    {
        // GET: ApplicationMenu
        RESPONSE_MODEL resp = new RESPONSE_MODEL();
        ApplicationMenuService menuService = new ApplicationMenuService();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListApplicationMenu(int application_id)
        {
            resp = menuService.GetListApplicationMenu(application_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetApplicationMenu(int menu_id)
        {
            resp = menuService.GetApplicationMenu(menu_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddApplicationMenu(APPLICATION_MENU_MODEL source)
        {
            resp = menuService.AddApplicationMenu(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetComboApplication()
        {
            resp = menuService.GetComboApplication();
            return Json(resp, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult GetApplicationMenuTreeview(int application_id)
        {
            resp = menuService.GetApplicationMenuTreeview(application_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateApplicationMenu(APPLICATION_MENU_MODEL source)
        {
            resp = menuService.UpdateApplicationMenu(source);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteApplicationMenu(int menu_id)
        {
            resp = menuService.DeleteApplicationMenu(menu_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}