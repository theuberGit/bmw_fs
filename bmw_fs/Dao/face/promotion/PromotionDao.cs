using bmw_fs.Models.promotion;
using IBatisNet.DataMapper;
using System.Collections.Generic;
using System;

namespace bmw_fs.Dao.face.promotion
{
    public class PromotionDao
    {
        public void insertPromotion(Promotion webPromotion)
        {
           Mapper.Instance().Insert("promotion.insertPromotion", webPromotion);
        }

        public IList<Promotion> findAll(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForList<Promotion>("promotion.findAll", webPromotion);
        }

        public int findAllCount(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForObject<int>("promotion.findAllCount", webPromotion);
        }

        public Promotion findPromotion(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForObject<Promotion>("promotion.findPromotion", webPromotion);
        }

        public void updatePromotion(Promotion webPromotion)
        {
            Mapper.Instance().Update("promotion.updatePromotion", webPromotion);
        }

        public void deletePromotion(Promotion webPromotion)
        {
            Mapper.Instance().Delete("promotion.deletePromotion", webPromotion);
        }

        public void insertPromotionUrl(PromotionUrl promotionUrl)
        {
            Mapper.Instance().Insert("promotion.insertPromotionUrl", promotionUrl);
        }

        public String findPromotionImgUrl(Promotion webPromotion)
        {
           return  Mapper.Instance().QueryForObject<String>("promotion.findPromotionImgUrl", webPromotion);
        }

        public void deletePromotionUrl(PromotionUrl promotionUrl)
        {
            Mapper.Instance().Delete("promotion.deletePromotionUrl", promotionUrl);
        }
    }
}