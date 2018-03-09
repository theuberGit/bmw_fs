using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Models.admin
{
    public class Sms
    {
        public String subject { get; set; }
        public String callback { get; set; }
        public String phone { get; set; }
        public String msg { get; set; }
    }
}