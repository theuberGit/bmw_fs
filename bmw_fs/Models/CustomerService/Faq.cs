using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.CustomerService
{
    public class Faq :SearchInfo
    {
        public int idx { get; set; }
        public String category { get; set; }
        public String ask { get; set; }
        [AllowHtml]
        public String question { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
    }
}