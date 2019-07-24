using System;
using System.Collections.Generic;
using System.Web.Mvc;
using bmw_fs.Models.common;

namespace bmw_fs.Models.news
{
    public class News : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        [AllowHtml]
        public String contents { get; set; }
        public String type { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public int hit { get; set; }

        public IList<int> thumbIdxs { get; set; }
        public IList<int> fileIdxs { get; set; }

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

        public string typeStr
        {
            get
            {
                if ("NEWS".Equals(type))
                {
                    return "뉴스";
                }
                else if ("NOTICE".Equals(type))
                {
                    return "공지";
                }
                return "";
            }
        }

        override
        public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + ", " : "") +
                (!string.IsNullOrWhiteSpace(title) ? "제목 : " + (title.Length > 5 ? title.Substring(0, 5) + "..." : title) + ", " : "") +
                (!string.IsNullOrWhiteSpace(typeStr) ? "카테고리 : " + typeStr + ", " : "") +
                (!string.IsNullOrWhiteSpace(contents) ? "내용 : " + (contents.Length > 5 ? contents.Substring(0, 5) + "..." : contents) + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYn) ? "배포여부 : " + deployYnStr + ", " : "") +
                (page > 1 ? " page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOption + " 검색어 : " + searchInput : "");
        }
    }
}