using bmw_fs.Models.admin;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.admin
{
    public class MemberDao
    {
        public IList<Member> findAll(Member member)
        {
            return Mapper.Instance().QueryForList<Member>("member.findAll", member);
        }

        public int findAllCount(Member member)
        {
            return Mapper.Instance().QueryForObject<int>("member.findAllCount", member);
        }

        public Member findMemberByIdx(Member member)
        {
            return Mapper.Instance().QueryForObject<Member>("member.findMemberByIdx", member);
        }

        public Member findMemberForLogin(Member member)
        {
            return Mapper.Instance().QueryForObject<Member>("member.findMemberForLogin", member);
        }

        public void insertMember(Member member)
        {
            Mapper.Instance().Insert("member.insertMember", member);
        }

        public void updateMember(Member member)
        {
            Mapper.Instance().Update("member.updateMember", member);
        }

        public void deleteMember(Member member)
        {
            Mapper.Instance().Delete ("member.deleteMember", member);
        }

        public void updateLoginDate(Member member)
        {
            Mapper.Instance().Update("member.updateLoginDate", member);
        }

    }
}