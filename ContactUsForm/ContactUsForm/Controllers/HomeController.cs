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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy([Bind("ID, FirstName, LastName, PostalCode, Email, Topic, PhoneNumber, Questions")] Form form)
        {
            if(ModelState.IsValid)
            {
                
            }
            return View();
        }

        public IActionResult Contact()
        {

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

            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
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
