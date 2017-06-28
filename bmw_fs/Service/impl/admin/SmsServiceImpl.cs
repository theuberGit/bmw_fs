using bmw_fs.Dao.face.admin;
using bmw_fs.Models.admin;
using bmw_fs.Service.face.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.admin
{
    public class SmsServiceImpl : SmsService
    {
        private SmsDao smsDao = new SmsDao();

        public void insertSms(String subject, String phone, String callback, String msg)
        {
            Sms sms = new Sms();
            sms.subject = subject;
            sms.phone = phone;
            sms.callback = callback;
            sms.msg = msg;
            smsDao.insertSms(sms);
        }
    }
}