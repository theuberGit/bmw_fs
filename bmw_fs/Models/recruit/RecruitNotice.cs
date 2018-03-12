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
                (startDate != DateTime.MinValue ? "시작일 : " + startDate + ", " : "") +
                (endDate != DateTime.MinValue ? "종료일 : " + endDate + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYnStr) ? "배포여부 : " + deployYnStr + ", " : "") + 
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색 구분 : " + searchOption + "검색어 : " + searchInput : "");
        }

    }
}