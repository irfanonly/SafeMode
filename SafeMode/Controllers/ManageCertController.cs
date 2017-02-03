using PagedList;
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
        safeModeEntities db = new safeModeEntities();
        // GET: ManageCert
        public ActionResult Index(string name, int page = 1)
        {
            if (TempData["Succuss"] != null)
            {
                ViewBag.SuccessMsg = TempData["Succuss"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.ErrorMsg = TempData["Error"];
            }

            var certs = db.Certificates.OrderByDescending(x => x.uploadedon);

            if (name != null && name != "")
            {
                ViewBag.name = name;
                certs = certs.Where(x => x.name.Contains(name)).OrderBy(x => x.name);
            }

            return View(certs.ToPagedList(page, 10));
        }


        public ActionResult Add()
        {
            var certTypes = db.CertTypes;
            ViewBag.typeid = new SelectList(certTypes , "id", "name");
            return View();
        }

        public ActionResult AutocompleteCompanyName(string term)
        {
            var items = db.AspNetUsers.Where(x => x.AspNetRoles.FirstOrDefault().Id == "2" && x.Name.Contains(term)).Select(x => x.Name).ToList();

            //var filteredItems = items.Where(
            //    item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            //    );
            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }
    }
}