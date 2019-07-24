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
        public String brand { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public String fileName { get; set; }
        
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

        public
        override String ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + ", " : "") +
                (!string.IsNullOrWhiteSpace(title) ? "제목 : " + (title.Length > 8 ? title.Substring(0, 8) + "..." : title) + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYn) ? "배포여부 : " + deployYnStr + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "")+
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOption + " 검색어 : " + searchInput : "");
        }            

    }
}