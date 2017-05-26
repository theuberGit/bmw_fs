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
    public class BusinessController : Controller
    {
        BusinessService businessService = new BusinessServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        public ActionResult list(Business business)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(business, 20, businessService.findAllCount(business));
            ViewBag.list = businessService.findAll(business);
            ViewBag.pagination = business;

            return View("~/Views/LegalNotice/Business/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/LegalNotice/Business/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Business business)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            business.contents = Sanitizer.GetSafeHtmlFragment(business.contents);
            business.regId = System.Web.HttpContext.Current.User.Identity.Name;
            businessService.insertBusiness(multipartfiles, business);
            return RedirectToAction("list");
        }

        public ActionResult view(Business business)
        {
            Business item = businessService.findBusiness(business);
            ViewBag.item = item;
            ViewBag.files = filesService.findAllByMasterIdxAndType(item.idx, "file");
            return View("~/Views/LegalNotice/Business/view.cshtml");
        }

        public ActionResult modify(Business business)
        {
            Business item = businessService.findBusiness(business);
            ViewBag.item = item;
            ViewBag.files = filesService.findAllByMasterIdxAndType(item.idx, "file");
            return View("~/Views/LegalNotice/Business/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Business business)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            business.contents = Sanitizer.GetSafeHtmlFragment(business.contents);
            business.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            businessService.updateBusiness(multipartRequest, business);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Business business)
        {
            businessService.deleteBusiness(business);
            return RedirectToAction("list");
        }
    }
}