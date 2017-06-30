using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.admin
{
    public class Member : SearchInfo
    {
        public int idx { get; set; }
        public String userId { get; set; }
        public String password { get; set; }
        public String role { get; set; }
        public String activeYn { get; set; }
        public DateTime loginDate { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public String tel1 { get; set; }
        public String tel2 { get; set; }
        public String tel3 { get; set; }

        public IList<String> roles { get; set; }
    }
}