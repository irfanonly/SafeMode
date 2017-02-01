using Microsoft.AspNet.Identity;
using SafeMode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using PagedList;

namespace SafeMode.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageUserController : Controller
    {
        safeModeEntities db = new safeModeEntities();


        // GET: ManageUser
        public ActionResult Index(string username,int page = 1)
        {
            if (TempData["Succuss"] != null)
            {
                ViewBag.SuccessMsg = TempData["Succuss"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.ErrorMsg = TempData["Error"];
            }

            var users = db.AspNetUsers.Where(x => x.AspNetRoles.FirstOrDefault().Id == "2").OrderBy(x => x.Name);

            if(username != null  && username != "")
            {
                ViewBag.username = username;
                users = users.Where(x => x.UserName.Contains(username)).OrderBy(x => x.Name);
            }

            return View(users.ToPagedList(page, 10));
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

            UserEditVM userEditVM = new UserEditVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };


            return View(userEditVM);
        }

        public ActionResult Delete(string id)
        {
            var idss = id.Split(',');

            foreach(var i in idss)
            {
                
                var user = db.AspNetUsers.Find(i);
                db.AspNetUsers.Remove(user);

            }

            db.SaveChanges();

            TempData["Succuss"] = "Successfully user deleted";

            return RedirectToAction("Index");

           
        }

        [HttpPost]
        public ActionResult Edit(UserEditVM userEditVM) {

            if (ModelState.IsValid)
            {
                var user = db.AspNetUsers.Find(userEditVM.Id);

                user.Name = userEditVM.Name;
                user.Email = userEditVM.Email;
                user.PhoneNumber = userEditVM.PhoneNumber;

                db.SaveChanges();

                TempData["Succuss"] = "Successfully user updated";

                return RedirectToAction("Index");
            }

            return View(userEditVM);
        }

        public ActionResult ChangePassword(string id)
        {
            var user = db.AspNetUsers.Find(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            ChangePasswordVM userEditVM = new ChangePasswordVM
            {
                Id = user.Id,
                UserName = user.UserName
               
            };

            return View(userEditVM);
        }

    }
}