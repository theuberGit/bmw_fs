using bmw_fs.Models.privacy;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.privacy
{
    public class PrivacyDao
    {
        public void insertPrivacy(Privacy privacy)
        {
            Mapper.Instance().Insert("privacy.insertPrivacy", privacy);
        }
    }
}