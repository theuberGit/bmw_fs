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
using System.Text.RegularExpressions;
using bmw_fs.Common;

namespace bmw_fs.Service.impl.legalNotice
{
    public class BusinessServiceImpl : BusinessService
    {
        BusinessDao businessDao = new BusinessDao();
        SequenceService sequenceService = new SequenceServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        public IList<Business> findAll(Business business)
        {
            return businessDao.findAll(business);
        }

        public int findAllCount(Business business)
        {
            return businessDao.findAllCount(business);
        }

        public Business findBusiness(Business business)
        {
            return businessDao.findBusiness(business);
        }

        public void insertBusiness(HttpFileCollectionBase multipartFiles, Business business)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            business.idx = masterIdx;
            validation(business);
            try {
                Mapper.Instance().BeginTransaction();
                businessDao.insertBusiness(business);
                filesService.fileUpload(multipartFiles, "file", "pdf|hwp|docx", 10 * 1024 * 1024, masterIdx, null);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void updateBusiness(HttpFileCollectionBase multipartFiles, Business business)
        {
            validation(business);
            try { 
                Mapper.Instance().BeginTransaction();
                if (business.fileIdxs != null)
                { // 처음등록시 파일을 올렸던 경우 
                    filesService.deleteFileAndFileUpload(multipartFiles, "file", "pdf|hwp|docx", 10 * 1024 * 1024, business.idx, business.fileIdxs);
                }
                else if (business.fileIdxs == null) // 처음등록시 파일이 없었던 경우 새로 올림
                {
                    filesService.fileUpload(multipartFiles, "file", "pdf|hwp|docx", 10 * 1024 * 1024, business.idx, null);
                }                
                businessDao.updateBusiness(business);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void deleteBusiness(Business business)
        {
            try { 
                Mapper.Instance().BeginTransaction();
                businessDao.deleteBusiness(business);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        private void validation(Business business)
        {
            if (String.IsNullOrWhiteSpace(business.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(Regex.Replace(business.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
}