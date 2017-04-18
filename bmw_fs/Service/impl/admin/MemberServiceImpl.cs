using bmw_fs.Dao.face.admin;
using bmw_fs.Models.admin;
using bmw_fs.Service.face.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.admin
{
    public class MemberServiceImpl : MemberService
    {
        MemberDao memberDao = new MemberDao();

        public IList<Member> findAll(Member member)
        {
            return memberDao.findAll(member);
        }

        public int findAllCount(Member member)
        {
            return memberDao.findAllCount(member);
        }

        public Member findMemberByIdx(Member member)
        {
            return memberDao.findMemberByIdx(member);
        }

        public Member findMemberForLogin(Member member)
        {
            return memberDao.findMemberForLogin(member);
        }

        public void insertMember(Member member)
        {
            memberDao.insertMember(member);
        }

        public void updateMember(Member member)
        {
            memberDao.updateMember(member);
        }

        public void deleteMember(Member member)
        {
            memberDao.deleteMember(member);
        }

        public void updateLoginDate(Member member)
        {
            memberDao.updateLoginDate(member);
        }
    }
}