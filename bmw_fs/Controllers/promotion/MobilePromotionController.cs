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
    public class MobilePromotionController : Controller
    {
        WebPromotionService webPromotionService = new WebPromotionServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();
        
        public ActionResult list(Promotion mobilePromotion)
        {
            mobilePromotion.webYn = "N";
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(mobilePromotion, 6, webPromotionService.findAllCount(mobilePromotion));
            ViewBag.list = webPromotionService.findAll(mobilePromotion);
            ViewBag.pagination = mobilePromotion;
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Promotion mobilePromotion)
        {
            
            HttpFileCollectionBase multipartfiles = Request.Files;
            mobilePromotion.webYn = "N";
            mobilePromotion.regId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.insertWebPromotion(multipartfiles, mobilePromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Promotion mobilePromotion)
        {
            Promotion item = webPromotionService.findWebPromotion(mobilePromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findWebPromotionImgUrl(mobilePromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findWebPromotionImgUrlEng(mobilePromotion, mainImgEngList);

            return View();
        }

        public ActionResult modify(Promotion mobilePromotion)
        {
            Promotion item = webPromotionService.findWebPromotion(mobilePromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findWebPromotionImgUrl(mobilePromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findWebPromotionImgUrlEng(mobilePromotion, mainImgEngList);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Promotion mobilePromotion)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            mobilePromotion.webYn = "N";
            mobilePromotion.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.updateWebPromotion(multipartRequest, mobilePromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Promotion mobilePromotion)
        {
            webPromotionService.deleteWebPromotion(mobilePromotion);
            return RedirectToAction("list");
        }
    }
}