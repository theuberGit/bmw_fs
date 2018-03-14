using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Models.CustomerService
{
    public class Faq : SearchInfo
    {
        public int idx { get; set; }
        public String category { get; set; }
        public String question { get; set; }
        [AllowHtml]
        public String answer { get; set; }
        public String deployYn { get; set; }
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
                else if ("leaseIntro".Equals(category))
                {
                    return "리스 소개";
                }
                else if ("contract".Equals(category))
                {
                    return "계약/등록";
                }
                else if ("finance".Equals(category))
                {
                    return "금융조건의 변경";
                }
                else if ("cost".Equals(category))
                {
                    return "비용/서류";
                }
                else if ("lease".Equals(category))
                {
                    return "리스 승계";
                }
                else if ("process".Equals(category))
                {
                    return "만기 시 처리";
                }
                else if ("insurance".Equals(category))
                {
                    return "보험";
                }
                else if ("overdue".Equals(category))
                {
                    return "연체";
                }
                else if ("accounting".Equals(category))
                {
                    return "회계처리";
                }
                else if ("homepage".Equals(category))
                {
                    return "홈페이지 이용 관련";
                }
                else
                {
                    return null;
                }
            }
        }

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

        public string searchOptionStr
        {
            get
            {

                return "";
            }
        }

        public
        override String ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + ", " : "") +
                (!string.IsNullOrWhiteSpace(categoryName) ? "카테고리 : " + (categoryName.Length > 5 ? categoryName.Substring(0, 5) + "..." : categoryName) + ", " : "") +
                (!string.IsNullOrWhiteSpace(question) ? "질문 : " + (question.Length > 5 ? question.Substring(0, 5) + "..." : question) + ", " : "") +
                (!string.IsNullOrWhiteSpace(answer) ? "답변 : " + (answer.Length > 5 ? answer.Substring(0, 5) + "..." : answer) + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYn) ? "배포여부 : " + deployYnStr + ", " : "") + 
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOption + " 검색어 : " + searchInput : "");
        }
    }
}