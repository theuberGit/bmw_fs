using bmw_fs.Common;
using bmw_fs.Dao.face.recruit;
using bmw_fs.Models.recruit;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.recruit;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.recruit
{
    public class RecruitNoticeServiceImpl : RecruitNoticeService
    {
        RecruitNoticeDao recruitNoticeDao = new RecruitNoticeDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<RecruitNotice> findAll(RecruitNotice recruitNotice)
        {
            return recruitNoticeDao.findAll(recruitNotice);
        }

        public void insertRecruitNotice(HttpFileCollectionBase multipartFiles, RecruitNotice recruitNotice)
        {
            validation(recruitNotice);
            int masterIdx = sequenceService.getSequenceMasterIdx();
            recruitNotice.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            recruitNoticeDao.insertRecruitNotice(recruitNotice);
            filesService.fileUpload(multipartFiles, "files", "jpg|png|pdf|docx|hwp", 5 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        public RecruitNotice findRecruitNotice(RecruitNotice recruitNotice)
        {
            return recruitNoticeDao.findRecruitNotice(recruitNotice);  
        }

        public int findAllCount(RecruitNotice recruitNotice)
        {
            return recruitNoticeDao.findAllCount(recruitNotice);
        }

        public void updateRecruitNotice(HttpFileCollectionBase multipartFiles, RecruitNotice recruitNotice)
        {
            validation(recruitNotice);
            Mapper.Instance().BeginTransaction();
            filesService.deleteFileAndFileUpload(multipartFiles, "files", "jpg|png|pdf|docx|hwp", 5 * 1024 * 1024, recruitNotice.idx, recruitNotice.fileIdxs);
            recruitNoticeDao.updateRecruitNotice(recruitNotice);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteRecruitNotice(RecruitNotice recruitNotice)
        {
            Mapper.Instance().BeginTransaction();
            recruitNoticeDao.deleteRecruitNotice(recruitNotice);
            filesService.deleteRealFilesAndDataByFileMasterIdx(recruitNotice.idx);
            Mapper.Instance().CommitTransaction();
        }

        private void validation(RecruitNotice recruitNotice)
        {
            if (String.IsNullOrWhiteSpace(recruitNotice.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(recruitNotice.contents)) throw new CustomException("필수 값이 없습니다.");
            if (recruitNotice.startDate == DateTime.MinValue) throw new CustomException("필수 값이 없습니다.");
            if (recruitNotice.endDate  == DateTime.MinValue) throw new CustomException("필수 값이 없습니다.");
        }

    }
}