using bmw_fs.Models.news;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.news
{
    interface NewsService
    {
        void insertNews(HttpFileCollectionBase multipartFiles, News news);

        IList<News> findAll(News news);

        int findAllCount(News news);

        News findNews(News news);

        void updateNews(HttpFileCollectionBase multipartFiles, News news);

        void deleteNews(News news);
    }
}
