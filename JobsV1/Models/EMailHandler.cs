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
        private JobDBContainer db = new JobDBContainer();

        public string SendMail(int jobId, string renterMail, string mailType, string renterName, string site)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server

                MailDefinition md = new MailDefinition();
                // md.From = "admin@realwheelsdavao.com";      //sender mail
                md.From = "Realwheels.Reservation@RealWheelsDavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";      //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body, message;
                string siteName = site;
                //get job details

                md.Subject = renterName + ": NEW RealWheels Reservation";   //mail title

                if (mailType == "ADMIN")
                {
                    //mail content for admin
                    //id = carReservation Id

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    message = "A NEW Reservation Inquiry has been made. Please follow the link for the reservation details. <a href='" + siteName + "/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> Reservation Details  </a> ";
                }
                else if (mailType == "PAYMENT-SUCCESS")
                {
                    //mail content for successful payment
                    md.Subject = renterName + ": Paypal Reservation SUCCESS";   //mail title

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //id = carReservation Id
                    message = "Paypal Payment is SUCCESS. Please follow the link for the reservation details. <a href='" + siteName + "/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> Reservation Details </a> ";
                }
                else if (mailType == "PAYMENT-DENIED")
                {
                    //mail content for denied payment
                    //mail title
                    md.Subject = renterName + ": Paypal Reservation DENIED";

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //id = carReservation Id
                    message = "Paypal Payment have been DENIED. Please follow the link for the reservation details. <a href='" + siteName + "/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> Reservation Details </a> ";
                }
                else if (mailType == "PAYMENT-PENDING")
                {
                    //mail content for pending payment
                    //mail title
                    md.Subject = renterName + ": Paypal Reservation PENDING";

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //id = carReservation Id
                    message = "Paypal Payment has been sent. Please follow the link for the reservation details. <a href='" + siteName + "/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> Reservation Details </a> ";
                }
                else if (mailType == "CLIENT-PENDING")
                {
                    //mail title
                    md.Subject = "Realwheels Reservation";

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //mail content for pending payment
                    message = "We are happy to recieved your inquiry. We will contact you after we have processed your reservation. Please click the link below for your reservation details, Thank you. <a href='" + siteName + "/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> View Booking Details </a> ";
                }
                else
                {
                    //send email in /joborder
                    JobMain job = db.JobMains.Find(jobId);
                    //mail title
                    md.Subject = "Realwheels Reservation";

                    //mail content for client inquiries

                    message = " Your inquiry have been processed to confirm your reservation, please follow the link for the invoice and payment. <a href='" + siteName + "/" + jobId + "/" + job.JobDate.Month + "/" + job.JobDate.Day + "/" + job.JobDate.Year + "/" + job.Description + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> View Booking Details </a> ";
                }

                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:20px' align='center'>" +
                    " <div style='background-color:white;min-width:200px;margin:30px;padding:30px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
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

        public string SendMail2(int jobId, string renterMail, string mailType, string renterName, string site, string errorText)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server

                MailDefinition md = new MailDefinition();
                md.From = "Realwheels.Reservation@RealWheelsDavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";      //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body, message;
                string siteName = site;
                //get job details

                md.Subject = renterName + ": NEW RealWheels Reservation";   //mail title
                
                    //send email in /joborder
                    JobMain job = db.JobMains.Find(jobId);
                    //mail title
                    md.Subject = "Realwheels Reservation : " + errorText;

                    //mail content for client inquiries

                    message = errorText + " Your inquiry have been processed to confirm your reservation, please follow the link for the invoice and payment. <a href='" + siteName + "/" + jobId + "/" + job.JobDate.Month + "/" + job.JobDate.Day + "/" + job.JobDate.Year + "/" + job.Description + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> View Booking Details </a> ";
                
                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:20px' align='center'>" +
                    " <div style='background-color:white;min-width:250px;margin:30px;padding:30px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
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


        public string SendMailPayment(int jobId, string renterMail, string mailType, string renterName, string site)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server

                MailDefinition md = new MailDefinition();
                md.From = "Realwheels.Reservation@RealWheelsDavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";      //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body, message;
                string siteName = site;
                //get job details
                
                //send email in /joborder
                JobMain job = db.JobMains.Find(jobId);
                //mail title
                md.Subject = "Realwheels New Payment " ;

                //mail content for client inquiries

                message = " A new payment has been made from a customer. Please follow the link for the invoice and payment. <a href='" + siteName + "/" + jobId + "/" + job.JobDate.Month + "/" + job.JobDate.Day + "/" + job.JobDate.Year + "/" + job.Description + "' " +
                " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> View Booking Details </a> ";

                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:20px' align='center'>" +
                    " <div style='background-color:white;min-width:250px;margin:30px;padding:30px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
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


        public string SendMailClientPayment(int jobId, string renterMail, string mailType, string renterName, string site)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server

                MailDefinition md = new MailDefinition();
                md.From = "Realwheels.Reservation@RealWheelsDavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";      //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body, message;
                string siteName = site;
                //get job details

                //send email in /joborder
                JobMain job = db.JobMains.Find(jobId);
                //mail title
                md.Subject = "Realwheels Payment";

                //mail content for client inquiries

                message = "Your payment has been processed, please wait for our confirmation. Please follow the link for the invoice and payment. <a href='" + siteName + "/" + jobId + "/" + job.JobDate.Month + "/" + job.JobDate.Day + "/" + job.JobDate.Year + "/" + job.Description + "' " +
                " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> View Booking Details </a> ";

                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:20px' align='center'>" +
                    " <div style='background-color:white;min-width:200px;margin:30px;padding:30px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
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

        public string SendMailClientInvoice(int jobId, string renterMail, string mailType, string renterName, string site)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server

                MailDefinition md = new MailDefinition();
                md.From = "Realwheels.Reservation@RealWheelsDavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";      //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body, message;
                string siteName = site;
                //get job details

                //send email in /joborder
                JobMain job = db.JobMains.Find(jobId);
                //mail title
                md.Subject = "Realwheels Payment";

                //mail content for client inquiries

                message = "Please follow the link for the invoice and payment of your inquiry. <a href='" + siteName + "/" + jobId + "/" + job.JobDate.Month + "/" + job.JobDate.Day + "/" + job.JobDate.Year + "/" + job.Description + "' " +
                " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;min-width:100px;'> View Booking Details </a> ";

                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:20px' align='center'>" +
                    " <div style='background-color:white;min-width:200px;margin:20px;padding:30px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
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