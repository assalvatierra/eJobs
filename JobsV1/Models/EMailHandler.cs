using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace JobsV1.Models
{
    public class EMailHandler
    {

        public string SendMail() {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

               // mail.From = new MailAddress("reservation.realwheels@gmail.com");
               // mail.To.Add("jahdielsvillosa@gmail.com");
               // mail.Subject = "Test Mail";
               // mail.IsBodyHtml = true;
               // mail.Body = "<h1>HEADING TEST</h1>"+
                //     "<p> This is for testing SMTP mail from GMAIL using IMAP </p>";

                MailDefinition md = new MailDefinition();
                md.From = "reservation.realwheels@gmail.com";
                md.IsBodyHtml = true;
                md.Subject = "Test of Mail Definition";

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body = 
                    " <h1> Car Rental Reservation </h1>" +
                    " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <p>Please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/CarRental/FormThankYou?rsvId=1'>Click here </a> </p>" +
                    " ";

                MailMessage msg = md.CreateMailMessage("jahdielsvillosa@gmail.com", replacements, body, new System.Web.UI.Control());

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("reservation.realwheels@gmail.com", "realwheels123!");
                SmtpServer.EnableSsl = true;
                System.Net.ServicePointManager.Expect100Continue = false;
                SmtpServer.Send(msg);
                return "success";
            }
            catch (Exception ex)
            {
                return "error: " + ex;
            }
        }
    }
}