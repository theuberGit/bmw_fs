using bmw_fs.Models.legalNotice;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.legalNotice
{
    public class GeneralDao
    {
        public IList<General> findAll(General general)
        {
            return Mapper.Instance().QueryForList<General>("general.findAll", general);
        }
        public int findAllCount(General general)
        {
            return Mapper.Instance().QueryForObject<int>("general.findAllCount", general);
        }
        public void insertGeneral(General general)
        {
            Mapper.Instance().Insert("general.insertGeneral", general);
        }
        public General findGeneral(General general)
        {
            return Mapper.Instance().QueryForObject<General>("general.findGeneral", general);
        }
        public void updateGeneral(General general)
        {
            Mapper.Instance().Update("general.updateGeneral", general);
        }
        public void deleteGeneral(General general)
        {
            Mapper.Instance().Delete("general.deleteGeneral", general);
        }
    }
}