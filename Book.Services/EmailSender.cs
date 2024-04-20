using Book.Business.DTO;
using Book.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EmailSenderAsync(string email, string subject, string message)
        {
            bool status=false;
            try
            {
                GetEmailString s1 = new GetEmailString()
                {
                    key = _configuration.GetValue<string>("AppSettings:key"),
                    From = _configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer=_configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"),
                    Port = _configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = _configuration.GetValue<bool>("AppSettings:EmailSettings:EnableSSL"),
                };
                MailMessage m1 = new MailMessage()
                {
                    From = new MailAddress(s1.From),
                    Subject = subject,
                    Body = message,
                    BodyEncoding = System.Text.Encoding.ASCII,
                    IsBodyHtml = true

                };
                m1.To.Add(email);
                SmtpClient smtp = new SmtpClient(s1.SmtpServer)
                {
                    Port = s1.Port,
                    Credentials = new NetworkCredential(s1.From, s1.key),
                    EnableSsl = s1.EnableSSL
                };
                await smtp.SendMailAsync(m1);
                status = true;




                
            }
            catch (Exception ex)
            {
                status= false ;
            }
            return status;
            }
        }
    }

