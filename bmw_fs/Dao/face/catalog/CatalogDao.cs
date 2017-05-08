using bmw_fs.Models.catalog;
using IBatisNet.DataMapper;
using System.Collections.Generic;

namespace bmw_fs.Dao.face.catalog
{
    public class CatalogDao
    {
        public void insertCatalog(Catalog catalog)
        {

            System.Diagnostics.Debug.WriteLine(catalog.regDate);
           Mapper.Instance().Insert("catalog.insertCatalog", catalog);
        }

        public IList<Catalog> findAll(Catalog catalog)
        {
            return Mapper.Instance().QueryForList<Catalog>("catalog.findAll", catalog);
        }

        public int findAllCount(Catalog catalog)
        {
            return Mapper.Instance().QueryForObject<int>("catalog.findAllCount", catalog);
        }

        public Catalog findCatalog(Catalog catalog)
        {
            return Mapper.Instance().QueryForObject<Catalog>("catalog.findCatalog", catalog);
        }

        public void updateCatalog(Catalog catalog)
        {
            Mapper.Instance().Update("catalog.updateCatalog", catalog);
        }

        public void deleteCatalog(Catalog catalog)
        {
            Mapper.Instance().Delete("catalog.deleteCatalog", catalog);
        }
    }
}