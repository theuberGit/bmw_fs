using bmw_fs.Dao.face.recruit;
using bmw_fs.Models.recruit;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.recruit;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.recruit
{
    public class RecruitFaqServiceImpl : RecruitFaqService
    {
        RecruitFaqDao recruitFaqDao = new RecruitFaqDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<RecruitFaq> findAll(RecruitFaq recruitFaq)
        {
            return recruitFaqDao.findAll(recruitFaq);
        }

        public void insertRecruitFaq(RecruitFaq recruitFaq)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            recruitFaq.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            recruitFaqDao.insertRecruitFaq(recruitFaq);
            Mapper.Instance().CommitTransaction();
        }

        public RecruitFaq findRecruitFaq(RecruitFaq recruitFaq)
        {
            return recruitFaqDao.findRecruitFaq(recruitFaq);  
        }

        public int findAllCount(RecruitFaq recruitFaq)
        {
            return recruitFaqDao.findAllCount(recruitFaq);
        }

        public void updateRecruitFaq(RecruitFaq recruitFaq)
        {
            Mapper.Instance().BeginTransaction();
            recruitFaqDao.updateRecruitFaq(recruitFaq);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteRecruitFaq(RecruitFaq recruitFaq)
        {
            Mapper.Instance().BeginTransaction();
            recruitFaqDao.deleteRecruitFaq(recruitFaq);
            Mapper.Instance().CommitTransaction();
        }

    }
}