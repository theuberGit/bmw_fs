using bmw_fs.Models.privacy;
using bmw_fs.Service.face.privacy;
using bmw_fs.Service.impl.privacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers
{
    public class PrivacyController : Controller
    {
        PrivacyService privacyService = new PrivacyServiceImpl();

        public ActionResult list()
        {
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