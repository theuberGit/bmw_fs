using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.recruit
{
    public class RecruitNotice : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        [AllowHtml]
        public String contents { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime endDate { get; set; }
        public DateTime regDate { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime updateDate { get; set; }
        public String updateId { get; set; }
        public IList<int> fileIdxs { get; set; }
    }
}