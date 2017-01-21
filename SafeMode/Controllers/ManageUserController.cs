using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafeMode.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageUserController : Controller
    {
        safeModeEntities db = new safeModeEntities();
        // GET: ManageUser
        public ActionResult Index()
        {
            var users = db.AspNetUsers.Where(x => x.AspNetRoles.FirstOrDefault().Id == "2");

            return View(users);
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}