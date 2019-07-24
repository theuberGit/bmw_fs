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
        public String program { get; set; } //프로그램 선택 ( ZH-SMART 할부 ,ZL-SMART 운용리스 ,GH-일반할부 ,RT-SMART 렌트, PP-유예할부)
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
        public int ppRate { get; set; }
        public String ppPay1 { get; set; }
        public String ppPay2 { get; set; }
        public String ppPay3 { get; set; }
        public String deployYn { get; set; }
        public String regId { get; set; }
        public DateTime regDate { get; set; }
        public String uptId { get; set; }
        public DateTime uptDate { get; set; }
        public int orderNum { get; set; }
        
       //public IList<int> thumbIdxs { get; set; }
        public IList<int> carIdxs { get; set; }        
        public IList<String> programs { get; set; }

        public IList<int> fileIdxs { get; set; }
        public string fileName { get; set; }

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
                        programKo += "SMART/SMILE 할부";
                    }
                    else if ("ZL".Equals(str))
                    {
                        programKo += "SMART/SMILE 운용리스";
                    }
                    else if ("GH".Equals(str))
                    {
                        programKo += "일반할부";
                    }
                    else if ("RT".Equals(str))
                    {
                        programKo += "SMART/SMILE 렌트";
                    }
                    else if ("PP".Equals(str))
                    {
                        programKo += "유예할부";
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

        public string deployYnStr
        {
            get
            {
                if ("Y".Equals(deployYn))
                {
                    return "배포";
                }
                else if ("N".Equals(deployYn))
                {
                    return "미배포";
                }
                return "";
            }
        }

        public string searchOptionStr
        {
            get
            {
                if ("zangaHalbu".Equals(searchOption))
                {
                    return "SMART/SMILE 할부";
                }
                else if ("zangaLease".Equals(searchOption))
                {
                    return "SAMRT/SMILE 운용리스";
                }
                else if ("generalHalbu".Equals(searchOption))
                {
                    return "일반 할부";
                }
                else if ("smartRent".Equals(searchOption))
                {
                    return "SMART/SMILE 렌트";
                }
                else if ("postponeHalbu".Equals(searchOption))
                {
                    return "유예 할부";
                }

                return "";
            }
        }

        override
        public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + "," : "") +
                (!string.IsNullOrWhiteSpace(brand) ? "브랜드 : " + brand + ", " : "") +
                (!string.IsNullOrWhiteSpace(series) ? "시리즈 : " + series + ", " : "") +
                (!string.IsNullOrWhiteSpace(model) ? "모델 : " + model + ", " : "") +
                (!string.IsNullOrWhiteSpace(program) ? "프로그램 : " + programsKo + ", " : "") +
                (!string.IsNullOrWhiteSpace(modelName) ? "모델명 : " + modelName + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYnStr) ? "배포여부 : " + deployYnStr + ", " : "") +
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색구분 : " + searchOptionStr + " 검색어 : " + searchInput : "");
        }
    }
}