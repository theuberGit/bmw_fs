using bmw_fs.Models.promotion;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.promotion
{
    public class TabletPromotionDao
    {
        public void insertTabletPromotion(TabletPromotion tabletPromotion)
        {
            Mapper.Instance().Insert("tabletPromotion.insertTabletPromotion", tabletPromotion);
        }

        public IList<TabletPromotion> findAll(TabletPromotion tabletPromotion)
        {
            return Mapper.Instance().QueryForList<TabletPromotion>("tabletPromotion.findAll", tabletPromotion);
        }

        public int findAllCount(Promotion tabletPromotion)
        {
            return Mapper.Instance().QueryForObject<int>("tabletPromotion.findAllCount", tabletPromotion);
        }

        public TabletPromotion findTabletPromotion(TabletPromotion tabletPromotion)
        {
            return Mapper.Instance().QueryForObject<TabletPromotion>("tabletPromotion.findTabletPromotion", tabletPromotion);
        }

        public void updateTabletPromotion(TabletPromotion tabletPromotion)
        {
            Mapper.Instance().Update("tabletPromotion.updateTabletPromotion", tabletPromotion);
        }

        public void deleteTabletPromotion(TabletPromotion tabletPromotion)
        {
            Mapper.Instance().Delete("tabletPromotion.deleteTabletPromotion", tabletPromotion);
        }
    }
}