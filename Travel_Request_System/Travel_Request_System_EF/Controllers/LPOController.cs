using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using Travel_Request_System_EF.CustomAuthentication;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.DataAnnotations;
using Travel_Request_System_EF.Models.ViewModel;

namespace Travel_Request_System_EF.Controllers
{
    [HandleError]
    [RedirectingAction]
    public class LPOController : Controller
    {
        private static RFQ MasterRFQ = new RFQ();
        private static MembershipUser user;
        private static Users dbuser;
        private static string[] roles;

        public LPOController()
        {
            try
            {
                user = Membership.GetUser();
                CustomRole customRole = new CustomRole();
                roles = customRole.GetRolesForUser(user.UserName);
                ViewBag.RoleDetails = roles.ToList()[0];
                IsLoggedIn(roles.ToList());
                using (BTCEntities db = new BTCEntities())
                {
                    dbuser = db.Users.Where(a => a.Username == user.UserName).Include(a => a.Roles).Include(a => a.TravelRequests).Include(a => a.TravelRequests1).FirstOrDefault();
                    ViewBag.UserDetails = dbuser;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Your Login has expired!! Please login again.";
            }
        }

        // GET: LPO
        public async Task<ActionResult> Index(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var lpos = db.LPO.Include(a => a.RFQ).Include(a => a.Quotation).Include(a => a.RFQ).Include("RFQ.TravelRequests").Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.RFQ.TravelRequestID == id);
                return View(await lpos.ToListAsync());
            }
        }

        public async Task<ActionResult> LPOProcessing(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = true;
                var RFQList = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.LPO).Include(a => a.TravelAgency).Include(a => a.Users).Include(a => a.Quotation).Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.Processing == (int)ProcessingStatus.BeingProcessed && a.TravelRequestID == id).Distinct().ToListAsync();
                ViewBag.RFQList = RFQList;
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();
                return View(RFQList.ToList());
            }
        }

        public ActionResult GetQuotationList(string RFQID)
        {
            List<QuotationListViewModel> lstquotes = new List<QuotationListViewModel>();
            int RFQIDVal = Convert.ToInt32(RFQID);
            if (RFQIDVal > 0)
            {
                using (BTCEntities db = new BTCEntities())
                {
                    int travelRequestIDVal = (int)db.RFQ.Where(a => a.ID == RFQIDVal).Select(a => a.TravelRequestID).FirstOrDefault();
                    Quotation QuotationVal = db.Quotation.Include(a => a.TravelRequests).Where(a => a.TravelRequestID == travelRequestIDVal).FirstOrDefault();

                    lstquotes.AddRange(db.ATQuotation.Where(a => a.QuotationID == QuotationVal.ID)
                        .Select(a => new QuotationListViewModel
                        {
                            QuotationID = a.QuotationID,
                            RFQID = RFQIDVal,
                            TravelRequestID = travelRequestIDVal,
                            QuotationType = 1,
                            QuotationTypeID = a.ID,
                            QuotationName = QuotationVal.TravelRequests.ApplicationNumber + " / AT - Q"
                        }).ToList());

                    lstquotes.AddRange(db.HSQuotation.Where(a => a.QuotationID == QuotationVal.ID)
                        .Select(a => new QuotationListViewModel
                        {
                            QuotationID = a.QuotationID,
                            RFQID = RFQIDVal,
                            TravelRequestID = travelRequestIDVal,
                            QuotationType = 2,
                            QuotationTypeID = a.ID,
                            QuotationName = QuotationVal.TravelRequests.ApplicationNumber + " / HS - Q"
                        }).ToList());

                    lstquotes.AddRange(db.PCQuotation.Where(a => a.QuotationID == QuotationVal.ID)
                        .Select(a => new QuotationListViewModel
                        {
                            QuotationID = a.QuotationID,
                            RFQID = RFQIDVal,
                            TravelRequestID = travelRequestIDVal,
                            QuotationType = 3,
                            QuotationTypeID = a.ID,
                            QuotationName = QuotationVal.TravelRequests.ApplicationNumber + " / PC - Q"
                        }).ToList());
                }
            }
            else
            {
                lstquotes.Add(new QuotationListViewModel
                {
                    QuotationID = 0,
                    RFQID = RFQIDVal,

                    TravelRequestID = 0,
                    QuotationType = 0,
                    QuotationTypeID = 0,
                    QuotationName = "No Quotations"
                });
            }
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstquotes);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> LPOCreation(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                var lpodata = db.LPO.Where(a => a.ID == id).FirstOrDefault();
                if(lpodata.IsDeleted == null || (bool)lpodata.IsDeleted)
                {
                    lpodata.IsDeleted = false;

                    db.Entry(lpodata).State = EntityState.Modified;

                    db.LPO.Attach(lpodata);
                    db.Entry(lpodata).Property(x => x.IsDeleted).IsModified = true;
                    db.SaveChanges();

                    var rfq = db.RFQ.Where(a => a.ID == lpodata.RFQID).FirstOrDefault();
                    rfq.Processing = (int)ProcessingStatus.Processed;

                    var travelreq = db.TravelRequests.Where(a => a.ID == rfq.TravelRequestID).FirstOrDefault();
                    travelreq.ApprovalLevel = (int)ApprovalLevels.ApprovedbyTravelCo;

                    db.SaveChanges();
                }

                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                var LPO = await db.LPO.Include(a => a.RFQ).Include(a => a.Quotation).Include(a => a.RFQ).Include("RFQ.TravelRequests").Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.RFQID == id).FirstOrDefaultAsync();

                ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/AT-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                ViewBag.HSfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/HS-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                ViewBag.PCfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/PC-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();

                return View(LPO);
            }
        }

        [System.Web.Http.HttpPost]
        public void CreateTravelGrid(Dictionary<string, string> collection)
        {
            List<LPO> lpoList = new List<LPO>();
            List<string> RfqList = new List<string>();

            foreach (var item in collection)
            {
                if (item.Value.ToLower() == "true")
                {
                    RfqList.Add(item.Key.Split('-')[0]);
                }
            }

            foreach (var rfq in RfqList.Distinct())
            {
                LPO lpoObject = new LPO();
                foreach (var item in collection)
                {
                    if (item.Value.ToLower() == "true" && item.Key.Contains(rfq))
                    {
                        lpoObject.RFQID = Convert.ToInt32(item.Key.Split('-')[0].Substring(item.Key.Split('-')[0].IndexOf('Q') + 1));
                        lpoObject.QuotationID = Convert.ToInt32(item.Key.Split('-')[1].Substring(item.Key.Split('-')[1].IndexOf('t') + 1));
                        if (item.Key.Split('-')[2].Contains("ATCheck"))
                        {
                            lpoObject.IsAT = true;
                        }
                        if (item.Key.Split('-')[2].Contains("HSCheck"))
                        {
                            lpoObject.IsHS = true;
                        }
                        if (item.Key.Split('-')[2].Contains("PCCheck"))
                        {
                            lpoObject.IsPC = true;
                        }
                        lpoObject.IsDeleted = true;
                    }
                }
                lpoList.Add(lpoObject);
            }

            using (BTCEntities db = new BTCEntities())
            {
                foreach (var item in lpoList)
                {
                    if (db.LPO.Where(a => a.RFQID == item.RFQID && a.QuotationID == item.QuotationID).Count() > 0)
                    {
                        var lpodata = db.LPO.Where(a => a.RFQID == item.RFQID && a.QuotationID == item.QuotationID).FirstOrDefault();
                        lpodata.IsAT = item.IsAT;
                        lpodata.IsDeleted = item.IsDeleted;
                        lpodata.IsHS = item.IsHS;
                        lpodata.IsPC = item.IsPC;
                        lpodata.LPONo = "LPO - " + lpodata.ID;

                        db.Entry(lpodata).State = EntityState.Modified;
                        db.LPO.Attach(lpodata);
                        db.Entry(lpodata).Property(x => x.IsAT).IsModified = true;
                        db.Entry(lpodata).Property(x => x.IsDeleted).IsModified = true;
                        db.Entry(lpodata).Property(x => x.IsHS).IsModified = true;
                        db.Entry(lpodata).Property(x => x.IsPC).IsModified = true;
                    }
                    else
                    {
                        db.LPO.Add(item);
                    }
                }
                db.SaveChanges();
            }
        }

        public async Task<ActionResult> LPODetails(int id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                ViewBag.TravelAgency = db.TravelAgency.ToList();
                ViewBag.Cities = db.City.ToList();
                ViewBag.Currencies = db.Currency.ToList();
                ViewBag.ApprovalBy = db.Users.ToList();

                var LPO = await db.LPO.Include(a => a.RFQ).Include(a => a.Quotation).Include(a => a.RFQ).Include("RFQ.TravelRequests").Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.ID == id).FirstOrDefaultAsync();
                if (db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/AT-Q")).Count() > 0)
                {
                    ViewBag.ATfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/AT-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                }
                if (db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/HS-Q")).Count() > 0)
                {
                    ViewBag.HSfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/HS-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                }
                if (db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/PC-Q")).Count() > 0)
                {
                    ViewBag.PCfileUploader = db.AttachmentLink.Where(a => a.AttachmentFor.Contains(LPO.RFQ.TravelRequests.ApplicationNumber + "/PC-Q")).Select(x => x.Attachments).Include(a => a.AttachmentLink).Include(a => a.Users).ToList();
                }

                return View(LPO);
            }
        }

        public async Task<ActionResult> TravelRequestsList()
        {
            using (BTCEntities db = new BTCEntities())
            {
                var travelRequests = db.TravelRequests.Include(t => t.City).Include(t => t.City1).Include(t => t.Currency).Include(t => t.Users1).Include(t => t.Users).Where(a => a.IsSubmitted);
                return View(await travelRequests.ToListAsync());
            }
        }

        public void PrintLPO(int id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            var connectionString = ConfigurationManager.ConnectionStrings["AuthenticationDB"].ConnectionString;
            BTCDataSet ds = new BTCDataSet();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            Byte[] bytes;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp;

            adp = new SqlDataAdapter("select * FROM LPODetails WHERE LPOID = " + id, conx);
            ds = new BTCDataSet();
            adp.Fill(ds, ds.LPODetails.TableName);
            reportViewer.LocalReport.ReportPath = Path.Combine(@"C:\Users\kanniyappans\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Reports\", "LPOReport.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("detailsDataset", ds.Tables["LPODetails"]));
            bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= outputreport" + "." + extension);
            Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
        }

        public void SendEmail(int id)
        {
            var username = "john.doe@gmail.com";
            var password = "password";
            MailAddress MailFrom = new MailAddress("john.doe@gmail.com");
            MailAddress MailTo = new MailAddress("john.doe@gmail.com");
            var subject = "TEST SUBJECT";
            var attachmentPath = ExportReportToPDF(id);
            var mailBody = "<b>test</b>";


            NetworkCredential cred = new NetworkCredential(username, password);

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;

            MailMessage mail = new MailMessage();

            mail.IsBodyHtml = true;

            AlternateView avAlternateView = null;
            Encoding myEncoding = Encoding.GetEncoding("UTF-8");

            avAlternateView = AlternateView.CreateAlternateViewFromString(mailBody, myEncoding, "text/plain");
            mail.AlternateViews.Add(avAlternateView);

            avAlternateView = AlternateView.CreateAlternateViewFromString(mailBody, myEncoding, "text/html");
            mail.AlternateViews.Add(avAlternateView);

            mail.Sender = MailFrom;
            mail.From = MailFrom;

            mail.To.Add(MailTo);

            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.GetEncoding("UTF-8");

            mail.BodyEncoding = Encoding.GetEncoding("UTF-8");

            Attachment attachment = new Attachment(attachmentPath);
            mail.Attachments.Add(attachment);
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = new List<string>() { ex.Message, "Unable to send Message" };
            }
        }

        private string ExportReportToPDF(int id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            var connectionString = ConfigurationManager.ConnectionStrings["AuthenticationDB"].ConnectionString;
            BTCDataSet ds = new BTCDataSet();
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            Byte[] bytes;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp;

            adp = new SqlDataAdapter("select * FROM LPODetails WHERE LPOID = " + id, conx);
            ds = new BTCDataSet();
            adp.Fill(ds, ds.LPODetails.TableName);
            reportViewer.LocalReport.ReportPath = Path.Combine(@"C:\Users\kanniyappans\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Reports\", "LPOReport.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("detailsDataset", ds.Tables["LPODetails"]));
            bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            string filename = Path.Combine(Path.GetTempPath(), "LPODetails.pdf");
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }

            return filename;
        }

        public void IntimateEmployee() { }

        private void IsLoggedIn(List<string> Role)
        {
            var Val = true;
            ViewBag.LoggedOut = !Val;
            ViewBag.messages = Val;
            ViewBag.notifications = Val;
            ViewBag.tasks = Val;
            ViewBag.IsEmployee = !Val;
            ViewBag.IsHR = !Val;
            ViewBag.IsTravelCo = !Val;
            ViewBag.IsManager = !Val;
            ViewBag.IsAdmin = !Val;
            if (roles.Contains(Constants.Employee))
            {
                ViewBag.IsEmployee = Val;
            }
            if (roles.Contains(Constants.HR))
            {
                ViewBag.IsHR = Val;
            }
            if (roles.Contains(Constants.Admin))
            {
                ViewBag.IsAdmin = Val;
            }
            if (roles.Contains(Constants.Manager))
            {
                ViewBag.IsManager = Val;
            }
            if (roles.Contains(Constants.TravelCorordinator))
            {
                ViewBag.IsTravelCo = Val;
            }
        }
    }
}