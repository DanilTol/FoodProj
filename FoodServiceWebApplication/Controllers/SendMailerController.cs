using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FoodServiceWebApplication.Models;

namespace FoodServiceWebApplication.Controllers
{
    public class SendMailerController : Controller
    {
        //// GET: SendMailer
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: SendMailer/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: SendMailer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SendMailer/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SendMailer/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: SendMailer/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SendMailer/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: SendMailer/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MailModel objModelMail, HttpPostedFileBase fileUploader)
        {
            //if (ModelState.IsValid)
            //{
            //    string from = "mark.peters.jnr@gmail.com"; //example:- sourabh9303@gmail.com
            //    using (MailMessage mail = new MailMessage(from, objModelMail.To))
            //    {
            //        mail.Subject = objModelMail.Subject;
            //        mail.Body = objModelMail.Body;
            //        if (fileUploader != null)
            //        {
            //            string fileName = Path.GetFileName(fileUploader.FileName);
            //            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
            //        }
            //        mail.IsBodyHtml = false;
            //        SmtpClient smtp = new SmtpClient();
            //        smtp.Host = "smtp.gmail.com";
            //        smtp.EnableSsl = true;
            //        NetworkCredential networkCredential = new NetworkCredential(from, "27022007qq");
            //        //smtp.UseDefaultCredentials = true;
            //        smtp.Credentials = networkCredential;
            //        smtp.Port = 587;
            //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        smtp.Send(mail);
            //        ViewBag.Message = "Sent";
            //        return View("Index", objModelMail);
            //    }
            //}
            //else
            //{
            //    return View();
            //}


            SendEmail("second");
            return View();
        }


        public void SendEmail(string body)
        {
            //if (String.IsNullOrEmpty(email))
            //    return;
            try
            {
                MailMessage mail = new MailMessage();
                //mail.To.Add(email);
                mail.To.Add("d.a.tolmachov@gmail.com");
                mail.From = new MailAddress("mark.peters.jnr@gmail.com");
                mail.Subject = "sub";

                mail.Body = body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("mark.peters.jnr@gmail.com", "27022007qq"); // ***use valid credentials***
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send(mail);//Send(mail);
            }
            catch (Exception ex)
            {
                var f = 4;
                //print("Exception in sendEmail:" + ex.Message);
            }
        }




    }
}
