using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.rollling
{
    public class Rolling : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String engSite { get; set; }
        public int order { get; set; }
        public String deployYn { get; set; }
        public String gubun { get; set; }
        public String korButtonName { get; set; }
        public String KorButtonUrl { get; set; }
        public String engButtonName { get; set; }
        public String engButtonUrl { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }

        public IList<int> rollingIdx { get; set; }
    }
}