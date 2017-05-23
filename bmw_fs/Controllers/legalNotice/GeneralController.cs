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
    public class GeneralController : Controller
    {
        GeneralService generalService = new GeneralServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(General general)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(general, 20, generalService.findAllCount(general));
            ViewBag.list = generalService.findAll(general);
            ViewBag.pagination = general;

            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(General general)
        {
            general.contents = Sanitizer.GetSafeHtmlFragment(general.contents);
            general.regId = System.Web.HttpContext.Current.User.Identity.Name;
            generalService.insertGeneral(general);
            return RedirectToAction("list");
        }

        public ActionResult view(General general)
        {
            General item = generalService.findGeneral(general);
            ViewBag.item = item;

            return View();
        }

        public ActionResult modify(General general)
        {
            General item = generalService.findGeneral(general);
            ViewBag.item = item;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(General general)
        {
            general.contents = Sanitizer.GetSafeHtmlFragment(general.contents);
            general.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            generalService.updateGeneral(general);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(General general)
        {
            generalService.deleteGeneral(general);
            return RedirectToAction("list");
        }

    }
}