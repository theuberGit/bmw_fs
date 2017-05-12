using bmw_fs.Models.CustomerService;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.CustomerService
{
    public class DownLoadFormDao
    {
        public IList<DownLoadForm> findAll(DownLoadForm downloadForm)
        {
            return Mapper.Instance().QueryForList<DownLoadForm>("downloadForm.findAll", downloadForm);
        }

        public int findAllCount(DownLoadForm downloadForm)
        {
            return Mapper.Instance().QueryForObject<int>("downloadForm.findAllCount", downloadForm);
        }

        public void insertDownloadForm(DownLoadForm downloadForm)
        {
            Mapper.Instance().Insert("downloadForm.insertDownloadForm", downloadForm);
        }

        public DownLoadForm findDownloadForm(DownLoadForm downloadForm)
        {
            return Mapper.Instance().QueryForObject<DownLoadForm>("downloadForm.findDownloadForm", downloadForm);
        }

        public void updateDownloadForm(DownLoadForm downloadForm)
        {
            Mapper.Instance().Update("downloadForm.updateDownloadForm", downloadForm);
        }

        public void deleteDownloadForm(DownLoadForm downloadForm)
        {
            Mapper.Instance().Delete("downloadForm.deleteDownloadForm", downloadForm);
        }

    }
}