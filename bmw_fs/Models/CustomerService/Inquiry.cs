using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.CustomerService
{
    public class Inquiry : SearchInfo
    {
        public int idx { get; set; }
        public String category { get; set; }
        public String title { get; set; }
        public String contents { get; set; }
        public String email { get; set; }
        public String author { get; set; }
        public DateTime regDate { get; set; }
        public String replyRegId { get; set; }
        public DateTime replyRegDate { get; set; }
        public DateTime mailSendDate { get; set; }
        public String status { get; set; }
        public String delYn { get; set; }
        public String replyContents{ get; set; }
        public String mailSendId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}