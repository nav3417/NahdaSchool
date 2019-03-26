using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using AlnahdAweeklyPlans.Models;

namespace AlnahdAweeklyPlans
{
    public class WebUtil
    {
        public const string File = "file";
        public const string ParentId = "ParentId";
        public const string SubParentId = "SubParentId";
        public const string Controller = "controller";
        public const string CategoryId = "categoryId";
        public const string BasePath = "~/Images/LessonPlan/";
        public static void sentmail(string rmsg, string createddate,string category,string fordate, string clas,string subject)
        {
            var fromAddress = new MailAddress("alnahdatesting@gmail.com", "From HOS");
            var toAddress = new MailAddress("alnahdatesting@gmail.com", "To Teacher");
            const string fromPassword = "alnahda@3800";
            string teacher = "Naveed";
            string HOS = "Naveed";
            string body = "The Record was  on " + createddate +" for the date of "+createddate+" having Type of "+category+", subject "+subject+" and class "+clas+" for date "+fordate+ " BY " + teacher + " Has been " + rmsg + " By " + HOS;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            
        }
        public static void sentmailDigital(string rmsg, string CreatedOn, string category, string fromdate, string todate, string AdminRemards)
        {
            var fromAddress = new MailAddress("alnahdatesting@gmail.com", "From HOS");
            var toAddress = new MailAddress("alnahdatesting@gmail.com", "To Teacher");
            const string fromPassword = "alnahda@3800";
            string teacher = "Naveed";
            string HOS = "Naveed";
            string body = "The Record was  on " + CreatedOn + " for the date of " + CreatedOn + " having Type of " + category +" from date " + fromdate+" To date " + todate + " BY " + teacher + " Has been " + rmsg + " By " + HOS+" having Admin Remarks: "+AdminRemards;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = rmsg,
                Body = body
            })
            {
                smtp.Send(message);
            }

        }
        public static bool SchoolToParent(string To ,List<sendemail> list, string Body, string subject, List<string> File)
        {
            try
            {
                var fromAddress = new MailAddress("alnahdatesting@gmail.com", "From School");
                var toAddress = new MailAddress(To, "To Parent");
                const string fromPassword = "alnahda@3800";
                string body = Body;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000,
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Body = body,
                    Subject = subject,
                    IsBodyHtml=true,

                })
                {
                    foreach (var i in File)
                    {
                        ContentType contentType = new ContentType();
                        contentType.MediaType = MediaTypeNames.Application.Octet;
                        message.Attachments.Add(new Attachment(i, contentType));
                    }
                    //smtp.UseDefaultCredentials = true;
                    //message.bc
                    foreach (var i in list)
                    {
                        message.Bcc.Add(i.Email);
                    }
                    smtp.Send(message);     
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}