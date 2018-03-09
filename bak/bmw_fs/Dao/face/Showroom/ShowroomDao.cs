using bmw_fs.Models.Showroom;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.Showroom
{
    public class ShowroomDao
    {
        public IList<ShowRoom> findAll(ShowRoom showroom)
        {
            return Mapper.Instance().QueryForList<ShowRoom>("showroom.findAll", showroom);
        }

        public int findAllCount(ShowRoom showroom)
        {
            return Mapper.Instance().QueryForObject<int>("showroom.findAllCount", showroom);
        }

        public void insertShowroom(ShowRoom showroom)
        {
            Mapper.Instance().Insert("showroom.insertShowroom", showroom);
        }

        public ShowRoom findShowroom(ShowRoom showroom)
        {
            return Mapper.Instance().QueryForObject<ShowRoom>("showroom.findShowroom", showroom);
        }

        public void updateShowroom(ShowRoom showroom)
        {
            Mapper.Instance().Update("showroom.updateShowroom", showroom);
        }

        public void deleteShowroom(ShowRoom showroom)
        {
            Mapper.Instance().Delete("showroom.deleteShowroom", showroom);
        }

    }
}