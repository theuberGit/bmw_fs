using bmw_fs.Models.legalNotice;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.legalNotice
{
    public class BusinessDao
    {
        public IList<Business> findAll(Business business)
        {
            return Mapper.Instance().QueryForList<Business>("business.findAll", business);
        }

        public int findAllCount(Business business)
        {
            return Mapper.Instance().QueryForObject<int>("business.findAllCount", business);
        }

        public void insertBusiness(Business business)
        {
            Mapper.Instance().Insert("business.insertBusiness", business);
        }

        public Business findBusiness(Business business)
        {
            return Mapper.Instance().QueryForObject<Business>("business.findBusiness", business);
        }

        public void updateBusiness(Business business)
        {
            Mapper.Instance().Update("business.updateBusiness", business);
        }

        public void deleteBusiness(Business business)
        {
            Mapper.Instance().Delete("business.deleteBusiness", business);
        }
    }
}