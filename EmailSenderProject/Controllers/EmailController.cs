using EmailSenderProject.EmailServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderProject.Controllers
{
    public class EmailController : Controller
    {
        IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public string EmailSend()
        {
            //Note=https://myaccount.google.com/lesssecureapps?pli=1 The security of the sending address must be turned off. otherwise it will give an error.
            //Note=asppsettings.json see settings

            var message = new Message(new string[] { "receiverMailAdress@gmail.com" }, "e-mail is being done", String.Format("{0}://{1}{2}", Request.Scheme, Request.Host, "Url"));
            _emailSender.SendEmailRegister(message);
            return "email sent";
        }
    }
}
