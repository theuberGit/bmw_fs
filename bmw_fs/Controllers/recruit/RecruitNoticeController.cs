﻿using bmw_fs.Models.recruit;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.recruit;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.recruit;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.recruit
{
    [Authorize(Roles = "MASTER, HR")]
    public class RecruitNoticeController : Controller
    {
        RecruitNoticeService recruitNoticeService = new RecruitNoticeServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesServce = new FilesServiceImpl();

        public ActionResult list(RecruitNotice recruitNotice)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(recruitNotice, 20, recruitNoticeService.findAllCount(recruitNotice));
            ViewBag.list = recruitNoticeService.findAll(recruitNotice);
            ViewBag.pagination = recruitNotice;
            ViewBag.today = DateTime.Now;

            return View("~/Views/Recruit/RecruitNotice/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/Recruit/RecruitNotice/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(RecruitNotice recruitNotice)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            recruitNotice.regId = System.Web.HttpContext.Current.User.Identity.Name;
            recruitNotice.contents = Sanitizer.GetSafeHtmlFragment(recruitNotice.contents);
            recruitNoticeService.insertRecruitNotice(multipartRequest, recruitNotice);

         
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }
       
        public ActionResult view(RecruitNotice recruitNotice)
        {
            RecruitNotice item = recruitNoticeService.findRecruitNotice(recruitNotice);
            ViewBag.item = item;
            ViewBag.filesList1 = filesServce.findAllByMasterIdxAndType(item.idx, "files");

            return View("~/Views/Recruit/RecruitNotice/view.cshtml");
        }

        public ActionResult modify(RecruitNotice recruitNotice)
        {
            RecruitNotice item = recruitNoticeService.findRecruitNotice(recruitNotice);
            ViewBag.item = item;
            ViewBag.filesList1 = filesServce.findAllByMasterIdxAndTypeForUpload(item.idx, "files");

            return View("~/Views/Recruit/RecruitNotice/modify.cshtml");
        }
        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(RecruitNotice recruitNotice)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            recruitNotice.updateId = System.Web.HttpContext.Current.User.Identity.Name;
            recruitNotice.contents = Sanitizer.GetSafeHtmlFragment(recruitNotice.contents);
            recruitNoticeService.updateRecruitNotice(multipartRequest, recruitNotice);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(RecruitNotice recruitNotice)
        {
            recruitNoticeService.deleteRecruitNotice(recruitNotice);
            return RedirectToAction("list");
        }

    }
}