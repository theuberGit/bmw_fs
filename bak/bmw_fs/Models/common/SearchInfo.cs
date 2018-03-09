using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.common
{
    public class SearchInfo : Pagination
    {
        public String searchOption { get; set; }
        public String searchInput { get; set; }
        public Boolean isSearch = false;
    }
}