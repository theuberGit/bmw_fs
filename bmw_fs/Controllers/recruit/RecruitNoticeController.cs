﻿using bmw_fs.Models.recruit;
using bmw_fs.Service.face.common;Board
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
    public class RecruitNoticeController : Controller
    {
        RecruitNoticeService recruitNoticeSerivce = new RecruitNoticeServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesServce = new FilesServiceImpl();

        public ActionResult list(RecruitNotice recruitNotice)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(recruitNotice, 10, recruitNoticeSerivce.findAllCount(recruitNotice));
            ViewBag.list = recruitNoticeSerivce.findAll(recruitNotice);
            ViewBag.pagination = recruitNotice;
            
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(RecruitNotice recruitNotice)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            recruitNotice.regId = System.Web.HttpContext.Current.User.Identity.Name;
            recruitNotice.contents = Sanitizer.GetSafeHtmlFragment(recruitNotice.contents);
            recruitNoticeSerivce.insertRecruitNotice(multipartRequest, recruitNotice);

         
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }
       
        public ActionResult view(RecruitNotice recruitNotice)
        {
            RecruitNotice item = recruitNoticeSerivce.findRecruitNotice(recruitNotice);
            ViewBag.item = item;
            ViewBag.filesList1 = filesServce.findAllByMasterIdxAndType(item.idx, "files");

            return View();
        }

        public ActionResult modify(RecruitNotice recruitNotice)
        {
            RecruitNotice item = recruitNoticeSerivce.findRecruitNotice(recruitNotice);
            ViewBag.item = item;
            ViewBag.filesList1 = filesServce.findAllByMasterIdxAndTypeForUpload(item.idx, "files");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(RecruitNotice recruitNotice)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            recruitNotice.updateId = System.Web.HttpContext.Current.User.Identity.Name;
            recruitNotice.contents = Sanitizer.GetSafeHtmlFragment(recruitNotice.contents);
            recruitNoticeSerivce.updateRecruitNotice(multipartRequest, recruitNotice);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(RecruitNotice recruitNotice)
        {
            recruitNoticeSerivce.deleteRecruitNotice(recruitNotice);
            return RedirectToAction("list");
        }

    }
}