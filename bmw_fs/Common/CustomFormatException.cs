using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Common
{
    public class CustomFormatException : FormatException
    {
        public CustomFormatException(String message) : base(message)
        {
        }
    }
}