using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafeMode.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageCertController : Controller
    {
        // GET: ManageCert
        public ActionResult Index()
        {
            return View();
        }
    }
}