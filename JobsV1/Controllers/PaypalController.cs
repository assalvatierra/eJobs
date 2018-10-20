using Newtonsoft.Json.Linq;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using JobsV1.Models;
using System.Web.Http.Cors;

namespace JobsV1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PaypalController : Controller
    {
        DBClasses DB = new DBClasses();

        JobDBContainer db = new JobDBContainer();

        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult Webhook()
        {
            // The APIContext object can contain an optional override for the trusted certificate.
            var apiContext = PayPalConfiguration.GetAPIContext();

            // Get the received request's headers
            var requestheaders = HttpContext.Request.Headers;

            // Get the received request's body
            var requestBody = string.Empty;
            using (var reader = new System.IO.StreamReader(HttpContext.Request.InputStream))
            {
                requestBody = reader.ReadToEnd();
            }

            dynamic jsonBody = JObject.Parse(requestBody);
            string webhookId = jsonBody.id;
            string paypalID = jsonBody.resource.id;
            decimal Totalamount = (decimal)jsonBody.resource.amount.total;
            int jobId = (int)jsonBody.resource.invoice_number;
            var ev = WebhookEvent.Get(apiContext, webhookId);

            // We have all the information the SDK needs, so perform the validation.
            // Note: at least on Sandbox environment this returns false.
            // var isValid = WebhookEvent.ValidateReceivedEvent(apiContext, ToNameValueCollection(requestheaders), requestBody, webhookId);

            // DB.addTestNotification(jobId, paypalID);
            //get job description
            JobMain jobOrder = db.JobMains.Find(jobId);
            string clientName = jobOrder.Description;

            EMailHandler mail = new EMailHandler();

            switch (ev.event_type)
            {
                case "PAYMENT.cAPTURE.COMPLETED":
                case "PAYMENT.SALE.COMPLETED":
                    // Handle payment completed

                    //add to log
                    DB.addTestNotification(jobId, paypalID);

                    //record payment
                    AddPaymentRecord(jobId, Totalamount);

                    //send mail
                    mail.SendMail(jobId, "reservation.realwheels@gmail.com", "PAYMENT-SUCCESS", clientName);
                    mail.SendMail(jobId, "aj88davao@gmail.com", "PAYMENT-PENDING", clientName);
                    mail.SendMail(jobId, "travel.realbreze@gmail.com", "PAYMENT-PENDING", clientName);
                    break;
                case "PAYMENT.SALE.DENIED":
                case "PAYMENT.CAPTURE.DENIED":
                    // Handle payment denied

                    //add to log
                    DB.addTestNotification(jobId, paypalID);

                    //send mail
                    mail.SendMail(jobId, "jahdielsvillosa@gmail.com", "PAYMENT-DENIED", clientName);
                    mail.SendMail(jobId, "aj88davao@gmail.com", "PAYMENT-PENDING", clientName);
                    mail.SendMail(jobId, "travel.realbreze@gmail.com", "PAYMENT-PENDING", clientName);
                    break;
                // Handle other webhooks
                default: // Handle payment denied
                    
                    //add to log
                    DB.addTestNotification(jobId, paypalID);

                    //send mail
                    mail.SendMail(jobId, "jahdielsvillosa@gmail.com", "PAYMENT-PENDING", clientName);
                    mail.SendMail(jobId, "aj88davao@gmail.com", "PAYMENT-PENDING", clientName);
                    mail.SendMail(jobId, "travel.realbreze@gmail.com", "PAYMENT-PENDING", clientName);
                    break;
            }

            return new HttpStatusCodeResult(200);
        }

        public static class PayPalConfiguration
        {
            public readonly static string ClientId;
            public readonly static string ClientSecret;

            // Static constructor for setting the readonly static members.
            static PayPalConfiguration()
            {
                var config = GetConfig();
                ClientId = config["clientId"];
                ClientSecret = config["clientSecret"];
            }

            // Create the configuration map that contains mode and other optional configuration details.
            public static Dictionary<string, string> GetConfig()
            {
                // ConfigManager.Instance.GetProperties(); // it doesn't work on ASPNET 5
                return new Dictionary<string, string>() {
                { "clientId", "AeKvfmAZjDaTJ4bH4PFGurLMvFZOl9OeHaK6xUlSCB0Ny8RU2WEeijZLTeRGvz0GjQXrX1SuaYvf53-H" },
                { "clientSecret", "EASK4ghccZuqU3VDsEwA9WzEbNWqqtWPJQWXkd1UAcKflTQ1CX1dAvj2ZyKcE_nILs2ewK0rQkJ85hAX" }
            };
            }

            // Create accessToken
            private static string GetAccessToken()
            {
                // ###AccessToken
                // Retrieve the access token from OAuthTokenCredential by passing in
                // ClientID and ClientSecret
                // It is not mandatory to generate Access Token on a per call basis.
                // Typically the access token can be generated once and reused within the expiry window                
                string accessToken = new OAuthTokenCredential
                    (ClientId, ClientSecret, GetConfig()).GetAccessToken();
                return accessToken;
            }

            // Returns APIContext object
            public static APIContext GetAPIContext(string accessToken = "")
            {
                // Pass in a `APIContext` object to authenticate 
                // the call and to send a unique request id 
                // (that ensures idempotency). The SDK generates
                // a request id if you do not pass one explicitly. 
                var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ?
                    GetAccessToken() : accessToken);
                apiContext.Config = GetConfig();

                return apiContext;
            }
        }

        //add paypal payment to job payment (full/partial) 
        private void AddPaymentRecord(int JobMainId, decimal amount ) {
            string remarks = "PayPal Payment";
            JobPayment jobPayment = new JobPayment();
            jobPayment.BankId = 4;                      //personal guarantee, need to add (5) paypal
            jobPayment.DtPayment = DateTime.Now;
            jobPayment.JobMainId = (int)JobMainId;
            jobPayment.PaymentAmt = amount;
            jobPayment.Remarks = remarks;

            db.JobPayments.Add(jobPayment);
            db.SaveChanges();
        }
    }
}