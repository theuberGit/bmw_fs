using System;
using System.Collections.Generic;
using System.Web.Mvc;
using bmw_fs.Models.common;

namespace bmw_fs.Models.promotion
{
    public class PromotionUrl : SearchInfo
    {
        public int pIdx { get; set; }
        public int fileIdx { get; set; }
        public String url { get; set; }
    }
}