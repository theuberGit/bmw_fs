using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.CustomerService
{
    public class DownLoadForm : SearchInfo
    {
        public int idx { get; set; }
        public String formName { get; set; }
        public String usagePurpose { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
    }
}