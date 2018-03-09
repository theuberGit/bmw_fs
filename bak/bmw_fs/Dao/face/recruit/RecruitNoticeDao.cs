using bmw_fs.Models.recruit;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.recruit
{
    public class RecruitNoticeDao
    {
        public IList<RecruitNotice> findAll(RecruitNotice recruitNotice)
        {
            return Mapper.Instance().QueryForList<RecruitNotice>("recruitNotice.findAll", recruitNotice);
        }

        public void insertRecruitNotice(RecruitNotice recruitNotice)
        {
            Mapper.Instance().Insert("recruitNotice.insertRecruitNotice", recruitNotice);
        }

        public RecruitNotice findRecruitNotice(RecruitNotice recruitNotice)
        {
            return Mapper.Instance().QueryForObject<RecruitNotice>("recruitNotice.findRecruitNotice", recruitNotice);
        }

        public int findAllCount(RecruitNotice recruitNotice)
        {
            return Mapper.Instance().QueryForObject<int>("recruitNotice.findAllCount", recruitNotice);
        }

        public void updateRecruitNotice(RecruitNotice recruitNotice)
        {
            Mapper.Instance().Update("recruitNotice.updateRecruitNotice", recruitNotice);
        }

        public void deleteRecruitNotice(RecruitNotice recruitNotice)
        {
            Mapper.Instance().Delete("recruitNotice.deleteRecruitNotice", recruitNotice);
        }
    }
}