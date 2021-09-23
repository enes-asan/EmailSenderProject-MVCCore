using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderProject.EmailServices
{
    public interface IEmailSender
    {
        void SendEmailRegister(Message message);
    }
}
