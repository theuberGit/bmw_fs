using bmw_fs.Models.common;
using bmw_fs.Models.promotion;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.promotion;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.promotion;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.promotion
{
    [Authorize]
    public class PromotionController : Controller
    {
        WebPromotionService webPromotionService = new WebPromotionServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();
        
        public ActionResult list(Promotion webPromotion)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(webPromotion, 6, webPromotionService.findAllCount(webPromotion));
            ViewBag.list = webPromotionService.findAll(webPromotion);
            ViewBag.pagination = webPromotion;
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Promotion webPromotion)
        {
            
            HttpFileCollectionBase multipartfiles = Request.Files;
            webPromotion.regId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.insertWebPromotion(multipartfiles, webPromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Promotion webPromotion)
        {
            Promotion item = webPromotionService.findWebPromotion(webPromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            ViewBag.topMainList = filesService.findAllByMasterIdxAndType(item.idx, "topMain");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findWebPromotionImgUrl(webPromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            ViewBag.topMainEngList = filesService.findAllByMasterIdxAndType(item.idx, "engTopMain");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findWebPromotionImgUrlEng(webPromotion, mainImgEngList);

            return View();
        }

        public ActionResult modify(Promotion webPromotion)
        {
            Promotion item = webPromotionService.findWebPromotion(webPromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            ViewBag.topMainList = filesService.findAllByMasterIdxAndType(item.idx, "topMain");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findWebPromotionImgUrl(webPromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            ViewBag.topMainEngList = filesService.findAllByMasterIdxAndType(item.idx, "engTopMain");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findWebPromotionImgUrlEng(webPromotion, mainImgEngList);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Promotion webPromotion)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            webPromotion.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.updateWebPromotion(multipartRequest, webPromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Promotion webPromotion)
        {
            webPromotionService.deleteWebPromotion(webPromotion);
            return RedirectToAction("list");
        }
    }
}