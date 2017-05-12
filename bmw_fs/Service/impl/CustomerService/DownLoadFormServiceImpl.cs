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
        DownLoadFormDao downloadFormDao = new DownLoadFormDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl(); //시퀀스생성

        public IList<DownLoadForm> findAll(DownLoadForm downloadForm)
        {
            return downloadFormDao.findAll(downloadForm);
        }

        public int findAllCount(DownLoadForm downloadForm)
        {
            return downloadFormDao.findAllCount(downloadForm);
        }

        public DownLoadForm findDownLoadForm(DownLoadForm downloadForm)
        {
            return downloadFormDao.findDownloadForm(downloadForm);
        }
        
        public void insertDownLoadForm(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            downloadForm.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(multipartFiles, downloadForm);
            downloadFormDao.insertDownloadForm(downloadForm);
            filesService.fileUpload(multipartFiles, "formFile", "pdf", 5 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        public void updateDownLoadForm(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm)
        {
            //throw new NotImplementedException();
        }

        public void deleteDownLoadForm(DownLoadForm downloadForm)
        {
          
        }

        private void validation(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm)
        {
            if (String.IsNullOrWhiteSpace(downloadForm.formName)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(downloadForm.usagePurpose)) throw new CustomException("필수 값이 없습니다.");
            if (multipartFiles.Count < 0) throw new CustomException("필수 값이 없습니다.");

            
        }
    }
}