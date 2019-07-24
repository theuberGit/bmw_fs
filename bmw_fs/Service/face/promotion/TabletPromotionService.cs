using bmw_fs.Models.promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.face.promotion
{
    interface TabletPromotionService
    {
        void insertTabletPromotion(HttpFileCollectionBase multipartFiles, TabletPromotion tabletPromotion);
        
        IList<TabletPromotion> findAll(TabletPromotion tabletPromotion);

        int findAllCount(TabletPromotion tabletPromotion);

        TabletPromotion findTabletPromotion(TabletPromotion tabletPromotion);

        void updateTabletPromotion(HttpFileCollectionBase multipartFiles, TabletPromotion tabletPromotion);

        void deleteTabletPromotion(TabletPromotion tabletPromotion);
    }
}