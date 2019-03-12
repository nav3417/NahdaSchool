using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

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
    }
}