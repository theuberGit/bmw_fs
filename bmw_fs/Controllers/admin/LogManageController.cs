using bmw_fs.Models.admin;
using bmw_fs.Service.face.admin;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.admin;
using bmw_fs.Service.impl.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.admin
{
    [Authorize(Roles = "MASTER, IT")]
    public class LogManageController : Controller
    {
        SearchService searchService = new SearchServiceImpl();
        LogManageService logManageService = new LogManageServiceImpl();

        public ActionResult list(LogManage logManage)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(logManage, 20, logManageService.findAllCount(logManage));
            ViewBag.list = logManageService.findAll(logManage);
            ViewBag.pagination = logManage;
            return View();
        }
    }
}