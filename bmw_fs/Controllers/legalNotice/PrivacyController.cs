using bmw_fs.Models.privacy;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.privacy;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.privacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.legalNotice
{
    public class PrivacyController : Controller
    {
        PrivacyService privacyService = new PrivacyServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(Privacy privacy)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(privacy, 10, privacyService.findAllCount(privacy));
            ViewBag.list = privacyService.findAll(privacy);
            ViewBag.pagination = privacy;
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Privacy privacy)
        {
            privacyService.insertPrivacy(privacy);

            return RedirectToAction("list");
        }
    }
}