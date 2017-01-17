using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafeMode.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}