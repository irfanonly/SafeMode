using Microsoft.AspNet.Identity;
using SafeMode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace SafeMode.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageUserController : Controller
    {
        safeModeEntities db = new safeModeEntities();


        // GET: ManageUser
        public ActionResult Index()
        {
            if (TempData["Succuss"] != null)
            {
                ViewBag.SuccessMsg = TempData["Succuss"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.ErrorMsg = TempData["Error"];
            }

            var users = db.AspNetUsers.Where(x => x.AspNetRoles.FirstOrDefault().Id == "2");

            return View(users);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {

            var user = db.AspNetUsers.Find(id);

            if(user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }


    }
}