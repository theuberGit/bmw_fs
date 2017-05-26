﻿using bmw_fs.Models.common;
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
        public String ask { get; set; }
        [AllowHtml]
        public String question { get; set; }
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
                else if ("process".Equals(category))
                {
                    return "리스 승계";
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
    }
}