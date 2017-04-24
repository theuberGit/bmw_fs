using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bmw_fs.Common.Filter
{

    public class TestFilter : ActionFilterAttribute
    {

        /*
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.QueryString);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.Params);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.QueryString);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.Params);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.QueryString);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.Params);
        }
        */

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.QueryString);
            Debug.WriteLine("param : " + filterContext.HttpContext.Request.Params);
        }

        private void Log(string methodName, RouteData routeData)
        {            
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            Debug.WriteLine(message, "Action Filter Log");
            
        }

    }
}