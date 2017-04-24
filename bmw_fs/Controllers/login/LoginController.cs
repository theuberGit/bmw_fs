using bmw_fs.Models.admin;
using bmw_fs.Service.face.admin;
using bmw_fs.Service.impl.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace bmw_fs.Controllers.login
{
    public class LoginController : Controller
    {

        MemberService memberService = new MemberServiceImpl();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login(String returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Member model, String returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Member member = new Member();
            member = memberService.findMemberForLogin(model);

            if (member != null)
            {
                FormsAuthentication.SetAuthCookie(model.userId, false);

                var authTicket = new FormsAuthenticationTicket(1, member.userId, DateTime.Now, DateTime.Now.AddMinutes(20), false, member.role);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);                                                                                                          
                memberService.updateLoginDate(member);
                if (String.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }

                
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
    
}