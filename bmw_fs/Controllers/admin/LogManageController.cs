using bmw_fs.Models.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.admin
{
    public class LogManageController : Controller
    {

        public ActionResult list(LogManage logManage)
        {
            return View();
        }
    }
}