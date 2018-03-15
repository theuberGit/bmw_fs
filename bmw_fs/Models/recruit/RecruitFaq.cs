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
                (idx > 0 ? "idx : " + idx + ", " : "") +
                (!string.IsNullOrWhiteSpace(title) ? "제목 : " + (title.Length > 5 ? title.Substring(0, 5) + "..." : title) + ", " : "") +
                (!string.IsNullOrWhiteSpace(contents) ? "내용 : " + (contents.Length > 5 ? contents.Substring(0, 5) + "..." : contents) + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYnStr) ? "배포여부 : " + deployYnStr + ", " : "") + 
                (!string.IsNullOrWhiteSpace(titleEng) ? "제목eng : " + titleEng + ", " : "") + 
                (!string.IsNullOrWhiteSpace(contentsEng) ? "내용eng : " + contentsEng + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYn) ? "배포여부 : " + deployYnStr + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOption + " 검색어 : " + searchInput : "");
        }
    }
}