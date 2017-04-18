using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        [Authorize(Roles = "MASTER")]
        public ActionResult master()
        {
            return View();
        }

        // GET: Member
        [Authorize(Roles = "UBER")]
        public ActionResult list()
        {
            return View();
        }

    }
}