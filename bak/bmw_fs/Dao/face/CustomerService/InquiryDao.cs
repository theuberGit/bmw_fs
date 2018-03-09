using bmw_fs.Models.CustomerService;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.CustomerService
{
    public class InquiryDao
    {
        public IList<Inquiry> findAll(Inquiry inquiry)
        {
            return Mapper.Instance().QueryForList<Inquiry>("inquiry.findAll", inquiry);
        }

        public int findAllCount(Inquiry inquiry)
        {
            return Mapper.Instance().QueryForObject<int>("inquiry.findAllCount", inquiry);
        }

        public Inquiry findInquiry(Inquiry inquiry)
        {
            return Mapper.Instance().QueryForObject<Inquiry>("inquiry.findInquiry", inquiry);
        }

        public void updateInquiry(Inquiry inquiry)
        {
            Mapper.Instance().Update("inquiry.updateInquiry", inquiry);
        }

        public void updateInquirySendMail(Inquiry inquiry)
        {
            Mapper.Instance().Update("inquiry.updateInquirySendMail", inquiry);
        }

        public void deleteInquiry(Inquiry inquiry)
        {
            Mapper.Instance().Delete("inquiry.deleteInquiry", inquiry);
        }

        public IList<Inquiry> findAllExcel(Inquiry inquiry)
        {
            return Mapper.Instance().QueryForList<Inquiry>("inquiry.findAllExcel", inquiry);
        }

    }
}