using bmw_fs.Service.face.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.legalNotice;
using bmw_fs.Dao.face.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;

namespace bmw_fs.Service.impl.legalNotice
{
    public class ProductServiceImpl : ProductService
    {
        ProductDao productDao = new ProductDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void deleteProduct(Product product)
        {
            try {
                Mapper.Instance().BeginTransaction();
                productDao.deleteProduct(product);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }

        }

        public IList<Product> findAll(Product product)
        {
            return productDao.findAll(product);
        }

        public int findAllCount(Product product)
        {
            return productDao.findAllCount(product);
        }

        public Product findProduct(Product product)
        {
            return productDao.findProduct(product);
        }

        public void insertProduct(Product product)
        {
            validation(product);
            try {
                int masterIdx = sequenceService.getSequenceMasterIdx();
                product.idx = masterIdx;
                Mapper.Instance().BeginTransaction();                
                productDao.insertProduct(product);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }

        }

        public void updateProduct(Product product)
        {
            validation(product);
            try {
                Mapper.Instance().BeginTransaction();                
                productDao.updateProduct(product);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        private void validation(Product product)
        {
            if (String.IsNullOrWhiteSpace(product.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(product.category)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(Regex.Replace(product.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
}