using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.common
{
    interface MailService
    {
        Task<bool> sendMail(String to, String from, String subject, String body);
    }
}
