using bmw_fs.Models.legalNotice;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.legalNotice
{
    public class ProductDao
    {
        public IList<Product> findAll(Product product)
        {
            return Mapper.Instance().QueryForList<Product>("product.findAll", product);
        }

        public int findAllCount(Product product)
        {
            return Mapper.Instance().QueryForObject<int>("product.findAllCount", product);
        }

        public void insertProduct(Product product)
        {
            Mapper.Instance().Insert("product.insertProduct", product);
        }

        public Product findProduct(Product product)
        {
            return Mapper.Instance().QueryForObject<Product>("product.findProduct", product);
        }

        public void updateProduct(Product product)
        {
            Mapper.Instance().Update("product.updateProduct", product);
        }

        public void deleteProduct(Product product)
        {
            Mapper.Instance().Delete("product.deleteProduct", product);
        }

    }
}