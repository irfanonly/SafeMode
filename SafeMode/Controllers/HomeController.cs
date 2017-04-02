using SafeMode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using System.Net.Mail;
using System.Net;

namespace SafeMode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel contactViewModel)
        {
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                //ModelState.AddModelError("", "Captcha is not valid");
                //return View(contactViewModel);
            }
            if (ModelState.IsValid)
            {
                string reciever = "jahan119@gmail.com";

                string message = "Sender : " + contactViewModel.Name+ "(" + contactViewModel.Mail + ")" 
                    + "<br/>" + "Website : " + contactViewModel.Mail 
                    + "<br/><br/>" + contactViewModel.Message.Replace("\n", "<br/>");
                int a =  SendMail(message, "Safe Mode - User Request", reciever);

                if(a == 1)
                {
                    ViewBag.Success = "True";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Mail not send, Try again later.");
                }

               
            }

            ViewBag.Error = "True";

            return View(contactViewModel);
        }

        [AllowAnonymous]
        public ActionResult CertificateVerification(string returnUrl, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
               message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
               : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
               : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
               : message == ManageMessageId.Error ? "An error has occurred."
               : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
               : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
               : "";

            if (Request.IsAuthenticated){
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole("user"))
                {
                    return RedirectToAction("Index", "User");
                }
                
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Calendar()
        {

            return View();
        }

        public int SendMail(string body, string subject, string reciever)
        {
            int success = 0;

            try
            {

                MailAddress toAddress = new MailAddress(reciever, reciever);

                SmtpClient smtp = new SmtpClient();
                MailAddress fromAddress = new MailAddress(((NetworkCredential)smtp.Credentials).UserName, "FMDC");
                using (MailMessage message = new MailMessage()
                {
                    Subject = subject,
                    Body = body,

                })

                {
                    message.IsBodyHtml = true;
                    message.From = fromAddress;
                    message.To.Add(new MailAddress(toAddress.ToString()));
                    smtp.Send(message);


                    success = 1;
                }

            }
            catch
            {
                Console.WriteLine("error in sending mail");
                success = 0;
            }

            return success;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
    }
}