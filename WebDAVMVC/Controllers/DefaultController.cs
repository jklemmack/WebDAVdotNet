using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDAVMVC.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Get(string resource)
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
