using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Travel_Request_System_EF.Models;
using Travel_Request_System_EF.Models.ViewModel;
using System.Web.Http;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.IO;

namespace Travel_Request_System_EF.Controllers
{
    public class LPOController : Controller
    {
        // GET: LPO
        public async Task<ActionResult> Index()
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

            //adp = new SqlDataAdapter("select * FROM TravelRequestDetails", conx);
            //ds = new BTCDataSet();
            //adp.Fill(ds, ds.TravelRequestDetails.TableName);
            //reportViewer.LocalReport.ReportPath = Path.Combine(@"C:\Users\kanniyappans\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Reports\", "TravelRequestReport.rdlc");
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("detailsDataset", ds.Tables["TravelRequestDetails"]));
            //bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename= outputreport" + "." + extension);
            //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            //Response.Flush(); // send it to the client to download  

            //adp = new SqlDataAdapter("select * FROM RFQDetails", conx);
            //ds = new BTCDataSet();
            //adp.Fill(ds, ds.RFQDetails.TableName);
            //reportViewer.LocalReport.ReportPath = Path.Combine(@"C:\Users\kanniyappans\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Reports\", "RFQReport.rdlc");
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("detailsDataset", ds.Tables["RFQDetails"]));
            //bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename= outputreport" + "." + extension);
            //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            //Response.Flush(); // send it to the client to download  

            adp = new SqlDataAdapter("select * FROM LPODetails", conx);
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

            ViewBag.ReportViewer = reportViewer;

            using (BTCEntities db = new BTCEntities())
            {
                var lpos = db.LPO.Include(a => a.RFQ).Include(a => a.Quotation).Include(a => a.RFQ).Include("RFQ.TravelRequests").Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation");
                return View(await lpos.ToListAsync());
            }
        }

        public async Task<ActionResult> LPOProcessing(int? id)
        {
            using (BTCEntities db = new BTCEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = true;
                var RFQList = await db.RFQ.Include(a => a.TravelRequests).Include(a => a.LPO).Include(a => a.TravelAgency).Include(a => a.Users).Include(a => a.Quotation).Include("Quotation.ATQuotation").Include("Quotation.HSQuotation").Include("Quotation.PCQuotation").Where(a => a.Processing == (int)ProcessingStatus.Processed).Distinct().ToListAsync();
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
                        lpoObject.RFQID = Convert.ToInt32(Regex.Match(item.Key.Split('-')[0], "(\\w+)(\\d+)").Groups[2].Value);
                        lpoObject.QuotationID = Convert.ToInt32(Regex.Match(item.Key.Split('-')[1], "(\\w+)(\\d+)").Groups[2].Value);
                        if (Regex.Match(item.Key.Split('-')[2], "(\\w+)(\\d+)").Groups[1].Value.Contains("ATCheck"))
                        {
                            lpoObject.IsAT = true;
                        }
                        if (Regex.Match(item.Key.Split('-')[2], "(\\w+)(\\d+)").Groups[1].Value.Contains("HSCheck"))
                        {
                            lpoObject.IsHS = true;
                        }
                        if (Regex.Match(item.Key.Split('-')[2], "(\\w+)(\\d+)").Groups[1].Value.Contains("PCCheck"))
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
    }
}