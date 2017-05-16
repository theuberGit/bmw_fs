using bmw_fs.Models.rollling;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.rolling
{
    public class RollingDao
    {
        public IList<Rolling> findAll(Rolling rolling)
        {
            return Mapper.Instance().QueryForList<Rolling>("rolling.findAll", rolling);
        }

        public int findAllCount(Rolling rolling)
        {
            return Mapper.Instance().QueryForObject<int>("rolling.findAllCount", rolling);
        }

        public void insertRolling(Rolling rolling)
        {
            Mapper.Instance().Insert("rolling.insertRollingt", rolling);
        }

        public Rolling findRolling(Rolling rolling)
        {
            return Mapper.Instance().QueryForObject<Rolling>("rolling.findCredit", rolling);
        }

        public void updateRolling(Rolling rolling)
        {
            Mapper.Instance().Update("rolling.updateRolling", rolling);
        }

        public void deleteRolling(Rolling rolling)
        {
            Mapper.Instance().Delete("rolling.deleteRolling", rolling);
        }
    }
}