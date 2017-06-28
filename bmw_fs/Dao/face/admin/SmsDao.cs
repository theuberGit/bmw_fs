using bmw_fs.Models.admin;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace bmw_fs.Dao.face.admin
{
    public class SmsDao
    {
        static DomSqlMapBuilder dom = new DomSqlMapBuilder();
        static ISqlMapper mapper = dom.Configure(@".\SqlMapSms.config");

        public void insertSms(Sms sms)
        {
            mapper.Insert("sms.insertSms", sms);
        }
        
    }
}