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

        override
        public string ToString()
        {
            return
                (idx > 0 ? "idx : " + idx + ", " : "") +
                (!string.IsNullOrWhiteSpace(brand) ? "브랜드 : " + brand + ", " : "") +
                (!string.IsNullOrWhiteSpace(showroomName) ? "전시장명 : " + showroomName + ", " : "") +
                (!string.IsNullOrWhiteSpace(dealerName) ? "딜러명 : " + dealerName + ", " : "") +
                (!string.IsNullOrWhiteSpace(location) ? "지역 : " + location + ", " : "") +
                (!string.IsNullOrWhiteSpace(address) ? "주소 : " + (address.Length > 5 ? address.Substring(0, 5) + "..." : address) + ", " : "") +
                (!string.IsNullOrWhiteSpace(lat) ? "위도 : " + lat + ", " : "") +
                (!string.IsNullOrWhiteSpace(lng) ? "경도 : " + lng + ", " : "") +
                (!string.IsNullOrWhiteSpace(tel1) ? "전화번호 : " + tel1 + tel2 + tel3 + ", " : "") +
                (!string.IsNullOrWhiteSpace(businessTime) ? "영업시간 : " + businessTime + ", " : "") +
                (!string.IsNullOrWhiteSpace(deployYnStr) ? "배포여부 : " + deployYnStr + ", " : "") + 
                (page > 1 ? "page : " + page + ", " : "") +
                (!string.IsNullOrWhiteSpace(searchOption) ? "검색 구분 : " + searchOption + "검색어 : " + searchInput : "");
        }

    }
}
