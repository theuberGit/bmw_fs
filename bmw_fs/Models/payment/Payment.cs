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
        public int zanga { get; set; }
        public String payment1 { get; set; }
        public String payment2 { get; set; }
        public String payment3 { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        
       //public IList<int> thumbIdxs { get; set; }
        public IList<int> carIdxs { get; set; }
    }
}