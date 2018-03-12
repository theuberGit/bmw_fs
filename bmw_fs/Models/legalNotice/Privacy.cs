using bmw_fs.Models.common;
using System;
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

        override
         public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + "," : "") +
                (!string.IsNullOrWhiteSpace(title) ? "제목 : " + (title.Length > 5 ? title.Substring(0, 5) + "..." : title) + ", " : "") +
                (!string.IsNullOrWhiteSpace(contents) ? "내용 : " + (contents.Length > 5 ? contents.Substring(0, 5) + "..." : contents) + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색 구분 : " + searchOption + "검색어 : " + searchInput : "");
        }
    }
}