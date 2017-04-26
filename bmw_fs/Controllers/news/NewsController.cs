using bmw_fs.Models.news;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.news;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.news;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.news
{
    public class NewsController : Controller
    {
        NewsService newsService = new NewsServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        // GET: News
        public ActionResult list(News news)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(news, 20, newsService.findAllCount(news));
            ViewBag.list = newsService.findAll(news);
            ViewBag.pagination = news;
            ViewBag.noticeList = newsService.findAllNotice(news);
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(News news)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            news.regId = "testnews";
            newsService.insertNews(multipartfiles, news);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(News news)
        {
            News item = newsService.findNews(news);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbImg");
            ViewBag.mainList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            return View();
        }

        public ActionResult modify(News news)
        {
            News item = newsService.findNews(news);
            ViewBag.item = item;
            ViewBag.thumbList = filesService.findAllByMasterIdxAndType(item.idx, "thumbImg");
            ViewBag.mainList = filesService.findAllByMasterIdxAndType(item.idx, "mainImg");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(News news)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            news.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            news.contents = Sanitizer.GetSafeHtmlFragment(news.contents);
            newsService.updateNews(multipartRequest, news);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }
    }
}