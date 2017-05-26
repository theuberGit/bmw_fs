using bmw_fs.Models.common;
using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.legalNotice;
using bmw_fs.Service.impl.common;
using bmw_fs.Service.impl.legalNotice;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.legalNotice
{
    [Authorize]
    public class ProductController : Controller
    {
        ProductService productService = new ProductServiceImpl();
        SearchService searchService = new SearchServiceImpl();

        public ActionResult list(Product product)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(product, 20, productService.findAllCount(product));
            ViewBag.list = productService.findAll(product);
            ViewBag.pagination = product;
                
            return View("~/Views/LegalNotice/Product/list.cshtml");
        }
        public ActionResult register(Product product)
        {
            return View("~/Views/LegalNotice/Product/register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Product product)
        {
            product.contents = Sanitizer.GetSafeHtmlFragment(product.contents);
            product.regId = System.Web.HttpContext.Current.User.Identity.Name;
            productService.insertProduct(product);

            return RedirectToAction("list");
        }

        public ActionResult view(Product product)
        {
            ViewBag.item = productService.findProduct(product);
            return View("~/Views/LegalNotice/Product/view.cshtml");
        }

        public ActionResult modify(Product product)
        {
            ViewBag.item = productService.findProduct(product);
            return View("~/Views/LegalNotice/Product/modify.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Product product)
        {
            product.contents = Sanitizer.GetSafeHtmlFragment(product.contents);
            product.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            productService.updateProduct(product);

            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Product product)
        {
            productService.deleteProduct(product);
            return RedirectToAction("list");
        }

    }
}