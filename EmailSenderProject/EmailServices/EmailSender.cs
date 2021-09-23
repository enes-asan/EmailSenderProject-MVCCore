using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailSenderProject.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void SendEmailRegister(Message message)
        {
            var emailMessage = CreateEmailMessageRegister(message);
            Send(emailMessage);
        }

        private void Send(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(emailMessage);
                }
                catch
                {
                    throw new Exception();
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        //NETCore.MailKit pack download
        private MimeMessage CreateEmailMessageRegister(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format("<div style='background-color:lightblue;padding:20px;'>" +
                                         "<h2 style='color:green'>Confirming your e-mail address</h2>" +
                                         "<p>Hello, you need to confirm your email address to use your account on yoursiteadress.com site.</p>" +
                                          "<h3>For email address confirmation <a href ='{0}' style='color:red;text-aling=center;' > here </ a > click here.</h3>" +
                                     "</div>", message.Content)
            };
            return emailMessage;
        }
    }
}
