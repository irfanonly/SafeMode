using PagedList;
using SafeMode.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public ActionResult Add(CertficateCreateModel model)
        {
            string assineeId = null;
            if(model.file != null)
            {
                var ext = Path.GetExtension(model.file.FileName);

                if (ext != ".pdf")
                {
                    ModelState.AddModelError("file", "please upload pdf format file");
                }

                if (model.file.FileName.Length > 45)
                {
                    ModelState.AddModelError("file", "please minimize file name");
                }

                if (model.file.ContentLength > (4 * 1024 * 1024  ))
                {
                    ModelState.AddModelError("file", "file should be less than 4MB");
                }
            }

            if (model.assigneeid != null)
            {
                var assignee = db.AspNetUsers.Where(x => x.Name == model.assigneeid);
                if (!assignee.Any())
                {
                    ModelState.AddModelError("assigneeid", "please select the assignee from the list");
                }
                else
                {
                    model.assigneeid = assignee.FirstOrDefault().Id;
                }
            }
            if (ModelState.IsValid)
            {
                Certificate certificate = new Certificate();
                certificate.name = model.name;
                certificate.assigneeid = model.assigneeid;
                certificate.typeid = model.typeid;
                certificate.uploadedby = User.Identity.Name;
                certificate.uploadedon = Times.getQatarTime();
                certificate.urlname = model.file.FileName;

                db.Certificates.Add(certificate);
                db.SaveChanges();

                var Cid = db.Certificates.Max(x=> x.id);

                model.file.SaveAs(Server.MapPath("~/images/certificates/" + Cid + "_"+ model.file.FileName) );                                                                          


                TempData["Succuss"] = "Successfully certificate created";

                return RedirectToAction("Index");
            }

            var certTypes = db.CertTypes;
            ViewBag.typeid = new SelectList(certTypes, "id", "name");
            return View(model);
        }

        public ActionResult AutocompleteCompanyName(string term)
        {
            var items = db.AspNetUsers.Where(x => x.AspNetRoles.FirstOrDefault().Id == "2" && x.Name.Contains(term)).Select(x =>  x.Name ).ToList();

            //var filteredItems = items.Where(
            //    item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            //    );
            return Json(items.Take(10), JsonRequestBehavior.AllowGet);
        }
    }
}