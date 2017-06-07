using System;
using System.Collections.Generic;
using System.Web.Mvc;
using bmw_fs.Models.common;
using System.ComponentModel.DataAnnotations;

namespace bmw_fs.Models.promotion
{
    public class Promotion : SearchInfo
    {
        public int idx { get; set; }
        public String category { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String deployYn { get; set; }
        public String engYn { get; set; }
        public String title { get; set; }
        public String productBannerYn { get; set; }
        public String titleEng { get; set; }
        public String webYn { get; set; }
        public String btn1Name { get; set; }
        public String btn2Name { get; set; }
        public String btn1Link { get; set; }
        public String btn2Link { get; set; }
        public String btn1NameEng { get; set; }
        public String btn2NameEng { get; set; }
        public String btn1LinkEng { get; set; }
        public String btn2LinkEng { get; set; }
        public String note { get; set; }
        public String noteEng { get; set; }

        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public int fileIdx { get; set; }
        //국문
        public IList<int> thumbIdxs { get; set; }
        public IList<int> mainIdxs { get; set; } //본문 이미지 (국문)
        //영문
        public IList<int> engThumbIdxs { get; set; }
        public IList<int> engMainIdxs { get; set; } //본문 이미지 (영문)

        public IList<String> mainImgLinks { get; set; } //국문 본문이미지 url
        public IList<String> mainImgEngLinks { get; set; } //영문 본문이미지 url
    }
}