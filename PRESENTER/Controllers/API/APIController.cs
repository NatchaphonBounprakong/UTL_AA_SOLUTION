using DAL.APIServices;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace PRESENTER.Controllers.API
{
    public class APIController : Controller
    {
        // GET: API
        APIService service = new APIService();
        RESPONSE_MODEL resp = new RESPONSE_MODEL();
        public ActionResult Index()
        {
            return View();
        }

        
    }
}