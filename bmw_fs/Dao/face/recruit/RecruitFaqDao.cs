using bmw_fs.Models.recruit;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.recruit
{
    public class RecruitFaqDao
    {
        public IList<RecruitFaq> findAll(RecruitFaq recruitFaq)
        {
            return Mapper.Instance().QueryForList<RecruitFaq>("recruitFaq.findAll", recruitFaq);
        }

        public void insertRecruitFaq(RecruitFaq recruitFaq)
        {
            Mapper.Instance().Insert("recruitFaq.insertRecruitFaq", recruitFaq);
        }

        public RecruitFaq findRecruitFaq(RecruitFaq recruitFaq)
        {
            return Mapper.Instance().QueryForObject<RecruitFaq>("recruitFaq.findRecruitFaq", recruitFaq);
        }

        public int findAllCount(RecruitFaq recruitFaq)
        {
            return Mapper.Instance().QueryForObject<int>("recruitFaq.findAllCount", recruitFaq);
        }

        public void updateRecruitFaq(RecruitFaq recruitFaq)
        {
            Mapper.Instance().Update("recruitFaq.updateRecruitFaq", recruitFaq);
        }

        public void deleteRecruitFaq(RecruitFaq recruitFaq)
        {
            Mapper.Instance().Delete("recruitFaq.deleteRecruitFaq", recruitFaq);
        }
    }
}