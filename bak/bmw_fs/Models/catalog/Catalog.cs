using System;
using System.Collections.Generic;
using System.Web.Mvc;
using bmw_fs.Models.common;
using System.ComponentModel.DataAnnotations;

namespace bmw_fs.Models.catalog
{
    public class Catalog : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public String fileName { get; set; }
        
        public IList<int> fileIdxs { get; set; }
    }
}