using bmw_fs.Models.common;
using bmw_fs.Models.promotion;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.promotion;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.promotion
{
    [Authorize(Roles = "MASTER, MSS")]
    public class TabletPromotionController : Controller
    {
        TabletPromotionService tabletPromotionService = new TabletPromotionServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        public ActionResult list(TabletPromotion tabletPromotion) {
            if (tabletPromotion.searchOption == null)
            {
                tabletPromotion.searchOption = "now";
            }
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(tabletPromotion, 6, tabletPromotionService.findAllCount(tabletPromotion));
            ViewBag.list = tabletPromotionService.findAll(tabletPromotion);
            ViewBag.pagination = tabletPromotion;

            return View("~/Views/Promotion/TabletPromotion/list.cshtml");
        }

        public ActionResult register()
        {
            return View("~/Views/Promotion/TabletPromotion/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(TabletPromotion tabletPromotion)
        {

            HttpFileCollectionBase multipartfiles = Request.Files;
            tabletPromotion.regId = System.Web.HttpContext.Current.User.Identity.Name;
            tabletPromotionService.insertTabletPromotion(multipartfiles, tabletPromotion);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(TabletPromotion tabletPromotion)
        {
            TabletPromotion item = tabletPromotionService.findTabletPromotion(tabletPromotion);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbNail");
            ViewBag.bannerList = filesService.findAllByMasterIdxAndType(item.idx, "bannerImg");
            IList<Files> mainImgList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            ViewBag.mainImgList = mainImgList;

            ViewBag.state = tabletPromotion.searchOption;
            return View("~/Views/Promotion/TabletPromotion/view.cshtml");
        }
    }
}