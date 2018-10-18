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

        public string SendMail(int jobId, string renterMail, string mailType) {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server
                
                MailDefinition md = new MailDefinition();
                md.From = "admin@realwheelsdavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";   //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");
                string body;
                if (mailType == "ADMIN")
                {
                    //mail content
                    body =
                        "" +
                        // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                        " <div style='background-color:#f4f4f4;padding:50px' align='center'>" +
                        " <div style='background-color:white;margin:50px;padding:50px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                        "  A NEW Reservation Inquiry has been made. Please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/CarReservations/Details/" + jobId.ToString() + "' " +
                        " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Click here </a> " +
                        " </div></div>" +
                        "";
                }
                else if(mailType == "PAYMENT")
                {
                    md.Subject = "Reservation Payment";   //mail title

                    //mail content
                    body =
                        "" +
                        // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                        " <div style='background-color:#f4f4f4;padding:50px' align='center'>" +
                        " <div style='background-color:white;margin:50px;padding:50px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Payment </h1>" +
                        " Paypal payment has been submitted. Please follow the link for the Reservation Details. <a href='https://realwheelsdavao.com/JobOrder/BookingDetails/" + jobId.ToString() + "?iType=1' " +
                        " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Click here </a> " +
                        " </div></div>" +
                        "";

                }
                else
                {
                    //mail content
                    body =
                        "" +
                        // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                        " <div style='background-color:#f4f4f4;padding:50px' align='center'>" +
                        " <div style='background-color:white;margin:50px;padding:50px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                        " We have processed your inquiry. Please follow the link for the Reservation Details. <a href='https://realwheelsdavao.com/JobOrder/BookingDetails/" + jobId.ToString() + "?iType=1' " +
                        " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Click here </a> " +
                        " </div></div>" +
                        "";

                }
                MailMessage msg = md.CreateMailMessage(renterMail, replacements, body, new System.Web.UI.Control());

                SmtpServer.Port = 587;          //default smtp port
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@realwheelsdavao.com", "Real123!");
                SmtpServer.EnableSsl = false;   //enable for gmail smtp server
                System.Net.ServicePointManager.Expect100Continue = false;
                SmtpServer.Send(msg);           //send message
                return "success";
            }
            catch (Exception ex)
            {
                return "error: " + ex;
            }
        }
    }
}