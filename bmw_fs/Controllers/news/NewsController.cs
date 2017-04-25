using bmw_fs.Models.news;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.news;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.news;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            searchService.setPagination(news, 10, newsService.findAllCount(news));
            ViewBag.list = newsService.findAll(news);
            ViewBag.pagination = news;
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registerProc(News news)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            news.regId = "testnews";
            newsService.insertNews(multipartfiles, news);
            return RedirectToAction("list");
        }
    }
}