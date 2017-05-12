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
        private DownLoadFormService downLoadFormService = new DownLoadFormServiceImpl();
        private SearchService searchService = new SearchServiceImpl();
        private FilesService filesService = new FilesServiceImpl();

        public ActionResult list(DownLoadForm downLoadForm)
        {
            this.searchService.setSearchSession(Request, Session);
            this.searchService.setPagination(downLoadForm, 20, this.downLoadFormService.findAllCount(downLoadForm));
            ViewBag.list = this.downLoadFormService.findAll(downLoadForm);
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
            this.downLoadFormService.insertDownLoadForm(multipartfiles, downLoadForm);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(DownLoadForm downLoadForm)
        {
            DownLoadForm item = this.downLoadFormService.findDownLoadForm(downLoadForm);
            ViewBag.item = item;
            ViewBag.files = this.filesService.findAllByMasterIdxAndType(item.idx, "formFile");
            return View();
        }

        public ActionResult modify(DownLoadForm downLoadForm)
        {
            DownLoadForm item = this.downLoadFormService.findDownLoadForm(downLoadForm);
            ViewBag.item = item;
            ViewBag.files = this.filesService.findAllByMasterIdxAndType(item.idx, "formFile");
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult modifyProc(DownLoadForm downLoadForm)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            downLoadForm.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            this.downLoadFormService.updateDownLoadForm(multipartfiles,downLoadForm);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(DownLoadForm downLoadForm)
        {
            this.downLoadFormService.deleteDownLoadForm(downLoadForm);
            return RedirectToAction("list");
        }
    }
}