using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.CustomerService
{
    public class DownLoadForm : SearchInfo
    {
        public int idx { get; set; }
        public String formName { get; set; }
        public String usagePurpose { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }

        public int fileIdx { get; set; }
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

        override
         public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + ", " : "") +
                (!string.IsNullOrWhiteSpace(formName) ? "제목 : " + (formName.Length > 5 ? formName.Substring(0, 5) + "..." : formName) + ", " : "") +
                (!string.IsNullOrWhiteSpace(usagePurpose) ? "내용 : " + (usagePurpose.Length > 5 ? usagePurpose.Substring(0, 5) + "..." : usagePurpose) + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYnStr) ? "배포여부 : " + deployYnStr + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색 구분 : " + searchOption + "검색어 : " + searchInput : "");
        }
    }
}