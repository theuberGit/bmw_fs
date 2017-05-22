using bmw_fs.Models.Showroom;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.Showroom;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.Showroom;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.Showroom
{
    [Authorize]
    public class ShowroomController : Controller
    {
        private ShowroomService showroomService = new ShowroomServiceImpl();
        private SearchService searchService = new SearchServiceImpl();

        public ActionResult list(ShowRoom showroom)
        {
            this.searchService.setSearchSession(Request, Session);
            this.searchService.setPagination(showroom, 20, this.showroomService.findAllCount(showroom));
            ViewBag.list = this.showroomService.findAll(showroom);
            ViewBag.pagination = showroom;
            
            return View();
        }

        public ActionResult register(ShowRoom showroom)
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult registerProc(ShowRoom showroom)
        {

            showroom.regId = System.Web.HttpContext.Current.User.Identity.Name;
            this.showroomService.insertShowRoom(showroom);
            
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(ShowRoom showroom)
        {

            ShowRoom item = this.showroomService.findShowRoom(showroom);
            ViewBag.item = item;
            
            return View();
        }

        public ActionResult modify(ShowRoom showroom)
        {

            ShowRoom item = this.showroomService.findShowRoom(showroom);
            ViewBag.item = item;

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult modifyProc(ShowRoom showroom)
        {

            showroom.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            this.showroomService.updateShowRoom(showroom);
            
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(ShowRoom showroom)
        {
            this.showroomService.deleteShowRoom(showroom);
            return RedirectToAction("list");
        }
    }
}