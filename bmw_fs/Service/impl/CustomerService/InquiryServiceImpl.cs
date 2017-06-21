using bmw_fs.Common;
using bmw_fs.Dao.face.CustomerService;
using bmw_fs.Models.CustomerService;
using bmw_fs.Service.face.CustomerService;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.CustomerService
{
    public class InquiryServiceImpl : InquiryService
    {
        InquiryDao inquiryDao = new InquiryDao();

        public IList<Inquiry> findAll(Inquiry inquiry)
        {
            return inquiryDao.findAll(inquiry);
        }

        public int findAllCount(Inquiry inquiry)
        {
            return inquiryDao.findAllCount(inquiry);
        }

        public Inquiry findInquiry(Inquiry inquiry)
        {
            return inquiryDao.findInquiry(inquiry);
        }

        public void updateInquiry(Inquiry inquiry)
        {
            validation(inquiry);
            inquiryDao.updateInquiry(inquiry);
        }

        public void updateInquirySendMail(Inquiry inquiry)
        {
            validation(inquiry);
            try { 
                Mapper.Instance().BeginTransaction();
                inquiry.status = "F";
                inquiryDao.updateInquiry(inquiry);
                inquiryDao.updateInquirySendMail(inquiry);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void deleteInquiry(Inquiry inquiry)
        {
            inquiryDao.deleteInquiry(inquiry);
        }

        public IList<Inquiry> findAllExcel(Inquiry inquiry)
        {
            return inquiryDao.findAllExcel(inquiry);
        }

        public Object downloadExcel(Inquiry inquiry)
        {
            var inquiryList = new System.Data.DataTable("inquiry");
            inquiryList.Columns.Add("카테고리", typeof(String));
            inquiryList.Columns.Add("제목", typeof(string));
            inquiryList.Columns.Add("작성일", typeof(string));
            inquiryList.Columns.Add("내용", typeof(string));
            inquiryList.Columns.Add("상태", typeof(string));
            inquiryList.Columns.Add("답변내용", typeof(string));
            inquiryList.Columns.Add("답변완료시간", typeof(string));

            IList<Inquiry> list = inquiryDao.findAllExcel(inquiry);
            foreach (Inquiry item in list)
            {
                String sendDate = "";
                String status = "";                
                if (item.mailSendDate == DateTime.MinValue)
                {
                    sendDate = "";
                }
                else
                {
                    sendDate = item.mailSendDate.ToString();
                }
                if ("W".Equals(item.status))
                {
                    status = "접수중";
                }
                else if ("F".Equals(item.status))
                {
                    status = "답변완료";
                }
                else
                {
                    status = "";
                }               
                
                inquiryList.Rows.Add(item.categoryName, item.title, item.regDate, item.contents, status, item.replyContents, sendDate);
            }

            return inquiryList;
        }


        private void validation(Inquiry inquiry)
        {
            if (String.IsNullOrWhiteSpace(inquiry.replyContents)) throw new CustomException("필수 값이 없습니다.");
        }
    }
}