using bmw_fs.Models.CustomerService;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.CustomerService
{
    public class FaqDao
    {
        public IList<Faq> findAll(Faq faq)
        {
            return Mapper.Instance().QueryForList<Faq>("faq.findAll", faq);
        }

        public int findAllCount(Faq faq)
        {
            return Mapper.Instance().QueryForObject<int>("faq.findAllCount", faq);
        }

        public void insertFaq(Faq faq)
        {
            Mapper.Instance().Insert("faq.insertFaq", faq);
        }

        public Faq findFaq(Faq faq)
        {
            return Mapper.Instance().QueryForObject<Faq>("faq.findFaq", faq);
        }

        public void updateFaq(Faq faq)
        {
            Mapper.Instance().Update("faq.updateFaq", faq);
        }

        public void deleteFaq(Faq faq)
        {
            Mapper.Instance().Delete("faq.deleteFaq", faq);
        }

    }
}