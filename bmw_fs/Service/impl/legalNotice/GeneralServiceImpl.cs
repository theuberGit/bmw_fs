
using bmw_fs.Common;
using bmw_fs.Dao.face.legalNotice;
using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.legalNotice;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace bmw_fs.Service.impl.legalNotice
{
    public class GeneralServiceImpl : GeneralService
    {
        GeneralDao generalDao = new GeneralDao();
        SequenceService sequenceService = new SequenceServiceImpl();
        FilesService filesService = new FilesServiceImpl();

        public IList<General> findAll(General general)
        {
            return generalDao.findAll(general);
        }

        public int findAllCount(General general)
        {
            return generalDao.findAllCount(general);
        }

        public General findGeneral(General general)
        {
           return generalDao.findGeneral(general);
        }

        public void insertGeneral(HttpFileCollectionBase multipartFiles, General general)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            general.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            generalDao.insertGeneral(general);
            filesService.fileUpload(multipartFiles, "file", "pdf", 10 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        public void updateGeneral(HttpFileCollectionBase multipartFiles, General general)
        {
            Mapper.Instance().BeginTransaction();
            if (general.fileIdxs != null) { // 처음등록시 파일을 올렸던 경우 
            filesService.deleteFileAndFileUpload(multipartFiles, "file", "pdf", 10 * 1024 * 1024, general.idx, general.fileIdxs);
            }else if (general.fileIdxs == null) // 처음등록시 파일이 없었던 경우 새로 올림
            {
                filesService.fileUpload(multipartFiles, "file", "pdf", 10 * 1024 * 1024, general.idx, null);
            }

            generalDao.updateGeneral(general);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteGeneral(General general)
        {
            Mapper.Instance().BeginTransaction();
            generalDao.deleteGeneral(general);
            Mapper.Instance().CommitTransaction();
        }

        private void validation(General general)
        {
            if (String.IsNullOrWhiteSpace(general.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(Regex.Replace(general.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
}