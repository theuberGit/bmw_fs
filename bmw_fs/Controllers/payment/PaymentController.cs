using bmw_fs.Models.payment;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.payment;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.payment;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Controllers.payment
{
    [Authorize(Roles = "MASTER, MSS")]
    public class PaymentController : Controller
    {
        PaymentService paymentService = new PaymentServiceImpl();
        SearchService searchService = new SearchServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        // GET: Payment
        public ActionResult list(Payment payment)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(payment, paymentService.findAllCount(payment), paymentService.findAllCount(payment));
            ViewBag.list = paymentService.findAll(payment);
            ViewBag.pagination = payment;
            ViewBag.noticeList = paymentService.findAll(payment);
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Payment payment)
        {
            HttpFileCollectionBase multipartfiles = Request.Files;
            payment.regId = System.Web.HttpContext.Current.User.Identity.Name;
            paymentService.insertPayment(multipartfiles, payment);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Payment payment)
        {
            Payment item = paymentService.findPayment(payment);
            ViewBag.item = item;
            ViewBag.carList = filesService.findAllByMasterIdxAndType(item.idx, "carImg");
            return View();
        }

        public ActionResult modify(Payment payment)
        {
            Payment item = paymentService.findPayment(payment);
            ViewBag.item = item;
            ViewBag.carList = filesService.findAllByMasterIdxAndType(item.idx, "carImg");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Payment payment)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            payment.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            paymentService.updatePayment(multipartRequest, payment);
            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        [HttpPost]
        public RedirectToRouteResult delete(Payment payment)
        {
            paymentService.deletePayment(payment);
            return RedirectToAction("list");
        }
    }
}