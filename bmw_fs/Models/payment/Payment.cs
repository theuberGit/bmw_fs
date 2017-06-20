using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.payment
{
    public class Payment : SearchInfo
    {
        public int idx { get; set; }
        public String brand { get; set; }
        public String series { get; set; }
        public String model { get; set; }
        public String modelName { get; set; }
        public String price { get; set; }
        public String item { get; set; }
        public String itemName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(item))
                {
                    return null;
                }
                else if ("zangaLease".Equals(item))
                {
                    return "잔가보장형 리스";
                }
                else if ("zangaHalbu".Equals(item))
                {
                    return "잔가보장형 할부";
                }
                else if ("generalHalbu".Equals(item))
                {
                    return "일반 할부";
                }
                else
                {
                    return null;
                }
            }
        }
        public int zlZanga { get; set; }
        public String zlPay1 { get; set; }
        public String zlPay2 { get; set; }
        public String zlPay3 { get; set; }
        public int zhZanga { get; set; }
        public String zhPay1 { get; set; }
        public String zhPay2 { get; set; }
        public String zhPay3 { get; set; }
        public String ghPay1 { get; set; }
        public String ghPay2 { get; set; }
        public String ghPay3 { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        
       //public IList<int> thumbIdxs { get; set; }
        public IList<int> carIdxs { get; set; }
    }
}