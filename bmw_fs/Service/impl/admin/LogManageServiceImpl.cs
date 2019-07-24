using bmw_fs.Dao.face.admin;
using bmw_fs.Models.admin;
using bmw_fs.Service.face.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.admin
{
    public class LogManageServiceImpl : LogManageService
    {
        LogManageDao logManageDao = new LogManageDao();

        public IList<LogManage> findAll(LogManage logManage)
        {
            return logManageDao.findAll(logManage);
        }

        public int findAllCount(LogManage logManage)
        {
            return logManageDao.findAllCount(logManage);
        }

        public void insertAdminLog(LogManage logManage)
        {
            logManageDao.insertAdminLog(logManage);
        }

        public string convertMenuNameByUrl(string url)
        {
            if (url.ToLower().Contains("/admin/"))
            {
                return "담당자 관리";
            }
            else if (url.ToLower().Contains("/logmanage/"))
            {
                return "로그 관리";
            }
            else if (url.ToLower().Contains("/promotion/"))
            {
                return "웹 이벤트 관리";
            }
            else if (url.ToLower().Contains("/mobilepromotion/"))
            {
                return "모바일 이벤트 관리";
            }
            else if (url.ToLower().Contains("/catalog/"))
            {
                return "카탈로그 관리";
            }
            else if (url.ToLower().Contains("/payment/"))
            {
                return "월납입금 정보 관리";
            }
            else if (url.ToLower().Contains("/carimage/"))
            {
                return "차량 이미지 관리";
            }
            else if (url.ToLower().Contains("/news/"))
            {
                return "뉴스 관리";
            }
            else if (url.ToLower().Contains("/showroom/"))
            {
                return "전시장 관리";
            }
            else if (url.ToLower().Contains("/privacy/"))
            {
                return "개인정보처리방침";
            }
            else if (url.ToLower().Contains("/credit/"))
            {
                return "신용정보활용체계";
            }
            else if (url.ToLower().Contains("/product/"))
            {
                return "상품약관";
            }
            else if (url.ToLower().Contains("/business/"))
            {
                return "경영공시";
            }
            else if (url.ToLower().Contains("/general/"))
            {
                return "일반공시";
            }
            else if (url.ToLower().Contains("/recruitnotice/"))
            {
                return "채용 공고";
            }
            else if (url.ToLower().Contains("/recruitfaq/"))
            {
                return "채용 FAQ";
            }
            else if (url.ToLower().Contains("/faq/"))
            {
                return "FAQ";
            }
            else if (url.ToLower().Contains("/inquiry/"))
            {
                return "1:1문의";
            }
            else if (url.ToLower().Contains("/downloadform/"))
            {
                return "다운로드 자료/양식";
            }
            else if (url.ToLower().Contains("/login/logoff"))
            {
                return "로그아웃";
            }
            else if (url.ToLower().Contains("/filedownload"))
            {
                return "파일 다운로드";
            }
            else if (url.ToLower().Contains("/imageview"))
            {
                return "이미지 뷰";
            }

            return url;

        }
    }
}