using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.privacy;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.privacy;
using Microsoft.Security.Application;
using System.Web.Mvc;

namespace bmw_fs.Controllers.legalNotice
{
    [Authorize(Roles = "MASTER, COMPLIANCE")]
    public class PrivacyController : Controller
    {
        PrivacyService privacyService = new PrivacyServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(Privacy privacy)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(privacy, 20, privacyService.findAllCount(privacy));
            ViewBag.list = privacyService.findAll(privacy);
            ViewBag.pagination = privacy;
            return View("~/Views/LegalNotice/Privacy/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/LegalNotice/Privacy/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Privacy privacy)
        {
            privacy.contents = Sanitizer.GetSafeHtmlFragment(privacy.contents);
            privacy.regId = System.Web.HttpContext.Current.User.Identity.Name;
            privacyService.insertPrivacy(privacy);
            return RedirectToAction("list");
        }

        public ActionResult view(Privacy privacy)
        {
            Privacy item =  privacyService.findPrivacy(privacy);
            ViewBag.item = item;

            return View("~/Views/LegalNotice/Privacy/view.cshtml");
        }

        public ActionResult modify(Privacy privacy)
        {
            Privacy item = privacyService.findPrivacy(privacy);
            ViewBag.item = item;

            return View("~/Views/LegalNotice/Privacy/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Privacy privacy)
        {
            privacy.contents = Sanitizer.GetSafeHtmlFragment(privacy.contents);
            privacy.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            privacyService.updatePrivacy(privacy);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Privacy privacy)
        {
            privacyService.deletePrivacy(privacy);
            return RedirectToAction("list");
        }
    }
}