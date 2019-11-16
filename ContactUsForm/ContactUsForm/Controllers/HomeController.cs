using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactUsForm.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace ContactUsForm.Controllers
{
    public class HomeController : Controller
    {
        const string PASS = "shelob2000";
        private readonly ILogger<HomeController> _logger;
        private readonly ContactUsFormContext _context;

        public HomeController(ContactUsFormContext context)
        {
            _context = context;
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
         
            return View();
        }

       
        public IActionResult Contact()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("ID, FirstName, LastName, PostalCode, Email, Topic, PhoneNumber, Questions, TimeStamp")] Form form)
        {
            if (ModelState.IsValid)
            {
                bool success = SendEmail(form.ID, form.FirstName, form.LastName, form.PostalCode, form.Email, form.Topic, form.PhoneNumber, form.Questions);

                if (success)
                {
                    ViewBag.message = "Thank you " + form.FirstName + form.LastName + " your email was sent";
                    _context.Add(form);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewBag.message = "Error your email has not been sent";
               
                }
            }
            return View();
        }

        private bool SendEmail(int id, string fName, string lName, string postalCode, string email, string topic, string phoneNumber, string questions)
        {
            bool success = false;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CoolBixes", "coolbixes420@gmail.com"));
            message.To.Add(new MailboxAddress("CoolBixes", email));
            message.To.Add(new MailboxAddress("CoolBixes", "coolbixes420@gmail.com"));
            message.ReplyTo.Add(new MailboxAddress("CoolBixes", "coolbixes420@gmail.com"));

            message.Subject = topic;
            message.Body = new TextPart("plain")
            {
                Text = questions
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.Auto);
                client.Authenticate("coolbixes420@gmail.com", PASS);
                client.Send(message);
                client.Disconnect(true);
                success = true;
            }

            return success;
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
