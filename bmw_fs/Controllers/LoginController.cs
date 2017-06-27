using bmw_fs.Models.admin;
using bmw_fs.Models.common;
using bmw_fs.Service.face.admin;
using bmw_fs.Service.impl.admin;
using bmw_fs.Service.impl.common;
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
        private String _domain = "172.30.81.67";
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login(String returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Login.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Member model, String returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Login.cshtml", model);
            }

            Member member = new Member();
            member = memberService.findMemberForLogin(model);

            if(member != null)
            {
                LdapAuthenticationService ldapService = new LdapAuthenticationService("LDAP://" + _domain);
                //Boolean isLogin = ldapService.IsAuthenticated(_domain, model.userId, model.password);
                Boolean isLogin = true;

                if (isLogin)
                {
                    FormsAuthentication.SetAuthCookie(model.userId, false);

                    var authTicket = new FormsAuthenticationTicket(1, member.userId, DateTime.Now, DateTime.Now.AddMinutes(20), false, member.role);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    memberService.updateLoginDate(member);

                    if (LoginSession.cookieValue.ContainsKey(model.userId))
                    {
                        LoginSession.cookieValue.Remove(model.userId);
                    }
                    LoginSession.cookieValue.Add(model.userId, authCookie.Value);

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
                    //ModelState.AddModelError("", "Invalid login attempt.");
                    ViewBag.errorMsg = "Invalid login attempt.";
                    return View("~/Views/Login.cshtml", model);
                }

            }
            else
            {
                //ModelState.AddModelError("", "Invalid login attempt.");
                ViewBag.errorMsg = "Invalid login attempt.";
                return View("~/Views/Login.cshtml", model);
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