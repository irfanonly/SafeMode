using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SafeMode.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        safeModeEntities db = new safeModeEntities();
        // GET: User
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            var userId = User.Identity.GetUserId();
            var certi = db.Certificates.Where(x => x.assigneeid == userId);
            return View(certi);
        }
    }
}