using bmw_fs.Service.face.catalog;
using bmw_fs.Dao.face.catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using bmw_fs.Models.catalog;
using IBatisNet.DataMapper;
using bmw_fs.Common;

namespace bmw_fs.Service.impl.catalog
{
    public class CatalogServiceImpl : CatalogService
    {
        CatalogDao catalogDao = new CatalogDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void insertCatalog(HttpFileCollectionBase multipartFiles, Catalog catalog)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            catalog.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(catalog);
            catalogDao.insertCatalog(catalog);
            filesService.fileUpload(multipartFiles, "file", "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        public IList<Catalog> findAll(Catalog catalog)
        {
            return this.catalogDao.findAll(catalog);
        }

        public int findAllCount(Catalog catalog)
        {
            return this.catalogDao.findAllCount(catalog);
        }

        public Catalog findCatalog(Catalog catalog)
        {
            return this.catalogDao.findCatalog(catalog);
        }

        public void updateCatalog(HttpFileCollectionBase multipartFiles, Catalog catalog)
        {
            Mapper.Instance().BeginTransaction();
            filesService.deleteFileAndFileUpload(multipartFiles, "file", "jpg|png", 5 * 1024 * 1024, catalog.idx, catalog.fileIdxs);
            validation(catalog);
            this.catalogDao.updateCatalog(catalog);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteCatalog(Catalog catalog)
        {
            Mapper.Instance().BeginTransaction();
            this.catalogDao.deleteCatalog(catalog);
            filesService.deleteRealFilesAndDataByFileMasterIdx(catalog.idx);
            Mapper.Instance().CommitTransaction();
        }

        private void validation(Catalog catalog)
        {
            if (String.IsNullOrWhiteSpace(catalog.title)) throw new CustomException("필수 값이 없습니다.(카탈로그)");
        }
    }
}