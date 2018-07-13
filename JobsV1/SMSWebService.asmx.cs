using System;
using System.Linq;
using System.Web.Services;
using Newtonsoft.Json;
using JobsV1.Models;
using System.Data;
using System.Data.Entity;

namespace JobsV1
{
    /// <summary>
    /// Summary description for SMSWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SMSWebService : System.Web.Services.WebService
    {
        //database
        private JobDBContainer db = new JobDBContainer();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void GetNotification(int jobId)
        {

            string sData = "Pickup Details";

            Models.JobServicePickup svcpu;
            Models.JobServices svc = db.JobServices.Find(jobId);

            if (svc.JobServicePickups.FirstOrDefault() == null)
            {
                sData += "\nPickup: undefined ";
            }
            else
            {
                Decimal quote = (svc.QuotedAmt == null ? 0 : (decimal)svc.QuotedAmt);

                svcpu = svc.JobServicePickups.FirstOrDefault();
                sData += "\nDate:" + ((DateTime)svc.DtStart).ToString("dd MMM yyyy (ddd)");
                sData += "\nTime&Location:" + svcpu.JsTime + " " + svcpu.JsLocation;
                sData += "\nGuest:" + svcpu.ClientName + " #" + svcpu.ClientContact;
                sData += "\nDriver:" + svcpu.ProviderName + " #" + svcpu.ProviderContact;
                sData += "\nUnit:" + svc.SupplierItem.Description + " " + svc.SupplierItem.Remarks;
                sData += "\nRate:P" + quote.ToString("##,###.00");
                sData += "\nParticulars:" + svc.Particulars;
                sData += "\n  " + svc.Remarks;
                sData += "\n\nHave a safe trip,\nAJ88 Car Rental";

                //create table for the message
                DataTable Dt = new DataTable("Table");
                //columns for the table
                Dt.Columns.Add("Id", typeof(int));
                Dt.Columns.Add("DtSchedule", typeof(string));
                Dt.Columns.Add("Message", typeof(string));
                Dt.Columns.Add("Recipient", typeof(string));
                Dt.Columns.Add("RecType", typeof(string));

                //add for the client, driver, admin
                int Id = 0;
                string dtSchedule = ((DateTime)svc.DtStart).ToString("dd MMM yyyy");
                string msg = sData;
                string recipients = svcpu.ProviderContact;
                Dt.Rows.Add(Id, dtSchedule, msg, recipients);

                //driver
                Id = 0;
                //the same schedule
                //the same msg
                recipients = svcpu.ClientContact;
                Dt.Rows.Add(Id, dtSchedule, msg, recipients);

                //admin
                Id = 0;
                //the same schedule
                //the same msg
                recipients = "09279016517"; //admin contact
                Dt.Rows.Add(Id, dtSchedule, msg, recipients);


                DataSet ds = new DataSet();
                ds.Tables.Add(Dt);
                ds.DataSetName = "Table";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented));

            }

        }

        [WebMethod]
        public string AddNotification(int jobServiceId)
        {
            Models.JobServicePickup svcpu;
            Models.JobServices svc = db.JobServices.Find(jobServiceId);

            svcpu = svc.JobServicePickups.FirstOrDefault();

            //add notification
            JobNotificationRequest jnr = new JobNotificationRequest();
            db.JobNotificationRequests.Add(new JobNotificationRequest
            {
                RefId = "0",    //job id
                ReqDt = DateTime.Parse(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss")),
                ServiceId = jobServiceId
            });

            db.SaveChanges();
            return "Notification added to the list";

        }

        [WebMethod]
        public void getUnsentItems()
        {
            JobNotificationRequest jnr;

            if (db.JobNotificationRequests.Where(r => r.RefId.Equals("0")).FirstOrDefault() == null)
            {
                //return none
            }
            else
            {
                jnr = db.JobNotificationRequests.Where(r => r.RefId.Equals("0")).FirstOrDefault();

                var svcId = jnr.ServiceId;
                string sData = "Pickup Details";

                Models.JobServicePickup svcpu;
                Models.JobServices svc = db.JobServices.Find(svcId);

                if (svc.JobServicePickups.FirstOrDefault() == null)
                {
                    sData += "\nPickup: undefined ";
                    Console.WriteLine("\nPickup: undefined");
                }
                else
                {
                    Decimal quote = (svc.QuotedAmt == null ? 0 : (decimal)svc.QuotedAmt);
                    Console.WriteLine("new added");
                    svcpu = svc.JobServicePickups.FirstOrDefault();
                    sData += "\nDate:" + ((DateTime)svc.DtStart).ToString("dd MMM yyyy (ddd)");
                    sData += "\nTime&Location:" + svcpu.JsTime + " " + svcpu.JsLocation;
                    sData += "\nGuest:" + svcpu.ClientName + " #" + svcpu.ClientContact;
                    sData += "\nDriver:" + svcpu.ProviderName + " #" + svcpu.ProviderContact;
                    sData += "\nUnit:" + svc.SupplierItem.Description;
                    //sData += "\nRate:P" + quote.ToString("##,###.00");
                    // sData += "\nParticulars:" + svc.Particulars;
                    // sData += "\n  " + svc.Remarks;
                    // sData += "\n\nHave a safe trip,\nAJ88 Car Rental";

                    //create table for the message
                    DataTable Dt = new DataTable("Table");
                    //columns for the table
                    Dt.Columns.Add("RequestId", typeof(string));
                    Dt.Columns.Add("ServiceId", typeof(string));
                    Dt.Columns.Add("DtSchedule", typeof(string));
                    Dt.Columns.Add("Message", typeof(string));
                    Dt.Columns.Add("ClientName", typeof(string));
                    Dt.Columns.Add("Recipient", typeof(string));
                    Dt.Columns.Add("RecType", typeof(string));

                    //add for the client, driver, admin
                    int Id = jnr.Id;
                    string adminContact = "09279016517";
                    int ServiceId = svcId;
                    string clientName = svcpu.ClientName;
                    string dtSchedule = ((DateTime)svc.DtStart).ToString("dd MMM yyyy");
                    string msg = sData;
                    string recipients = svcpu.ProviderContact + "," + svcpu.ClientContact + "," + adminContact;

                    Dt.Rows.Add(Id, ServiceId, dtSchedule, msg, clientName, recipients, "request");


                    DataSet ds = new DataSet();
                    ds.Tables.Add(Dt);
                    ds.DataSetName = "Table";

                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write(JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented));

                }
            }
        }//eof


        [WebMethod]
        public string updateNotification(int requestID, int refId)
        {

                JobNotificationRequest jnr = db.JobNotificationRequests.Where(n => n.Id.Equals(requestID)).FirstOrDefault();
                jnr.RefId = refId.ToString();
                db.Entry(jnr).State = EntityState.Modified;
                db.SaveChanges();
                return "successful";

        }
    }
}
