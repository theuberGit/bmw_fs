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

        private MemberService memberService = new MemberServiceImpl();
        private SmsService smsService = new SmsServiceImpl();

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

                //로컬작업시 활성
                Boolean isLogin = true;
                
                //실섭에서 활성
                //Boolean isLogin = false;
                //try { 
                //    isLogin = ldapService.IsAuthenticated(_domain, model.userId, model.password);
                //}
                //catch (Exception e)
                //{
                //    ViewBag.errorMsg = "Invalid login attempt.";
                //    return View("~/Views/Login.cshtml", model);
                //}
                
                
                if (isLogin)
                {
                    FormsAuthentication.SetAuthCookie(model.userId, false);

                    var authTicket = new FormsAuthenticationTicket(1, member.userId, DateTime.Now, DateTime.Now.AddMinutes(30), false, member.role);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    memberService.updateLoginDate(member);


                    if (!String.IsNullOrWhiteSpace(member.tel1) && !String.IsNullOrWhiteSpace(member.tel2) && !String.IsNullOrWhiteSpace(member.tel3))
                    {
                        smsService.insertSms("[BMW FS 관리자]", member.tel1+"-"+member.tel2+"-"+member.tel3, "02-3441-5439", "[BMW FS 관리자] " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "에 로그인 되었습니다.");
                    }

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
                    ViewBag.errorMsg = "Invalid login attempt.";
                    return View("~/Views/Login.cshtml", model);
                }

            }
            else
            {                
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