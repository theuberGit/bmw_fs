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
        PromotionService webPromotionService = new PromotionServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();
        
        public ActionResult list(Promotion webPromotion)
        {
            webPromotion.webYn = "Y";
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(webPromotion, 6, webPromotionService.findAllCount(webPromotion));
            ViewBag.list = webPromotionService.findAll(webPromotion);
            ViewBag.pagination = webPromotion;
            return View("~/Views/Promotion/Promotion/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/Promotion/Promotion/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Promotion webPromotion)
        {
            
            HttpFileCollectionBase multipartfiles = Request.Files;
            webPromotion.webYn = "Y";
            webPromotion.regId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.insertPromotion(multipartfiles, webPromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Promotion webPromotion)
        {
            Promotion item = webPromotionService.findPromotion(webPromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findPromotionImgUrl(webPromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findPromotionImgUrlEng(webPromotion, mainImgEngList);

            return View("~/Views/Promotion/Promotion/view.cshtml");
        }

        public ActionResult modify(Promotion webPromotion)
        {
            Promotion item = webPromotionService.findPromotion(webPromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findPromotionImgUrl(webPromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findPromotionImgUrlEng(webPromotion, mainImgEngList);
            return View("~/Views/Promotion/Promotion/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Promotion webPromotion)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            webPromotion.webYn = "Y";
            webPromotion.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.updatePromotion(multipartRequest, webPromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Promotion webPromotion)
        {
            webPromotionService.deletePromotion(webPromotion);
            return RedirectToAction("list");
        }
    }
}