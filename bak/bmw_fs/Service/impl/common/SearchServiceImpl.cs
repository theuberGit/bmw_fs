using bmw_fs.Models.common;
using bmw_fs.Service.face.common;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;

namespace bmw_fs.Service.impl.common
{
    public class SearchServiceImpl : SearchService 
    {
        public void setPagination(SearchInfo searchInfo, int itemPerPage, int totalItemCount)
        {
            searchInfo.itemPerPage = itemPerPage;
            searchInfo.totalItemCount = totalItemCount;
        }

        public void setSearchSession(HttpRequestBase request, HttpSessionStateBase session)
        {
            NameValueCollection queryStringOriginal = request.QueryString;
            String isSearch = queryStringOriginal.Get("search");
            String queryString = "";
            RouteValueDictionary route = new RouteValueDictionary();
            if ("true".Equals(isSearch))
            {
                queryString = queryStringOriginal.ToString();
                String[] keys = queryStringOriginal.AllKeys;
                foreach(String key in keys)
                {
                    route.Add(key, queryStringOriginal.Get(key));
                }
            }
            else
            {
                queryString = "";
                route.Clear();
            }
            session.Add("searchString", queryString);
            session.Add("searchMap", route);
        }
    }
    
}