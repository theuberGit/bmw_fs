using bmw_fs.Models.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.admin
{
    interface MemberService
    {
        IList<Member> findAll(Member member);

        int findAllCount(Member member);

        Member findMemberByIdx(Member member);

        Member findMemberForLogin(Member member);

        void insertMember(Member member);

        void updateMember(Member member);

        void deleteMember(Member member);

        void updateLoginDate(Member member);

        bool findMemberDuplicated(Member member);


    }
}
