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
        private SequenceService sequenceService = new SequenceServiceImpl(); //시퀀스생성

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
            int masterIdx = this.sequenceService.getSequenceMasterIdx();
            showroom.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(showroom);
            this.showroomDao.insertShowroom(showroom);
            Mapper.Instance().CommitTransaction();
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

            Mapper.Instance().BeginTransaction();
            validation(showroom);
            this.showroomDao.updateShowroom(showroom);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteShowRoom(ShowRoom showroom)
        {
            findShowRoom(showroom);

            Mapper.Instance().BeginTransaction();
            this.showroomDao.deleteShowroom(showroom);
            Mapper.Instance().CommitTransaction();
        }


        /////////////////////////////////////////////////////////////////////////

        private void validation(ShowRoom showroom)
        {
            if (String.IsNullOrWhiteSpace(showroom.brand)) throw new CustomException("필수 값이 없습니다.(브랜드)");
            if (String.IsNullOrWhiteSpace(showroom.showroomName)) throw new CustomException("필수 값이 없습니다.(전시장명)");
            if (String.IsNullOrWhiteSpace(showroom.dealerName)) throw new CustomException("필수 값이 없습니다.(딜러명)");
            if (String.IsNullOrWhiteSpace(showroom.location)) throw new CustomException("필수 값이 없습니다.(지역)");
            if (String.IsNullOrWhiteSpace(showroom.address)) throw new CustomException("필수 값이 없습니다.(주소)");
            if (String.IsNullOrWhiteSpace(showroom.lat)) throw new CustomException("필수 값이 없습니다.(위치좌표1)");
            if (String.IsNullOrWhiteSpace(showroom.lng)) throw new CustomException("필수 값이 없습니다.(위치좌표2)");
            if ("-".Equals(showroom.tel1))
            {
                showroom.tel2 = "-";
                showroom.tel3 = "-";
            }
            else
            {
                if (String.IsNullOrWhiteSpace(showroom.tel1)) throw new CustomException("필수 값이 없습니다.(전화번호1)");
                if (String.IsNullOrWhiteSpace(showroom.tel2)) throw new CustomException("필수 값이 없습니다.(전화번호2)");
                if (String.IsNullOrWhiteSpace(showroom.tel3)) throw new CustomException("필수 값이 없습니다.(전화번호3)");
            }
            if (String.IsNullOrWhiteSpace(showroom.businessTime)) throw new CustomException("필수 값이 없습니다.(영업시간)");
        }
    }
}