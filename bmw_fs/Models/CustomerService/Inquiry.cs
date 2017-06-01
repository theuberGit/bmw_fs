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
        public String replyContents { get; set; }
        public String mailSendId { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        
        public DateTime isNullDate { get; set; }
        public String categoryName {
            get {
                if (String.IsNullOrWhiteSpace(category))
                {
                    return null;
                }
                else if ("product".Equals(category))
                {
                    return "상품 문의";
                }
                else if ("purchase".Equals(category))
                {
                    return "차량구입 문의";
                }
                else if ("contract".Equals(category))
                {
                    return "계약관련 문의";
                }
                else if ("arrears".Equals(category))
                {
                    return "연체 문의";
                }
                else if ("refinancing".Equals(category))
                {
                    return "재금융 문의";
                }
                else if ("etc".Equals(category))
                {
                    return "기타";
                }
                else
                {
                    return null;
                }
            }
        }
    }
}