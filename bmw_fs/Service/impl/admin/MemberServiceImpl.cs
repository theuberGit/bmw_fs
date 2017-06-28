using bmw_fs.Common;
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

        public bool findMemberDuplicated(Member member)
        {
            return memberDao.findMemberDuplicated(member) > 0 ? true : false;
        }

        public void insertMember(Member member)
        {
            String roles = "";
            if(member.roles.Count != 0)
            {
                for (int i = 0; i < member.roles.Count; i++)
                {
                    if (i == 0)
                    {
                        roles = member.roles[i];
                    }
                    else
                    {
                        roles = roles + "," + member.roles[i];
                    }
                }
                member.role = roles;
            }
           
            regValidation(member);
            memberDao.insertMember(member);
        }

        public void updateMember(Member member)
        {
            String roles = "";
            if (member.roles.Count != 0)
            {
                for (int i = 0; i < member.roles.Count; i++)
                {
                    if (i == 0)
                    {
                        roles = member.roles[i];
                    }
                    else
                    {
                        roles = roles + "," + member.roles[i];
                    }
                }
                member.role = roles;
            }
            modValidation(member);
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

        private void regValidation(Member member)
        {
            if (String.IsNullOrWhiteSpace(member.userId)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(member.password)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(member.role)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(member.activeYn)) throw new CustomException("필수 값이 없습니다.");
        }

        private void modValidation(Member member)
        {
            if (String.IsNullOrWhiteSpace(member.role)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(member.activeYn)) throw new CustomException("필수 값이 없습니다.");
        }
    }
}