using bmw_fs.Models.news;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.news
{
    public class NewsDao
    {
        public void insertNews(News news)
        {
           Mapper.Instance().Insert("news.insertNews", news);
        }

        public IList<News> findAll(News news)
        {
            return Mapper.Instance().QueryForList<News>("news.findAll", news);
        }

        public int findAllCount(News news)
        {
            return Mapper.Instance().QueryForObject<int>("news.findAllCount", news);
        }

        public IList<News> findAllNotice(News news)
        {
            return Mapper.Instance().QueryForList<News>("news.findAllNotice", news);
        }

        public News findNews(News news)
        {
            return Mapper.Instance().QueryForObject<News>("news.findNews", news);
        }

        public void updateNews(News news)
        {
            Mapper.Instance().Update("news.updateNews", news);
        }

        public void deleteNews(News news)
        {
            Mapper.Instance().Delete("news.deleteNews", news);
        }
    }
}