using bmw_fs.Models.admin;
using bmw_fs.Service.face.admin;
using bmw_fs.Service.impl.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace bmw_fs.Common
{
    public class AdminLogFilter : FilterAttribute, IActionFilter
    {
        private LogManageService logManageService = new LogManageServiceImpl();

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var param = filterContext.ActionParameters;
                string url = filterContext.RequestContext.HttpContext.Request.RawUrl;
                if (!"/".Equals(url))
                {
                    string paramStr = "";
                    foreach (var item in param.ToList())
                    {
                        paramStr += item.Value.ToString();
                    }
                    LogManage logManage = new LogManage();
                    logManage.qid = authTicket.Name;
                    logManage.menuName = logManageService.convertMenuNameByUrl(url);
                    logManage.contents = paramStr;
                    logManageService.insertAdminLog(logManage);
                }
            }
        }
    }
}