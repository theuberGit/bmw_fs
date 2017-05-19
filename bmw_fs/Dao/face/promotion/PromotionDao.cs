using bmw_fs.Models.promotion;
using IBatisNet.DataMapper;
using System.Collections.Generic;
using System;

namespace bmw_fs.Dao.face.promotion
{
    public class PromotionDao
    {
        public void insertWebPromotion(Promotion webPromotion)
        {
           Mapper.Instance().Insert("promotion.insertWebPromotion", webPromotion);
        }

        public IList<Promotion> findAll(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForList<Promotion>("promotion.findAll", webPromotion);
        }

        public int findAllCount(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForObject<int>("promotion.findAllCount", webPromotion);
        }

        public Promotion findWebPromotion(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForObject<Promotion>("promotion.findWebPromotion", webPromotion);
        }

        public void updateWebPromotion(Promotion webPromotion)
        {
            Mapper.Instance().Update("promotion.updateWebPromotion", webPromotion);
        }

        public void deleteWebPromotion(Promotion webPromotion)
        {
            Mapper.Instance().Delete("promotion.deleteWebPromotion", webPromotion);
        }

        public void insertWebPromotionUrl(PromotionUrl promotionUrl)
        {
            Mapper.Instance().Insert("promotion.insertWebPromotionUrl", promotionUrl);
        }

        public String findWebPromotionImgUrl(Promotion webPromotion)
        {
           return  Mapper.Instance().QueryForObject<String>("promotion.findWebPromotionImgUrl", webPromotion);
        }

        public void deleteWebPromotionUrl(PromotionUrl promotionUrl)
        {
            Mapper.Instance().Delete("promotion.deleteWebPromotionUrl", promotionUrl);
        }
    }
}