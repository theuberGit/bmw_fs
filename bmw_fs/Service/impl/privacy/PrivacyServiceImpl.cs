using bmw_fs.Service.face.privacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.privacy;
using bmw_fs.Dao.face.privacy;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;

namespace bmw_fs.Service.impl.privacy
{
    public class PrivacyServiceImpl : PrivacyService
    {
        PrivacyDao privacyDao = new PrivacyDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void insertPrivacy(Privacy privacy)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            privacy.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            privacyDao.insertPrivacy(privacy);
            Mapper.Instance().CommitTransaction();
        }
    }
} 