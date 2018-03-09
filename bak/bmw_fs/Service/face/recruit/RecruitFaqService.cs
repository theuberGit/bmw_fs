using bmw_fs.Models.recruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.recruit
{
    interface RecruitFaqService
    {
        IList<RecruitFaq> findAll(RecruitFaq recruitFaq);

        void insertRecruitFaq(RecruitFaq recruitFaq);

        RecruitFaq findRecruitFaq(RecruitFaq recruitFaq);

        int findAllCount(RecruitFaq recruitFaq);

        void updateRecruitFaq(RecruitFaq recruitFaq);

        void deleteRecruitFaq(RecruitFaq recruitFaq);
        
    }
}
