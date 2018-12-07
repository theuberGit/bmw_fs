using bmw_fs.Common;
using bmw_fs.Dao.face.promotion;
using bmw_fs.Models.common;
using bmw_fs.Models.promotion;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.promotion;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.promotion
{
    public class TabletPromotionServiceImpl : TabletPromotionService
    {
        TabletPromotionDao tabletPromotionDao = new TabletPromotionDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void insertTabletPromotion(HttpFileCollectionBase multipartFiles, TabletPromotion tabletPromotion)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            tabletPromotion.idx = masterIdx;
            validation(tabletPromotion);
            try
            {
                Mapper.Instance().BeginTransaction();
                tabletPromotionDao.insertTabletPromotion(tabletPromotion);

                filesService.fileUpload(multipartFiles, "thumbNail", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//섬네일
                filesService.fileUpload(multipartFiles, "bannerImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//띠배너
                filesService.fileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//본문

                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }

            //IList<Promotion> findAll(TabletPromotion tabletPromotion);

            //int findAllCount(TabletPromotion tabletPromotion);

            //Promotion findTabletPromotion(TabletPromotion tabletPromotion);

            //void updateTabletPromotion(HttpFileCollectionBase multipartFiles, TabletPromotion tabletPromotion);

            //void deleteTabletPromotion(TabletPromotion tabletPromotion);


        }

        private void validation(TabletPromotion tabletPromotion)
        {
            if (String.IsNullOrWhiteSpace(tabletPromotion.startDate)) throw new CustomException("필수 값이 없습니다.(게시 시작일)");
            if (String.IsNullOrWhiteSpace(tabletPromotion.endDate)) throw new CustomException("필수 값이 없습니다.(게시 종료일)");
            if (String.IsNullOrWhiteSpace(tabletPromotion.deployYn)) throw new CustomException("필수 값이 없습니다.(배포 여부)");
            if (String.IsNullOrWhiteSpace(tabletPromotion.title)) throw new CustomException("필수 값이 없습니다.(제목)");
        }
    }
}