using bmw_fs.Models.CustomerService;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.CustomerService;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.CustomerService;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.CustomerService
{
    [Authorize]
    public class DownLoadFormController : Controller
    {
        DownLoadFormService downLoadFormService = new DownLoadFormServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(DownLoadForm downLoadForm)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(downLoadForm, 20, downLoadFormService.findAllCount(downLoadForm));
            ViewBag.list = downLoadFormService.findAll(downLoadForm);
            ViewBag.pagination = downLoadForm;

            return View();
        }

        public ActionResult register(DownLoadForm downLoadForm)
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult registerProc(DownLoadForm downLoadForm)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            downLoadForm.regId = System.Web.HttpContext.Current.User.Identity.Name;
            downLoadFormService.insertDownLoadForm(multipartfiles, downLoadForm);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(DownLoadForm downLoadForm)
        {
            ViewBag.item = downLoadFormService.findDownLoadForm(downLoadForm);
            return View();
        }

        public ActionResult modify(DownLoadForm downLoadForm)
        {
            ViewBag.item = downLoadFormService.findDownLoadForm(downLoadForm);
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult modifyProc(DownLoadForm downLoadForm)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            downLoadForm.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            downLoadFormService.updateDownLoadForm(multipartfiles,downLoadForm);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(DownLoadForm downLoadForm)
        {
            downLoadFormService.deleteDownLoadForm(downLoadForm);
            return RedirectToAction("list");
        }
    }
}