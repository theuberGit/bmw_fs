using bmw_fs.Service.face.rolling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.rolling;
using IBatisNet.DataMapper;
using bmw_fs.Dao.face.rolling;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using bmw_fs.Common;
using Microsoft.Ajax.Utilities;

namespace bmw_fs.Service.impl.rolling
{
    public class RollingServiceImpl : RollingService
    {
        RollingDao rollingDao = new RollingDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<Rolling> findAll(Rolling rolling)
        {
            return rollingDao.findAll(rolling);
        }

        public int findAllCount(Rolling rolling)
        {
            return rollingDao.findAllCount(rolling);
        }

        public void insertRolling(HttpFileCollectionBase multipartFiles, Rolling rolling)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            rolling.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(rolling);
            this.rollingDao.insertRolling(rolling);
            filesService.fileUpload(multipartFiles, "KRImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            filesService.fileUpload(multipartFiles, "ERImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        //public Rolling findRolling(Rolling rolling)
        //{
        //    return rollingDao.findRolling(rolling);
        //}

        //public void updateRollingt(HttpFileCollectionBase multipartFiles, Rolling rolling)
        //{
        //    Mapper.Instance().BeginTransaction();
        //    filesService.deleteFileAndFileUpload(multipartFiles, "KRImg", "jpg|png", 5 * 1024 * 1024, rolling.idx, rolling.rollingIdx);
        //    validation(rolling);
        //    this.rollingDao.updateRolling(rolling);
        //    Mapper.Instance().CommitTransaction();
        //}

        //public void deleteRolling(Rolling rolling)
        //{
        //    Mapper.Instance().BeginTransaction();
        //    this.rollingDao.deleteRolling(rolling);
        //    filesService.deleteRealFilesAndDataByFileMasterIdx(rolling.idx);
        //    Mapper.Instance().CommitTransaction();
        //}

        public void validation(Rolling rolling)
        {
            if (String.IsNullOrWhiteSpace(rolling.title)) throw new CustomException("필수 값이 없습니다.(제목)");
            if (String.IsNullOrWhiteSpace(rolling.engSite)) throw new CustomException("필수 값이 없습니다.(영문 사이트)");
            if (String.IsNullOrWhiteSpace(rolling.deployYn)) throw new CustomException("필수 값이 없습니다.(배포)");
            if (String.IsNullOrWhiteSpace(rolling.gubun)) throw new CustomException("필수 값이 없습니다.(구분)");
            if (String.IsNullOrWhiteSpace(rolling.korButtonName)) throw new CustomException("필수 값이 없습니다.(버튼명(국문))");
            if (String.IsNullOrWhiteSpace(rolling.KorButtonUrl)) throw new CustomException("필수 값이 없습니다.(버튼URL(국문))");
            if (String.IsNullOrWhiteSpace(rolling.engButtonName)) throw new CustomException("필수 값이 없습니다.(버튼명(영문))");
            if (String.IsNullOrWhiteSpace(rolling.engButtonUrl)) throw new CustomException("필수 값이 없습니다.(버튼URL(영문))");
            //int형식 벨리데이션 처리? (order)
        }
    }
}