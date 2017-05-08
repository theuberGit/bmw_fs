using bmw_fs.Service.face.news;
using bmw_fs.Dao.face.news;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using bmw_fs.Models.news;
using IBatisNet.DataMapper;
using System.Text.RegularExpressions;
using bmw_fs.Common;
using System.Diagnostics;

namespace bmw_fs.Service.impl.news
{
    public class NewsServiceImpl : NewsService
    {
        NewsDao newsDao = new NewsDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void insertNews(HttpFileCollectionBase multipartFiles, News news)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            news.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            validation(news);
            newsDao.insertNews(news);
            filesService.fileUpload(multipartFiles, "thumbImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            filesService.fileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, masterIdx, null);
            Mapper.Instance().CommitTransaction();
        }

        public IList<News> findAll(News news)
        {
            return this.newsDao.findAll(news);
        }

        public int findAllCount(News news)
        {
            return this.newsDao.findAllCount(news);
        }

        public IList<News> findAllNotice(News news)
        {
            return this.newsDao.findAllNotice(news);
        }

        public News findNews(News news)
        {
            return this.newsDao.findNews(news);
        }

        public void updateNews(HttpFileCollectionBase multipartFiles, News news)
        {
            Mapper.Instance().BeginTransaction();
            if(("G".Equals(news.type) && news.thumbIdxs[0] != -1) || ("N".Equals(news.type) && news.thumbIdxs[0] != 0 && news.thumbIdxs[0] != -1))
            {
                filesService.deleteFileAndFileUpload(multipartFiles, "thumbImg", "jpg|png", 5 * 1024 * 1024, news.idx, news.thumbIdxs);  
            }else if("N".Equals(news.type) && news.thumbIdxs[0] == 0)//기존에 등록되어있던 경우
            {
                filesService.deleteRealFilesAndDataByFileMasterIdxAndTp(news.idx, "thumbImg");
            }else if(("N".Equals(news.type) && news.thumbIdxs[0] == -1) || ("G".Equals(news.type) && news.thumbIdxs[0] == -1))//새로 등록하는 경우
            {
                filesService.fileUpload(multipartFiles, "thumbImg", "jpg|png", 5 * 1024 * 1024, news.idx, null);
            }

            filesService.deleteFileAndFileUpload(multipartFiles, "mainImg", "jpg|png", 5 * 1024 * 1024, news.idx, news.fileIdxs);
            validation(news);
            this.newsDao.updateNews(news);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteNews(News news)
        {
            Mapper.Instance().BeginTransaction();
            this.newsDao.deleteNews(news);
            filesService.deleteRealFilesAndDataByFileMasterIdx(news.idx);
            Mapper.Instance().CommitTransaction();
        }

        private void validation(News news)
        {
            if (String.IsNullOrWhiteSpace(news.type)) throw new CustomException("필수 값이 없습니다.(구분)");
            if (String.IsNullOrWhiteSpace(news.title)) throw new CustomException("필수 값이 없습니다.(제목)");
            if (String.IsNullOrWhiteSpace(Regex.Replace(news.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.(내용)");
        }
    }
}