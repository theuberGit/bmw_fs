using bmw_fs.Models.recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.recruit
{
    interface RecruitNoticeService
    {
        IList<RecruitNotice> findAll(RecruitNotice recruitNotice);

        void insertRecruitNotice(HttpFileCollectionBase multipartFiles, RecruitNotice recruitNotice);

        RecruitNotice findRecruitNotice(RecruitNotice recruitNotice);

        int findAllCount(RecruitNotice recruitNotice);

        void updateRecruitNotice(HttpFileCollectionBase multipartFiles, RecruitNotice recruitNotice);

        void deleteRecruitNotice(RecruitNotice recruitNotice);
        
    }
}
