using System;
using System.Collections.Generic;
using System.Web.Mvc;
using bmw_fs.Models.common;

namespace bmw_fs.Models.news
{
    public class News : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        [AllowHtml]
        public String contents { get; set; }
        public String type { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public int hit { get; set; }

        public IList<int> thumbIdxs { get; set; }
        public IList<int> fileIdxs { get; set; }
    }
}