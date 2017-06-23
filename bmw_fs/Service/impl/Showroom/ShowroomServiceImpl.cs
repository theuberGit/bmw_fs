using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;
using bmw_fs.Service.face.Showroom;
using bmw_fs.Models.Showroom;
using bmw_fs.Dao.face.Showroom;

namespace bmw_fs.Service.impl.Showroom
{
    public class ShowroomServiceImpl : ShowroomService
    {
        private ShowroomDao showroomDao = new ShowroomDao();

        public IList<ShowRoom> findAll(ShowRoom showroom)
        {
            return this.showroomDao.findAll(showroom);
        }

        public int findAllCount(ShowRoom showroom)
        {
            return this.showroomDao.findAllCount(showroom);
        }

        public void insertShowRoom(ShowRoom showroom)
        {
            validation(showroom);
            try { 
                Mapper.Instance().BeginTransaction();                
                this.showroomDao.insertShowroom(showroom);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public ShowRoom findShowRoom(ShowRoom showroom)
        {
            ShowRoom findShowRoomOne = this.showroomDao.findShowroom(showroom);

            if (findShowRoomOne == null) throw new CustomException("데이터가 존재하지 않습니다.");

            return findShowRoomOne;
        }

        public void updateShowRoom(ShowRoom showroom)
        {
            findShowRoom(showroom);
            validation(showroom);
            try { 
                Mapper.Instance().BeginTransaction();                
                this.showroomDao.updateShowroom(showroom);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void deleteShowRoom(ShowRoom showroom)
        {
            findShowRoom(showroom);
            try {
                Mapper.Instance().BeginTransaction();
                this.showroomDao.deleteShowroom(showroom);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }


        /////////////////////////////////////////////////////////////////////////

        private void validation(ShowRoom showroom)
        {
            if (String.IsNullOrWhiteSpace(showroom.brand)) throw new CustomException("필수 값이 없습니다.(브랜드)");
            if (String.IsNullOrWhiteSpace(showroom.showroomName)) throw new CustomException("필수 값이 없습니다.(전시장명)");
            if (String.IsNullOrWhiteSpace(showroom.location)) throw new CustomException("필수 값이 없습니다.(지역)");
            if (String.IsNullOrWhiteSpace(showroom.address)) throw new CustomException("필수 값이 없습니다.(주소)");
            if (String.IsNullOrWhiteSpace(showroom.lat)) throw new CustomException("필수 값이 없습니다.(위치좌표1)");
            if (String.IsNullOrWhiteSpace(showroom.lng)) throw new CustomException("필수 값이 없습니다.(위치좌표2)");

            if (string.IsNullOrWhiteSpace(showroom.tel1) && string.IsNullOrWhiteSpace(showroom.tel2) && string.IsNullOrWhiteSpace(showroom.tel3))
            {
                showroom.tel1 = "";
                showroom.tel2 = "";
                showroom.tel3 = "";
            }

            if (String.IsNullOrWhiteSpace(showroom.businessTime)) throw new CustomException("필수 값이 없습니다.(영업시간)");
        }
    }
}