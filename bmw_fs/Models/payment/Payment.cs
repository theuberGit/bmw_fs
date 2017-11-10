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
        public String program { get; set; } //프로그램 선택 ( ZH-SMART 할부 ,ZL-SMART 운용리스 ,GH-일반할부 ,RT-SMART 렌트)
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
        public String rtPay1 { get; set; }
        public String rtPay2 { get; set; }
        public String rtPay3 { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        
       //public IList<int> thumbIdxs { get; set; }
        public IList<int> carIdxs { get; set; }
        public IList<String> programs { get; set; }
        
        public String programsKo
        {
            get
            {
                programs = program.Split(',');
                String programKo = "";
                int count = 0;

                foreach( String str in programs)
                {
                    count += 1;
                    if ("ZH".Equals(str))
                    {
                        programKo += "SMART할부";
                    }
                    else if ("ZL".Equals(str))
                    {
                        programKo += "SMART운용리스";
                    }
                    else if ("GH".Equals(str))
                    {
                        programKo += "일반할부";
                    }
                    else if ("RT".Equals(str))
                    {
                        programKo += "SMART렌트";
                    }
                    else
                    {
                        programKo = null;
                    }
                    //마지막 ,제거
                    if (programKo != null && count != programs.Count )
                    {
                        programKo += ", ";
                    }
                    else
                    {
                        programKo += " ";
                    }
                }
                return programKo;
            }
        }
    }
}