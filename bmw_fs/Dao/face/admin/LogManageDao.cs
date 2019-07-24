using bmw_fs.Models.admin;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.admin
{
    public class LogManageDao
    {
        public IList<LogManage> findAll(LogManage logManage)
        {
            return Mapper.Instance().QueryForList<LogManage>("logManage.findAll", logManage);
        }

        public int findAllCount(LogManage logManage)
        {
            return Mapper.Instance().QueryForObject<int>("logManage.findAllCount", logManage);
        }

        public void insertAdminLog(LogManage logManage)
        {
            Mapper.Instance().Insert("logManage.insertAdminLog", logManage);
        }
    }
}