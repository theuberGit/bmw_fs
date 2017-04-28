using bmw_fs.Models.catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.catalog
{
    interface CatalogService
    {
        void insertCatalog(HttpFileCollectionBase multipartFiles, Catalog catalog);

        IList<Catalog> findAll(Catalog catalog);

        int findAllCount(Catalog catalog);

        Catalog findCatalog(Catalog catalog);

        void updateCatalog(HttpFileCollectionBase multipartFiles, Catalog catalog);

        void deleteCatalog(Catalog catalog);
    }
}
