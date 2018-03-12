using bmw_fs.Common;
using bmw_fs.Models.common;
using bmw_fs.Models.payment;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.payment;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.payment;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.payment
{
    [Authorize(Roles = "MASTER, MSS")]
    public class PaymentController : Controller
    {
        PaymentService paymentService = new PaymentServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        // GET: Payment
        public ActionResult list(Payment payment)
        {
            if (String.IsNullOrWhiteSpace(payment.searchOption))
            {
                payment.searchOption = "zangaHalbu";
            }
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(payment, paymentService.findAllCount(payment), paymentService.findAllCount(payment));
            ViewBag.list = paymentService.findAll(payment);
            ViewBag.pagination = payment;
            ViewBag.noticeList = paymentService.findAll(payment);
            Files file = new Files();
            file.masterIdx = 900000;
            ViewBag.excelFile = filesService.findAllByMasterIdx(file);
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
            HttpFileCollectionBase multipartfiles = Request.Files;
            payment.regId = System.Web.HttpContext.Current.User.Identity.Name;
            paymentService.insertPayment(multipartfiles, payment);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Payment payment)
        {
            Payment item = paymentService.findPayment(payment);
            ViewBag.item = item;
            ViewBag.carList = filesService.findAllByMasterIdxAndType(item.idx, "carImg");
            return View();
        }

        public ActionResult modify(Payment payment)
        {
            Payment item = paymentService.findPayment(payment);
            item.programs = item.program.Split(',');
            ViewBag.item = item;
            ViewBag.carList = filesService.findAllByMasterIdxAndType(item.idx, "carImg");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Payment payment)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            payment.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            paymentService.updatePayment(multipartRequest, payment);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Payment payment)
        {
            paymentService.deletePayment(payment);
            return RedirectToAction("list");
        }

        [HttpPost]
        public ActionResult loadExcel()
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            string regId = System.Web.HttpContext.Current.User.Identity.Name;
            paymentService.loadExcelByFileAndUpdateToDB(multipartfiles, regId);            
            return RedirectToAction("list");
        }
        
        [HttpPost]
        public ActionResult findModels(Payment payment)
        {
            return Json(paymentService.findModel(payment), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult findSeries(Payment payment)
        {
            return Json(paymentService.findSeries(payment), JsonRequestBehavior.DenyGet);
        }
    }
}