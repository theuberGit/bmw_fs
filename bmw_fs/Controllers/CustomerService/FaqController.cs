using bmw_fs.Models.CustomerService;
using bmw_fs.Service.face.CustomerService;
using bmw_fs.Service.impl.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.CustomerService
{
    [Authorize]
    public class FaqController : Controller
    {
        FaqService faqService = new FaqServiceImpl();

        public ActionResult list(Faq faq)
        {

            return View();
        }

        public ActionResult register(Faq faq)
        {
            return View();
        }

        public ActionResult view(Faq faq)
        {
            return View();
        }

        public ActionResult modify(Faq faq)
        {
            return View();
        }
    }
}