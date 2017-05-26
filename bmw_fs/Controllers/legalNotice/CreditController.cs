using bmw_fs.Models.common;
using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.legalNotice;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.legalNotice;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.legalNotice
{
    [Authorize]
    public class CreditController : Controller
    {
        CreditService creditService = new CreditServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(Credit credit)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(credit, 20, creditService.findAllCount(credit));
            ViewBag.list = creditService.findAll(credit);
            ViewBag.pagination = credit;

            return View("~/Views/LegalNotice/Credit/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/LegalNotice/Credit/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Credit credit)
        {
            credit.contents = Sanitizer.GetSafeHtmlFragment(credit.contents);
            credit.regId = System.Web.HttpContext.Current.User.Identity.Name;
            creditService.insertCredit(credit);
            return RedirectToAction("list");
        }

        public ActionResult view(Credit credit)
        {
            Credit item = creditService.findCredit(credit);
            ViewBag.item = item;

            return View("~/Views/LegalNotice/Credit/view.cshtml");
        }

        public ActionResult modify(Credit credit)
        {
            Credit item = creditService.findCredit(credit);
            ViewBag.item = item;

            return View("~/Views/LegalNotice/Credit/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Credit credit)
        {
            credit.contents = Sanitizer.GetSafeHtmlFragment(credit.contents);
            credit.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            creditService.updateCredit(credit);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Credit credit)
        {
            creditService.deleteCredit(credit);
            return RedirectToAction("list");
        }
    }
}