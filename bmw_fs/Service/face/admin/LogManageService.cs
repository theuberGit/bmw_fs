using bmw_fs.Models.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.admin
{
    interface LogManageService
    {
        IList<LogManage> findAll(LogManage logManage);

        int findAllCount(LogManage logManage);

        void insertAdminLog(LogManage logManage);

        string convertMenuNameByUrl(string url);
    }
}
