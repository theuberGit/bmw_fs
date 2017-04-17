using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.common
{
    interface SearchService
    {
        void setPagination(SearchInfo searchInfo, int itemPerPage, int totalItemCount);

        void setSearchSession(HttpRequestBase request, HttpSessionStateBase session);
    }
}
