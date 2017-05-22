using bmw_fs.Service.face.promotion;
using bmw_fs.Dao.face.promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using bmw_fs.Models.promotion;
using IBatisNet.DataMapper;
using System.Text.RegularExpressions;
using bmw_fs.Common;
using System.Diagnostics;
using bmw_fs.Models.common;

namespace bmw_fs.Service.impl.promotion
{
    public class PromotionServiceImpl : PromotionService
    {
        PromotionDao promotionDao = new PromotionDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void insertPromotion(HttpFileCollectionBase multipartFiles, Promotion promotion)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            promotion.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(promotion);
            promotionDao.insertPromotion(promotion);

            filesService.fileUpload(multipartFiles, "thumbNail", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//섬네일 (국문)
            Files files =  filesService.fileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//본문 (국문)

            PromotionUrl promotionUrl = new PromotionUrl();
            for (int i = 0; i < files.fileIdxs.Count; i++)
            {
                promotionUrl.pIdx = masterIdx;
                promotionUrl.fileIdx = files.fileIdxs[i];
                promotionUrl.url = promotion.mainImgLinks[i];
                promotionDao.insertPromotionUrl(promotionUrl);//국문 본문 이미지 링크 INSERT
            }

            if ("Y".Equals(promotion.engYn))
            {
                PromotionUrl promotionUrlEng = new PromotionUrl();
                filesService.fileUpload(multipartFiles, "engThumbNail", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//섬네일 (영문)
                Files filesEng = filesService.fileUpload(multipartFiles, "engMainImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//본문 (영문)
                for (int j = 0; j < filesEng.fileIdxs.Count; j++)
                {
                    promotionUrlEng.pIdx = masterIdx;
                    promotionUrlEng.fileIdx = filesEng.fileIdxs[j];
                    promotionUrlEng.url = promotion.mainImgEngLinks[j];
                    promotionDao.insertPromotionUrl(promotionUrlEng);//영문 본문 이미지 링크 INSERT
                }
            }
            
            Mapper.Instance().CommitTransaction();

        }

        public IList<Promotion> findAll(Promotion promotion)
        {
            return this.promotionDao.findAll(promotion);
        }

        public int findAllCount(Promotion promotion)
        {
            return this.promotionDao.findAllCount(promotion);
        }

        public Promotion findPromotion(Promotion promotion)
        {
            return this.promotionDao.findPromotion(promotion);
        }

        public void updatePromotion(HttpFileCollectionBase multipartFiles, Promotion promotion)
        {
            Mapper.Instance().BeginTransaction();
            filesService.deleteFileAndFileUpload(multipartFiles, "thumbNail", "jpg|png", 5 * 1024 * 1024, promotion.idx, promotion.thumbIdxs);//섬네일 (국문)
            Files files = filesService.deleteFileAndFileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, promotion.idx, promotion.mainIdxs);//본문 (국문)

            PromotionUrl promotionUrl = new PromotionUrl();
            promotionUrl.pIdx = promotion.idx;
            promotionDao.deletePromotionUrl(promotionUrl);//기존 이미지url 삭제(국/영문)
            for (int i = 0; i < files.fileIdxs.Count; i++)
            {
                promotionUrl.fileIdx = files.fileIdxs[i];
                promotionUrl.url = promotion.mainImgLinks[i];
                promotionDao.insertPromotionUrl(promotionUrl);//국문 본문 이미지 링크 INSERT
            }
            if ("Y".Equals(promotion.engYn))
            {
                filesService.deleteFileAndFileUpload(multipartFiles, "engThumbNail", "jpg|png", 5 * 1024 * 1024, promotion.idx, promotion.engThumbIdxs);//섬네일 (영문)
                Files filesEng = filesService.deleteFileAndFileUpload(multipartFiles, "engMainImg", "jpg|png", 5 * 1024 * 1024, promotion.idx, promotion.engMainIdxs);//본문 (영문)

                PromotionUrl promotionUrlEng = new PromotionUrl();
                promotionUrlEng.pIdx = promotion.idx;
                for (int i = 0; i < filesEng.fileIdxs.Count; i++)
                {
                    promotionUrlEng.fileIdx = filesEng.fileIdxs[i];
                    promotionUrlEng.url = promotion.mainImgEngLinks[i];
                    promotionDao.insertPromotionUrl(promotionUrlEng);//영문 본문 이미지 링크 INSERT
                }
            }
            else
            {
                //영문사이트 파일 삭제
                filesService.deleteRealFilesAndDataByFileMasterIdxAndTp(promotion.idx, "engThumbNail");
                filesService.deleteRealFilesAndDataByFileMasterIdxAndTp(promotion.idx, "engMainImg");
            }
            validation(promotion);
            this.promotionDao.updatePromotion(promotion);
            Mapper.Instance().CommitTransaction();
        }

        public void deletePromotion(Promotion promotion)
        {
            Mapper.Instance().BeginTransaction();
            PromotionUrl promotionUrl = new PromotionUrl();
            promotionUrl.pIdx = promotion.idx;
            this.promotionDao.deletePromotion(promotion);
            filesService.deleteRealFilesAndDataByFileMasterIdx(promotion.idx);//파일 삭제
            promotionDao.deletePromotionUrl(promotionUrl);//관련 url 삭제
            Mapper.Instance().CommitTransaction();
        }

        private void validation(Promotion promotion)
        {
            if (String.IsNullOrWhiteSpace(promotion.startDate)) throw new CustomException("필수 값이 없습니다.(게시 시작일)");
            if (String.IsNullOrWhiteSpace(promotion.endDate)) throw new CustomException("필수 값이 없습니다.(게시 종료일)");
            if (String.IsNullOrWhiteSpace(promotion.engYn)) throw new CustomException("필수 값이 없습니다.(영문사이트 여부)");
            if (String.IsNullOrWhiteSpace(promotion.deployYn)) throw new CustomException("필수 값이 없습니다.(배포 여부)");
            if (String.IsNullOrWhiteSpace(promotion.title)) throw new CustomException("필수 값이 없습니다.(제목)");
            if (String.IsNullOrWhiteSpace(promotion.productBannerYn)) throw new CustomException("필수 값이 없습니다.(상품페이지 배너 여부)");
            if ("Y".Equals(promotion.engYn))
            {
                if (String.IsNullOrWhiteSpace(promotion.titleEng)) throw new CustomException("필수 값이 없습니다.(영문 제목)");
            }
        }

        public IList<string> findPromotionImgUrl(Promotion promotion, IList<Files> mainImgList)
        {
            IList<String> mainImgUrl = new List<String>();
            for(int i = 0; i < mainImgList.Count; i++)
            {
                promotion.fileIdx = mainImgList[i].fileIdx;
                mainImgUrl.Add(promotionDao.findPromotionImgUrl(promotion));
            }
            return mainImgUrl;
        }

        public IList<string> findPromotionImgUrlEng(Promotion promotion, IList<Files> mainImgEngList)
        {
            IList<String> mainImgUrlEng = new List<String>();
            for (int i = 0; i < mainImgEngList.Count; i++)
            {
                promotion.fileIdx = mainImgEngList[i].fileIdx;
                mainImgUrlEng.Add(promotionDao.findPromotionImgUrl(promotion));
            }
            return mainImgUrlEng;
        }
    }
}