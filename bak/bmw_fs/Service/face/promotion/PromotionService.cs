using bmw_fs.Models.common;
using bmw_fs.Models.promotion;
using System;
using System.Collections.Generic;
using System.Web;

namespace bmw_fs.Service.face.promotion
{
    interface PromotionService
    {
        void insertPromotion(HttpFileCollectionBase multipartFiles, Promotion promotion);

        IList<Promotion> findAll(Promotion promotion);

        int findAllCount(Promotion promotion);

        Promotion findPromotion(Promotion promotion);

        void updatePromotion(HttpFileCollectionBase multipartFiles, Promotion promotion);

        void deletePromotion(Promotion promotion);

        IList<String> findPromotionImgUrl(Promotion promotion, IList<Files> mainImgList);

        IList<String> findPromotionImgUrlEng(Promotion promotion, IList<Files> mainImgEngList);
    }
}
