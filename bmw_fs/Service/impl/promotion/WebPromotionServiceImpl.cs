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
    public class WebPromotionServiceImpl : WebPromotionService
    {
        WebPromotionDao webPromotionDao = new WebPromotionDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void insertWebPromotion(HttpFileCollectionBase multipartFiles, Promotion webPromotion)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            webPromotion.idx = masterIdx;
            webPromotion.webYn = "Y";
            Mapper.Instance().BeginTransaction();
            validation(webPromotion);
            webPromotionDao.insertWebPromotion(webPromotion);

            filesService.fileUpload(multipartFiles, "thumbNail", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//섬네일 (국문)
            Files files =  filesService.fileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//본문 (국문)

            PromotionUrl promotionUrl = new PromotionUrl();
            for (int i = 0; i < files.fileIdxs.Count; i++)
            {
                promotionUrl.pIdx = masterIdx;
                promotionUrl.fileIdx = files.fileIdxs[i];
                promotionUrl.url = webPromotion.mainImgLinks[i];
                webPromotionDao.insertWebPromotionUrl(promotionUrl);//국문 본문 이미지 링크 INSERT
            }

            if ("Y".Equals(webPromotion.engYn))
            {
                PromotionUrl promotionUrlEng = new PromotionUrl();
                filesService.fileUpload(multipartFiles, "engThumbNail", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//섬네일 (영문)
                Files filesEng = filesService.fileUpload(multipartFiles, "engMainImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);//본문 (영문)
                for (int j = 0; j < filesEng.fileIdxs.Count; j++)
                {
                    promotionUrlEng.pIdx = masterIdx;
                    promotionUrlEng.fileIdx = filesEng.fileIdxs[j];
                    promotionUrlEng.url = webPromotion.mainImgEngLinks[j];
                    webPromotionDao.insertWebPromotionUrl(promotionUrlEng);//영문 본문 이미지 링크 INSERT
                }
            }
            
            Mapper.Instance().CommitTransaction();

        }

        public IList<Promotion> findAll(Promotion webPromotion)
        {
            return this.webPromotionDao.findAll(webPromotion);
        }

        public int findAllCount(Promotion webPromotion)
        {
            return this.webPromotionDao.findAllCount(webPromotion);
        }

        public Promotion findWebPromotion(Promotion webPromotion)
        {
            return this.webPromotionDao.findWebPromotion(webPromotion);
        }

        public void updateWebPromotion(HttpFileCollectionBase multipartFiles, Promotion webPromotion)
        {
            webPromotion.webYn = "Y";
            Mapper.Instance().BeginTransaction();
            filesService.deleteFileAndFileUpload(multipartFiles, "thumbNail", "jpg|png", 5 * 1024 * 1024, webPromotion.idx, webPromotion.thumbIdxs);//섬네일 (국문)
            Files files = filesService.deleteFileAndFileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, webPromotion.idx, webPromotion.mainIdxs);//본문 (국문)

            PromotionUrl promotionUrl = new PromotionUrl();
            promotionUrl.pIdx = webPromotion.idx;
            webPromotionDao.deleteWebPromotionUrl(promotionUrl);//기존 이미지url 삭제(국/영문)
            for (int i = 0; i < files.fileIdxs.Count; i++)
            {
                promotionUrl.fileIdx = files.fileIdxs[i];
                promotionUrl.url = webPromotion.mainImgLinks[i];
                webPromotionDao.insertWebPromotionUrl(promotionUrl);//국문 본문 이미지 링크 INSERT
            }
            if ("Y".Equals(webPromotion.engYn))
            {
                filesService.deleteFileAndFileUpload(multipartFiles, "engThumbNail", "jpg|png", 5 * 1024 * 1024, webPromotion.idx, webPromotion.engThumbIdxs);//섬네일 (영문)
                Files filesEng = filesService.deleteFileAndFileUpload(multipartFiles, "engMainImg", "jpg|png", 5 * 1024 * 1024, webPromotion.idx, webPromotion.engMainIdxs);//본문 (영문)

                PromotionUrl promotionUrlEng = new PromotionUrl();
                promotionUrlEng.pIdx = webPromotion.idx;
                for (int i = 0; i < filesEng.fileIdxs.Count; i++)
                {
                    promotionUrlEng.fileIdx = filesEng.fileIdxs[i];
                    promotionUrlEng.url = webPromotion.mainImgEngLinks[i];
                    webPromotionDao.insertWebPromotionUrl(promotionUrlEng);//영문 본문 이미지 링크 INSERT
                }
            }
            else
            {
                //영문사이트 파일 삭제
                filesService.deleteRealFilesAndDataByFileMasterIdxAndTp(webPromotion.idx, "engThumbNail");
                filesService.deleteRealFilesAndDataByFileMasterIdxAndTp(webPromotion.idx, "engMainImg");
            }
            validation(webPromotion);
            this.webPromotionDao.updateWebPromotion(webPromotion);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteWebPromotion(Promotion webPromotion)
        {
            Mapper.Instance().BeginTransaction();
            PromotionUrl promotionUrl = new PromotionUrl();
            promotionUrl.pIdx = webPromotion.idx;
            this.webPromotionDao.deleteWebPromotion(webPromotion);
            filesService.deleteRealFilesAndDataByFileMasterIdx(webPromotion.idx);//파일 삭제
            webPromotionDao.deleteWebPromotionUrl(promotionUrl);//관련 url 삭제
            Mapper.Instance().CommitTransaction();
        }

        private void validation(Promotion webPromotion)
        {
            if (String.IsNullOrWhiteSpace(webPromotion.startDate)) throw new CustomException("필수 값이 없습니다.(게시 시작일)");
            if (String.IsNullOrWhiteSpace(webPromotion.endDate)) throw new CustomException("필수 값이 없습니다.(게시 종료일)");
            if (String.IsNullOrWhiteSpace(webPromotion.engYn)) throw new CustomException("필수 값이 없습니다.(영문사이트 여부)");
            if (String.IsNullOrWhiteSpace(webPromotion.deployYn)) throw new CustomException("필수 값이 없습니다.(배포 여부)");
            if (String.IsNullOrWhiteSpace(webPromotion.title)) throw new CustomException("필수 값이 없습니다.(제목)");
            if (String.IsNullOrWhiteSpace(webPromotion.productBannerYn)) throw new CustomException("필수 값이 없습니다.(상품페이지 배너 여부)");
            if ("Y".Equals(webPromotion.engYn))
            {
                if (String.IsNullOrWhiteSpace(webPromotion.titleEng)) throw new CustomException("필수 값이 없습니다.(영문 제목)");
            }
        }

        public IList<string> findWebPromotionImgUrl(Promotion webPromotion, IList<Files> mainImgList)
        {
            IList<String> mainImgUrl = new List<String>();
            for(int i = 0; i < mainImgList.Count; i++)
            {
                webPromotion.fileIdx = mainImgList[i].fileIdx;
                mainImgUrl.Add(webPromotionDao.findWebPromotionImgUrl(webPromotion));
            }
            return mainImgUrl;
        }

        public IList<string> findWebPromotionImgUrlEng(Promotion webPromotion, IList<Files> mainImgEngList)
        {
            IList<String> mainImgUrlEng = new List<String>();
            for (int i = 0; i < mainImgEngList.Count; i++)
            {
                webPromotion.fileIdx = mainImgEngList[i].fileIdx;
                mainImgUrlEng.Add(webPromotionDao.findWebPromotionImgUrl(webPromotion));
            }
            return mainImgUrlEng;
        }
    }
}