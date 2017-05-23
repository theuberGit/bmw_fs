using bmw_fs.Models.CustomerService;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.CustomerService;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.CustomerService;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.CustomerService
{
    [Authorize]
    public class InquiryController : Controller
    {
        
        SearchService searchService = new SearchServiceImpl();
        FilesService filesServce = new FilesServiceImpl();
        InquiryService inquiryService = new InquiryServiceImpl();

        public ActionResult list(Inquiry inquiry)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(inquiry, 20, inquiryService.findAllCount(inquiry));
            ViewBag.list = inquiryService.findAll(inquiry);
            ViewBag.pagination = inquiry;
            ViewBag.today = DateTime.Now;

            return View();
        }


        public ActionResult view(Inquiry inquiry)
        {
            Inquiry item = inquiryService.findInquiry(inquiry);
            ViewBag.item = item;            

            return View();
        }

        public ActionResult reply(Inquiry inquiry)
        {
            Inquiry item = inquiryService.findInquiry(inquiry);
            ViewBag.item = item;
            
            return View();
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
    }
}