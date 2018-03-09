using bmw_fs.Models.payment;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.payment;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.payment
{
    [Authorize(Roles = "MASTER, MSS")]
    public class CarImageController : Controller
    {
        private SearchService searchService = new SearchServiceImpl();
        private FilesService filesService = new FilesServiceImpl();
        private CarImageService carImageService = new CarImageServiceImpl();

        public ActionResult list(Payment payment)
        {
            searchService.setSearchSession(Request, Session);
            ViewBag.list = carImageService.findAll(payment);
            ViewBag.pagination = payment;
            
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Payment payment)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            payment.regId = System.Web.HttpContext.Current.User.Identity.Name;
            carImageService.insertCarImage(multipartRequest, payment);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Payment payment)
        {
            Payment item = carImageService.findCarImage(payment);
            ViewBag.item = item;
            ViewBag.carImg = filesService.findAllByMasterIdxAndType(item.idx, "carImg");
            
            return View();
        }

        public ActionResult modify(Payment payment)
        {
            Payment item = carImageService.findCarImage(payment);
            ViewBag.item = item;
            ViewBag.carImg = filesService.findAllByMasterIdxAndType(item.idx, "carImg");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Payment payment)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            payment.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            carImageService.updateCarImage(multipartRequest, payment);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Payment payment)
        {
            carImageService.deleteCarImage(payment);
            return RedirectToAction("list");
        }
    }
}