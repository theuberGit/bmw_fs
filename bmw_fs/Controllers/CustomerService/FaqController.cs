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
    public class FaqController : Controller
    {
        FaqService faqService = new FaqServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(Faq faq)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(faq, 20, faqService.findAllCount(faq));
            ViewBag.list = faqService.findAll(faq);
            ViewBag.pagination = faq;

            return View("~/Views/CustomerService/Faq/list.cshtml");
        }

        public ActionResult register(Faq faq)
        {
            return View("~/Views/CustomerService/Faq/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Faq faq)
        {
            faq.question = Sanitizer.GetSafeHtmlFragment(faq.question);
            faq.regId = System.Web.HttpContext.Current.User.Identity.Name;
            faqService.insertFaq(faq);

            return RedirectToAction("list");
        }

        public ActionResult view(Faq faq)
        {
            ViewBag.item = faqService.findFaq(faq);
            return View("~/Views/CustomerService/Faq/view.cshtml");
        }

        public ActionResult modify(Faq faq)
        {
            ViewBag.item = faqService.findFaq(faq);
            return View("~/Views/CustomerService/Faq/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Faq faq)
        {
            faq.question = Sanitizer.GetSafeHtmlFragment(faq.question);
            faqService.updateFaq(faq);

            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Faq faq)
        {
            faqService.deleteFaq(faq);
            return RedirectToAction("list");
        }
    }
}