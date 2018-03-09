using bmw_fs.Models.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.legalNotice
{
    interface ProductService
    {
        IList<Product> findAll(Product product);

        int findAllCount(Product product);

        void insertProduct(Product product);

        Product findProduct(Product product);

        void updateProduct(Product product);

        void deleteProduct(Product product);
    }
}
