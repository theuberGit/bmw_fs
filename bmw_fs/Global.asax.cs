﻿using bmw_fs.Models.common;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace bmw_fs
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {                
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
                //중복 로그인 방지
                if (LoginSession.cookieValue.ContainsKey(authTicket.Name))
                {
                    if (!LoginSession.cookieValue[authTicket.Name].ToString().Equals(authCookie.Value))
                    {
                        FormsAuthentication.SignOut();
                    }
                }
            }
        }
        
        
        protected void Application_Error(object sender, EventArgs e)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MvcApplication));

            var lastError = Server.GetLastError();
            Exception exception = lastError;
            var serverError = lastError as HttpException;
            
            if ("CustomException".Equals(exception.GetType().Name))
            {
                logger.Error(exception.ToString());
                Session.Add("msg", exception.Message);
                Server.ClearError();
                Response.Redirect("/Error/CustomError");
            }
            else
            {
                if (serverError != null && serverError.GetHttpCode() == 404)
                {
                    Response.Redirect("/Error/Error404");
                }
                else if (serverError != null && serverError.GetHttpCode() == 403)
                {
                    Response.Redirect("/Error/Error403");
                }
                else
                {
                    logger.Error(exception.ToString());
                    Server.ClearError();
                    Response.Redirect("/Error/Error500");
                }
            }
            
        }

    }

}
