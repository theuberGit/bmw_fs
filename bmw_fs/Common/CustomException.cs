using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Common
{
    public class CustomException : Exception
    {
        public CustomException(String message) : base(message)
        {
        }
    }
}