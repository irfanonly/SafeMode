using SafeMode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafeMode.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("Index","ManageUser");
        }

        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Profile(ProfileVM model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }
    }
}