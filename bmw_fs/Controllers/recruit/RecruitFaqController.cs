﻿using bmw_fs.Models.recruit;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.recruit;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.recruit
{
    [Authorize]
    public class RecruitFaqController : Controller
    {
        SearchService searchService = new SearchServiceImpl();
        RecruitFaqService recruitFaqService = new RecruitFaqServiceImpl();

        public ActionResult list(RecruitFaq recruitFaq)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(recruitFaq, 10, recruitFaqService.findAllCount(recruitFaq));
            ViewBag.list = recruitFaqService.findAll(recruitFaq);
            ViewBag.pagination = recruitFaq;
            ViewBag.today = DateTime.Now;

            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(RecruitFaq recruitFaq)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            recruitFaq.regId = System.Web.HttpContext.Current.User.Identity.Name;
            recruitFaqService.insertRecruitFaq(recruitFaq);


            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(RecruitFaq recruitFaq)
        {
            RecruitFaq item = recruitFaqService.findRecruitFaq(recruitFaq);
            ViewBag.item = item;
            return View();
        }

        public ActionResult modify(RecruitFaq recruitFaq)
        {
            RecruitFaq item = recruitFaqService.findRecruitFaq(recruitFaq);
            ViewBag.item = item;            

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(RecruitFaq recruitFaq)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            recruitFaq.updateId = System.Web.HttpContext.Current.User.Identity.Name;            
            recruitFaqService.updateRecruitFaq(recruitFaq);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(RecruitFaq recruitFaq)
        {
            recruitFaqService.deleteRecruitFaq(recruitFaq);
            return RedirectToAction("list");
        }

    }
}