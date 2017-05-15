using bmw_fs.Models.common;
using bmw_fs.Models.promotion;
using System;
using System.Collections.Generic;
using System.Web;

namespace bmw_fs.Service.face.promotion
{
    interface WebPromotionService
    {
        void insertWebPromotion(HttpFileCollectionBase multipartFiles, Promotion webPromotion);

        IList<Promotion> findAll(Promotion webPromotion);

        int findAllCount(Promotion webPromotion);

        Promotion findWebPromotion(Promotion webPromotion);

        void updateWebPromotion(HttpFileCollectionBase multipartFiles, Promotion webPromotion);

        void deleteWebPromotion(Promotion webPromotion);

        IList<String> findWebPromotionImgUrl(Promotion webPromotion, IList<Files> mainImgList);

        IList<String> findWebPromotionImgUrlEng(Promotion webPromotion, IList<Files> mainImgEngList);
    }
}
