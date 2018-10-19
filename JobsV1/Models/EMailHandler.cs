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

                string body,message;
                if (mailType == "ADMIN")
                {
                    //mail content for admin
                    message = "A NEW Reservation Inquiry has been made. Please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/CarReservations/Details/" + jobId.ToString() + "' ";
                }
                else if(mailType == "PAYMENT-SUCCESS")
                {
                    md.Subject = "Reservation Payment SUCCESS";   //mail title

                    //mail content for successful payment
                    message = "Paypal Payment is SUCCESS. Please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/CarReservations/Details/" + jobId.ToString() + "' ";
                }
                else if (mailType == "PAYMENT-DENIED")
                {
                    md.Subject = "Reservation Payment DENIED";   //mail title

                    //mail content for denied payment
                    message = "Paypal Payment have been DENIED. Please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/CarReservations/Details/" + jobId.ToString() + "' ";
                }
                else if (mailType == "PAYMENT-PENDING")
                {
                    md.Subject = "Reservation Payment PENDING";   //mail title

                    //mail content for pending payment
                    message = "Paypal Payment is still PENDING. Please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/CarReservations/Details/" + jobId.ToString() + "' ";
                }
                else if (mailType == "CLIENT-PENDING")
                {
                    md.Subject = "Reservation Payment PENDING";   //mail title

                    //mail content for pending payment
                    message = "We are happy to recieved your inquiry. We will contact you after we have processed your reservation. Thank you. <a   ";
                }
                else
                {
                    //mail content for client inquiries
                    message = " Your inquiry have been processed to confirm your reservation, please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/JobOrder/BookingDetails/" + jobId.ToString() + "?iType=1' ";
                }

                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:50px' align='center'>" +
                    " <div style='background-color:white;margin:50px;padding:50px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Click here </a> " +
                    " </div></div>" +
                    "";

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