using bmw_fs.Service.face.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.CustomerService;
using bmw_fs.Dao.face.CustomerService;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;

namespace bmw_fs.Service.impl.CustomerService
{
    public class DownLoadFormServiceImpl : DownLoadFormService
    {
        private DownLoadFormDao downloadFormDao = new DownLoadFormDao();
        private FilesService filesService = new FilesServiceImpl();
        private SequenceService sequenceService = new SequenceServiceImpl(); //시퀀스생성

        public IList<DownLoadForm> findAll(DownLoadForm downloadForm)
        {
            return this.downloadFormDao.findAll(downloadForm);
        }

        public int findAllCount(DownLoadForm downloadForm)
        {
            return this.downloadFormDao.findAllCount(downloadForm);
        }

        public DownLoadForm findDownLoadForm(DownLoadForm downloadForm)
        {
            DownLoadForm findDownLoadFormOne = this.downloadFormDao.findDownloadForm(downloadForm);

            if(findDownLoadFormOne == null) throw new CustomException("데이터가 존재하지 않습니다.");

            return findDownLoadFormOne;
        }
        
        public void insertDownLoadForm(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm)
        {
            int masterIdx = this.sequenceService.getSequenceMasterIdx();
            downloadForm.idx = masterIdx;
            validation(multipartFiles, downloadForm);
            try { 
                Mapper.Instance().BeginTransaction();                
                this.downloadFormDao.insertDownloadForm(downloadForm);
                this.filesService.fileUpload(multipartFiles, "formFile", "pdf", 10 * 1024 * 1024, masterIdx, null);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void updateDownLoadForm(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm)
        {
            findDownLoadForm(downloadForm);
            validation(multipartFiles, downloadForm);
            try { 
                Mapper.Instance().BeginTransaction();
                this.filesService.deleteFileAndFileUpload(multipartFiles, "formFile", "pdf", 10 * 1024 * 1024, downloadForm.idx, downloadForm.fileIdxs);                
                this.downloadFormDao.updateDownloadForm(downloadForm);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void deleteDownLoadForm(DownLoadForm downloadForm)
        {
            findDownLoadForm(downloadForm);
            try { 
                Mapper.Instance().BeginTransaction();
                this.downloadFormDao.deleteDownloadForm(downloadForm);
                this.filesService.deleteRealFilesAndDataByFileMasterIdx(downloadForm.idx);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        private void validation(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm)
        {
            if (String.IsNullOrWhiteSpace(downloadForm.formName)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(downloadForm.usagePurpose)) throw new CustomException("필수 값이 없습니다.");
            if (multipartFiles.Count < 0) throw new CustomException("필수 값이 없습니다.");
        }
    }
}