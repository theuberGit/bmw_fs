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

        public News findNews(News news)
        {
            return this.newsDao.findNews(news);
        }

        public void updateNews(HttpFileCollectionBase multipartFiles, News news)
        {
            Mapper.Instance().BeginTransaction();
            //type이 일반인 경우, 공지인 경우
            
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
    }
}