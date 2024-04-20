using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface IEmailSender
    {
        Task<bool>  EmailSenderAsync(string email,string subject,string message);
    }
}
