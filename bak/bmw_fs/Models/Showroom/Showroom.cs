using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.Showroom
{
    public class ShowRoom:SearchInfo
    {
        public int idx { get; set; }
        public String brand { get; set; }
        public String showroomName { get; set; }
        public String dealerName { get; set; }
        public String location { get; set; }
        public String address { get; set; }
        public String lat { get; set; }
        public String lng { get; set; }
        public String tel1 { get; set; }
        public String tel2 { get; set; }
        public String tel3 { get; set; }
        public String businessTime { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
    }
}