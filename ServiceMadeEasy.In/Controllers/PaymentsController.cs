using ServiceMadeEasy.In.Models;
using paytm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceMadeEasy.In.Controllers
{
    [Authorize]
    public class PaymentsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Payment(string ApplicationUserId, string email, string mobileNo, string amount, int packageId)
        {
            string customeId = ApplicationUserId;
            
            string Authority = ((System.Web.HttpRequestWrapper)Request).Url.Authority;
            string baseURL = "http://" + Authority;

            string orderId = Guid.NewGuid().ToString() + "_" + packageId.ToString();

            String merchantKey = "56Fpx#9kHB3OUyhX"; // // Rajesh change for customer
            //String merchantKey = "H5s&dn@OdPMnW4tV"; // change for customer
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "UlNquI78945631265421"); // Babalu // change for customer
            //parameters.Add("MID", "vQWyUz47251645613658"); // change for customer
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", email);
            parameters.Add("MOBILE_NO", mobileNo);
            parameters.Add("CUST_ID", customeId);
            parameters.Add("ORDER_ID", orderId);
            parameters.Add("TXN_AMOUNT", amount.ToString());
            parameters.Add("CALLBACK_URL", baseURL + "/Payments/PaymentStatus"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            //for
            //string paytmURL = "https://securegw.paytm.in/theia/processTransaction?orderid=" + orderId;
            //https://securegw.paytm.in/theia/processTransaction production url
            //https://securegw.paytm.in/theia/processTransaction?orderid=
            //https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=ORDER_ID
            //https://staging-dashboard.paytm.com/
            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderId;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            TempData["outputHTML"] = outputHTML;

            return RedirectToAction("MakePayment");

        }

        [HttpPost]
        public ActionResult PaymentStatus(PaymentResponseViewModel paymentResponseViewModel)
        {
            //String merchantKey = "H5s&dn@OdPMnW4tV";  // ConfigurationManager.AppSettings["MerchantKey"]; // Replace the with the Merchant Key provided by Paytm at the time of registration.
            String merchantKey = "56Fpx#9kHB3OUyhX"; // Babalu
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string paytmChecksum = "";
            foreach (string key in Request.Form.Keys)
            {
                parameters.Add(key.Trim(), Request.Form[key].Trim());
            }

            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }

            bool isActive = false;
            bool isCheckSumSuccess = false;
            bool isChecksumError = false;

            try
            {
                isCheckSumSuccess = CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum);
            }
            catch (Exception ex)
            {
                if (paymentResponseViewModel.STATUS == "TXN_SUCCESS")
                {
                    isCheckSumSuccess = true;
                }
                isChecksumError = true;
            }

            if (isCheckSumSuccess && paymentResponseViewModel.STATUS == "TXN_SUCCESS")
            {
                int lastIndex = paymentResponseViewModel.ORDERID.LastIndexOf("_");

                int packageId = Convert.ToInt32(paymentResponseViewModel.ORDERID.Substring(lastIndex));

                double actualAmount = 0.0; //db.Packages.Where(id => id.Id == packageId).FirstOrDefault().Price;

                lastIndex = paymentResponseViewModel.TXNAMOUNT.LastIndexOf('.');

                double txnAmount = Convert.ToDouble(paymentResponseViewModel.TXNAMOUNT.Remove(lastIndex));

                if (actualAmount == txnAmount)
                {
                    isActive = true;
                    ViewBag.outputHTML = "Checksum Matched";
                }
                else
                {
                    isActive = false;
                    paymentResponseViewModel.STATUS = paymentResponseViewModel.STATUS + "AMOUNT_CHANGED";
                    ViewBag.outputHTML = "Checksum Matched";
                }
            }
            else
            {
                isActive = false;
                ViewBag.outputHTML = "Checksum MisMatch";
            }

            if (User.Identity.Name != null)
            {
                var user = db.Users.FirstOrDefault(m => m.Email == User.Identity.Name);
                if (user != null)
                {
                    //ApplicationUserPackage ApplicationUserPackage = new ApplicationUserPackage();
                    //ApplicationUserPackage.ApplicationUserId = user.Id;
                    //ApplicationUserPackage.PackageId = 1;// Convert.ToInt32(TempData.Peek("PackageId"));
                    ////ApplicationUserPackage.Email = packageViewModel.Email;
                    ////ApplicationUserPackage.IsChecksumError = isChecksumError;
                    //ApplicationUserPackage.IsActive = isActive;
                    //ApplicationUserPackage.MID = paymentResponseViewModel.MID;
                    //ApplicationUserPackage.TXNID = paymentResponseViewModel.TXNID;
                    //ApplicationUserPackage.ORDERID = paymentResponseViewModel.ORDERID;
                    //ApplicationUserPackage.BANKTXNID = paymentResponseViewModel.BANKTXNID;
                    //ApplicationUserPackage.TXNAMOUNT = paymentResponseViewModel.TXNAMOUNT;
                    //ApplicationUserPackage.CURRENCY = paymentResponseViewModel.CURRENCY;
                    //ApplicationUserPackage.PAYMENTMODE = paymentResponseViewModel.PAYMENTMODE;
                    //ApplicationUserPackage.TXNDATE = paymentResponseViewModel.TXNDATE;
                    //ApplicationUserPackage.STATUS = paymentResponseViewModel.STATUS;
                    //ApplicationUserPackage.RESPCODE = paymentResponseViewModel.RESPCODE;
                    //ApplicationUserPackage.RESPMSG = paymentResponseViewModel.RESPMSG;
                    //ApplicationUserPackage.GATEWAYNAME = paymentResponseViewModel.GATEWAYNAME;
                    //ApplicationUserPackage.BANKNAME = paymentResponseViewModel.BANKNAME;
                    //ApplicationUserPackage.CHECKSUMHASH = paymentResponseViewModel.CHECKSUMHASH;
                    //ApplicationUserPackage.Created = DateTime.IndiaDateTime();
                    //ApplicationUserPackage.Updated = DateTime.IndiaDateTime();
                    //ApplicationUserPackage.IsActive = true;
                    //db.ApplicationUserPackages.Add(ApplicationUserPackage);
                    //db.SaveChanges();
                    //return View(ApplicationUserPackage);

                    return View();
                }
            }

            return RedirectToAction("MakePayment");
        }

        public ActionResult MakePayment()
        {
            if (User.Identity.Name != null)
            {
                //var user = db.ApplicationUsers.FirstOrDefault(m => m.Email == User.Identity.Name);
                //if (user != null)
                //{
                //    TempData["memberName"] = user.Name;
                //    foreach (var item in user.Roles)
                //    {
                //        TempData["RoleId"] = item.RoleId;
                //    }
                //}
            }
            return View();
        }

    }
}