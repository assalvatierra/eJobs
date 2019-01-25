﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace JobsV1.Models
{
    public class EmailBlaster
    {

        private JobDBContainer db = new JobDBContainer();

        public string SendMailBlaster( string renterMail)
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

                string body, message;

                //mail title
                md.Subject = "RealWheels ";  
                
                //mail subject
                md.Subject = "Realwheels Newsletter : ";
                
                //mail content for client inquiries
                message ="";
                
                body =
                    "" +
                    " <div style='background-color:#f4f4f4;padding:20px' align='center'>" +
                    " <div style='background-color:white;min-width:250px;margin:30px;padding:30px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
                    " <p> For further inquiries kindly email us through ajdavao88@gmail.com or dial(+63) 82 297 1831. </p> " +
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