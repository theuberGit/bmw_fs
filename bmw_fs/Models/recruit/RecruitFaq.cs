using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.recruit
{
    public class RecruitFaq : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        public String contents { get; set; }
        public String titleEng { get; set; }
        public String contentsEng { get; set; }
        public String deployYn { get; set; }
        public String engYn { get; set; }
        public DateTime regDate { get; set; }
        public String regId { get; set; }
        public DateTime updateDate { get; set; }
        public String updateId { get; set; }
    }
}