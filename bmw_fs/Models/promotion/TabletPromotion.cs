using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.promotion
{
    public class TabletPromotion : SearchInfo
    {
        public int idx { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String deployYn { get; set; }
        public String title { get; set; }
        public String note { get; set; }
        public String brand { get; set; }

        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public int fileIdx { get; set; }

        public IList<int> thumbIdxs { get; set; }
        public IList<int> bannerIdxs { get; set; }
        public IList<int> mainIdxs { get; set; }

        public string deployYnStr
        {
            get
            {
                if ("Y".Equals(deployYn))
                {
                    return "배포";
                }
                else if ("N".Equals(deployYn))
                {
                    return "미배포";
                }
                return "";
            }
        }

        override
        public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + "," : "") +
                (!string.IsNullOrWhiteSpace(title) ? "제목 : " + (title.Length > 5 ? title.Substring(0, 5) + "..." : title) + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "게시기간 : " + startDate + "~" + endDate + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYn) ? "배포여부 : " + deployYnStr + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOption + " 검색어 : " + searchInput : "");
        }
    }
}