using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.admin
{
    public class LogManage : SearchInfo
    {
        public int idx { get; set; }
        public string menuName { get; set; }
        public string qid { get; set; }
        public string contents { get; set; }
        public DateTime regDate { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        override
        public string ToString()
        {
            return
                (idx > 0 ? " idx : " + idx + "," : "") +
                (!string.IsNullOrWhiteSpace(qid) ? " 아이디 : " + qid + "," : "") +
                (!string.IsNullOrWhiteSpace(menuName) ? " 화면명 : " + menuName + "," : "") +
                (!string.IsNullOrWhiteSpace(contents) ? " 조건 : " + contents + "," : "") +
                (!string.IsNullOrWhiteSpace(startDate) || !string.IsNullOrWhiteSpace(endDate) ? " 접속일 : " + startDate + " ~ " + endDate + "," : "") +
                (page > 1 ? " page : " + page : "");

        }

    }
}