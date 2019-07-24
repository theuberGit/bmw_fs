﻿using bmw_fs.Common;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AdminLogFilter());
        }
    }
}
