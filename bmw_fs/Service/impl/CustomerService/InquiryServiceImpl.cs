using bmw_fs.Common;
using bmw_fs.Dao.face.CustomerService;
using bmw_fs.Models.CustomerService;
using bmw_fs.Service.face.CustomerService;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
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
            Mapper.Instance().BeginTransaction();
            inquiry.status = "F";
            inquiryDao.updateInquiry(inquiry);
            inquiryDao.updateInquirySendMail(inquiry);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteInquiry(Inquiry inquiry)
        {
            inquiryDao.deleteInquiry(inquiry);
        }
        
        private void validation(Inquiry inquiry)
        {
            if (String.IsNullOrWhiteSpace(inquiry.replyContents)) throw new CustomException("필수 값이 없습니다.");
        }
    }
}