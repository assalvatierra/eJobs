﻿using Newtonsoft.Json.Linq;
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
            var ev = WebhookEvent.Get(apiContext, webhookId);

            // We have all the information the SDK needs, so perform the validation.
            // Note: at least on Sandbox environment this returns false.
            // var isValid = WebhookEvent.ValidateReceivedEvent(apiContext, ToNameValueCollection(requestheaders), requestBody, webhookId);

            //DB.addTestNotification();
            switch (ev.event_type)
            {
                case "PAYMENT.CAPTURE.COMPLETED":
                    // Handle payment completed
                    DB.addTestNotification(5,webhookId);
                    break;
                case "PAYMENT.CAPTURE.DENIED":
                    // Handle payment denied
                    DB.addTestNotification(4, webhookId);
                    break;
                // Handle other webhooks
                default:
                    // Handle payment denied
                    DB.addTestNotification(3, webhookId);
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
    }
}