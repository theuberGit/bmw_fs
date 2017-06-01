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
    [Authorize(Roles = "MASTER, MSS")]
    public class MobilePromotionController : Controller
    {
        PromotionService webPromotionService = new PromotionServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();
        
        public ActionResult list(Promotion mobilePromotion)
        {
            mobilePromotion.webYn = "N";
            if (mobilePromotion.searchOption == null)
            {
                mobilePromotion.searchOption = "now";
            }
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(mobilePromotion, 6, webPromotionService.findAllCount(mobilePromotion));
            ViewBag.list = webPromotionService.findAll(mobilePromotion);
            ViewBag.pagination = mobilePromotion;
            return View("~/Views/Promotion/MobilePromotion/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/Promotion/MobilePromotion/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Promotion mobilePromotion)
        {
            
            HttpFileCollectionBase multipartfiles = Request.Files;
            mobilePromotion.webYn = "N";
            mobilePromotion.regId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.insertPromotion(multipartfiles, mobilePromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Promotion mobilePromotion)
        {
            Promotion item = webPromotionService.findPromotion(mobilePromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findPromotionImgUrl(mobilePromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findPromotionImgUrlEng(mobilePromotion, mainImgEngList);

            return View("~/Views/Promotion/MobilePromotion/view.cshtml");
        }

        public ActionResult modify(Promotion mobilePromotion)
        {
            Promotion item = webPromotionService.findPromotion(mobilePromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;
            ViewBag.imgUrlList = webPromotionService.findPromotionImgUrl(mobilePromotion, mainImgList);

            ViewBag.thumbEngList = filesService.findAllByMasterIdxAndType(item.idx, "engThumbNail");
            IList<Files> mainImgEngList = filesService.findAllByMasterIdxAndType(item.idx, "engMainImg");
            ViewBag.mainImgEngList = mainImgEngList;
            ViewBag.imgUrlEngList = webPromotionService.findPromotionImgUrlEng(mobilePromotion, mainImgEngList);
            return View("~/Views/Promotion/MobilePromotion/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Promotion mobilePromotion)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            mobilePromotion.webYn = "N";
            mobilePromotion.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            webPromotionService.updatePromotion(multipartRequest, mobilePromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Promotion mobilePromotion)
        {
            webPromotionService.deletePromotion(mobilePromotion);
            return RedirectToAction("list");
        }
    }
}