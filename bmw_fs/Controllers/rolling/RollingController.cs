using bmw_fs.Models.rolling;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.rolling;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.rolling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.rolling
{
    //[Authorize]
    //public class RollingController : Controller
    //{
    //    RollingService rollingService = new RollingServiceImpl();
    //    FilesService filesService = new FilesServiceImpl();
    //    SearchService searchService = new SearchServiceImpl();

    //    // GET: Rolling
    //    public ActionResult list(Rolling rolling)
    //    {
    //        searchService.setSearchSession(Request, Session);
    //        searchService.setPagination(rolling, 6, rollingService.findAllCount(rolling));
    //        ViewBag.pagination = rolling;
    //        ViewBag.list = rollingService.findAll(rolling);

    //        return View();
    //    }

    //    public ActionResult register()
    //    {
    //        return View();
    //    }

    //    public RedirectToRouteResult registerProc(Rolling rolling)
    //    {

    //        return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
    //    }

        



    //}
}