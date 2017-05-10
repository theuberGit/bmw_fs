using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;
using bmw_fs.Service.face.privacy;
using bmw_fs.Dao.face.legalNotice;

namespace bmw_fs.Service.impl.privacy
{
    public class PrivacyServiceImpl : PrivacyService
    {
        PrivacyDao privacyDao = new PrivacyDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<Privacy> findAll(Privacy privacy)
        {
           return privacyDao.findAll(privacy);
        }

        public int findAllCount(Privacy privacy)
        {
            return privacyDao.findAllCount(privacy);
        }

        public void insertPrivacy(Privacy privacy)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            privacy.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(privacy);
            privacyDao.insertPrivacy(privacy);
            Mapper.Instance().CommitTransaction();
        }

        public Privacy findPrivacy(Privacy privacy)
        {
            return privacyDao.findPrivacy(privacy);
        }

        public void updatePrivacy(Privacy privacy)
        {
            Mapper.Instance().BeginTransaction();
            validation(privacy);
            privacyDao.updatePrivacy(privacy);
            Mapper.Instance().CommitTransaction();
        }

        public void deletePrivacy(Privacy privacy)
        {
            Mapper.Instance().BeginTransaction();
            privacyDao.deletePrivacy(privacy);
            Mapper.Instance().CommitTransaction();
        }

        private void validation(Privacy privacy)
        {
            if (String.IsNullOrWhiteSpace(privacy.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(Regex.Replace(privacy.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
} 