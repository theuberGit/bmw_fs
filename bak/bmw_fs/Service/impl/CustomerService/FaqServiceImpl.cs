using bmw_fs.Service.face.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.CustomerService;
using bmw_fs.Dao.face.CustomerService;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;

namespace bmw_fs.Service.impl.CustomerService
{
    public class FaqServiceImpl : FaqService
    {
        FaqDao faqDao = new FaqDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void deleteFaq(Faq faq)
        {
            try { 
                Mapper.Instance().BeginTransaction();
                faqDao.deleteFaq(faq);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public IList<Faq> findAll(Faq faq)
        {
            return faqDao.findAll(faq);
        }

        public int findAllCount(Faq faq)
        {
            return faqDao.findAllCount(faq);
        }

        public Faq findFaq(Faq faq)
        {
            return faqDao.findFaq(faq);
        }

        public void insertFaq(Faq faq)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            faq.idx = masterIdx;
            validation(faq);
            try { 
                Mapper.Instance().BeginTransaction();            
                faqDao.insertFaq(faq);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void updateFaq(Faq faq)
        {
            validation(faq);
            try { 
                Mapper.Instance().BeginTransaction();
                faqDao.updateFaq(faq);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }

        }
        
        private void validation(Faq faq)
        {
            if (String.IsNullOrWhiteSpace(faq.question)) throw new CustomException("필수 값이 없습니다.(항목)");
            if (String.IsNullOrWhiteSpace(faq.answer)) throw new CustomException("필수 값이 없습니다.(답변)");
            if (String.IsNullOrWhiteSpace(Regex.Replace(faq.question, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
}