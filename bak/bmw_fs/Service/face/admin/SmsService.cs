using bmw_fs.Models.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.admin
{
    interface SmsService
    {
        void insertSms(String subject, String phone, String callback, String msg);
    }
}
