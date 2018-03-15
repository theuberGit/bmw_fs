using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.legalNotice
{
    public class Product : SearchInfo
    {
        public int idx { get; set; }
        public String title { get; set; }
        [AllowHtml]
        public String contents { get; set; }
        public String category { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }

        public String categoryName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(category))
                {
                    return null;
                }
                else if ("yeosin".Equals(category))
                {
                    return "여신거래";
                }
                else if ("halbu".Equals(category))
                {
                    return "할부거래";
                }
                else if ("lease".Equals(category))
                {
                    return "리스";
                }
                else if ("auto".Equals(category))
                {
                    return "오토론";
                }
                else
                {
                    return null;
                }
            }
        }

        override
         public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + "," : "") +
                (!string.IsNullOrWhiteSpace(title) ? "제목 : " + (title.Length > 5 ? title.Substring(0, 5) + "..." : title) + ", " : "") +
                (!string.IsNullOrWhiteSpace(contents) ? "내용 : " + (contents.Length > 5 ? contents.Substring(0, 5) + "..." : contents) + ", " : "") +
                (!string.IsNullOrWhiteSpace(categoryName) ? "카테고리 : " + categoryName + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOption + " 검색어 : " + searchInput : "");
        }
    }
}