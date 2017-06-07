using bmw_fs.Models.CustomerService;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.CustomerService;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.CustomerService;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bmw_fs.Controllers.CustomerService
{
    [Authorize(Roles = "MASTER, CIC")]
    public class InquiryController : Controller
    {
        
        SearchService searchService = new SearchServiceImpl();
        FilesService filesServce = new FilesServiceImpl();
        InquiryService inquiryService = new InquiryServiceImpl();
        MailService mailService = new MailServiceImpl();

        public ActionResult list(Inquiry inquiry)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(inquiry, 20, inquiryService.findAllCount(inquiry));
            ViewBag.list = inquiryService.findAll(inquiry);
            ViewBag.pagination = inquiry;
            ViewBag.today = DateTime.Now;

            return View("~/Views/CustomerService/Inquiry/list.cshtml");
        }


        public ActionResult view(Inquiry inquiry)
        {
            Inquiry item = inquiryService.findInquiry(inquiry);
            ViewBag.item = item;            

            return View("~/Views/CustomerService/Inquiry/view.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult reply(Inquiry inquiry)
        {
            inquiry.replyRegId = System.Web.HttpContext.Current.User.Identity.Name;
            inquiryService.updateInquiry(inquiry);            
            return RedirectToAction("view", new { idx = inquiry.idx });
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<RedirectToRouteResult> sendMail(Inquiry inquiry)
        {
            inquiry.replyRegId = System.Web.HttpContext.Current.User.Identity.Name;
            inquiry.mailSendId = System.Web.HttpContext.Current.User.Identity.Name;
            inquiryService.updateInquirySendMail(inquiry);
            Inquiry item = inquiryService.findInquiry(inquiry);

            await mailService.sendMail(item.email, "test@test.co.kr", item.title, inquiry.replyContents);
            return RedirectToAction("view", new { idx = inquiry.idx });
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult replyProc(Inquiry inquiry)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            inquiry.replyRegId = System.Web.HttpContext.Current.User.Identity.Name;
            inquiry.contents = Sanitizer.GetSafeHtmlFragment(inquiry.contents);
            inquiryService.updateInquiry(inquiry);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Inquiry inquiry)
        {
            inquiryService.deleteInquiry(inquiry);
            return RedirectToAction("list");
        }

        public ActionResult excelDownload(Inquiry inquiry)
        {
            GridView grid = new GridView();            
            grid.DataSource = inquiryService.downloadExcel(inquiry);
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Inquiry.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);          

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return Content(sw.ToString(), "application/ms-excel");
        }
    }
}