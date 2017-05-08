using bmw_fs.Models.legalNotice;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.legalNotice
{
    public class CreditDao
    {
        public IList<Credit> findAll(Credit credit)
        {
            return Mapper.Instance().QueryForList<Credit>("credit.findAll", credit);
        }

        public int findAllCount(Credit credit)
        {
            return Mapper.Instance().QueryForObject<int>("credit.findAllCount", credit);
        }

        public void insertCredit(Credit credit)
        {
            Mapper.Instance().Insert("credit.insertCredit", credit);
        }

        public Credit findCredit(Credit credit)
        {
            return Mapper.Instance().QueryForObject<Credit>("credit.findCredit", credit);
        }

        public void updateCredit(Credit credit)
        {
            Mapper.Instance().Update("credit.updateCredit", credit);
        }

        public void deleteCredit(Credit credit)
        {
            Mapper.Instance().Delete("credit.deleteCredit", credit);
        }
    }
}