using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.legalNotice
{
    public class Privacy : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        [AllowHtml]
        public String contents { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
    }
}