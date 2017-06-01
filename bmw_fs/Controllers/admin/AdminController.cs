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
using System.Web.Routing;

namespace bmw_fs.Controllers.admin
{
    [Authorize(Roles = "MASTER, IT")]
    public class AdminController : Controller
    {
        SearchService searchService = new SearchServiceImpl();
        MemberService memberService = new MemberServiceImpl();

        public ActionResult list(Member member)
        {
            searchService.setSearchSession(Request, Session);
            searchService.setPagination(member, 20, memberService.findAllCount(member));
            ViewBag.list = memberService.findAll(member);
            ViewBag.pagination = member;
            ViewBag.today = DateTime.Now;

            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult registerProc(Member member)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            member.regId = System.Web.HttpContext.Current.User.Identity.Name;
            memberService.insertMember(member);


            return RedirectToAction("list", (RouteValueDictionary)Session["searchMap"]);
        }

        public ActionResult view(Member member)
        {
            Member item = memberService.findMemberByIdx(member);
            ViewBag.item = item;
            return View();
        }

        public ActionResult modify(Member member)
        {
            Member item = memberService.findMemberByIdx(member);
            ViewBag.item = item;

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public RedirectToRouteResult modifyProc(Member member)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            member.uptId = System.Web.HttpContext.Current.User.Identity.Name;
            memberService.updateMember(member);
            return RedirectToAction("list");
        }

        [HttpPost]
        public RedirectToRouteResult delete(Member member)
        {
            memberService.deleteMember(member);
            return RedirectToAction("list");
        }

        [HttpPost]
        public ActionResult isDuplicated(Member member)
        {
            int caseCode = 0;
            if (memberService.findMemberDuplicated(member))
            {
                caseCode = -1;  //중복
            }
            else if (false) //TODO AD에서 아이디 검색
            {
                caseCode = -2; // AD CHECK
            }
            else
            {
                caseCode = 1;
            }
            

            return Json(caseCode, JsonRequestBehavior.DenyGet);
        }

    }
}