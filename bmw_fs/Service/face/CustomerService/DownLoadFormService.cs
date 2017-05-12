using bmw_fs.Models.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.CustomerService
{
    interface DownLoadFormService
    {
        IList<DownLoadForm> findAll(DownLoadForm downloadForm);

        int findAllCount(DownLoadForm downloadForm);

        void insertDownLoadForm(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm);

        DownLoadForm findDownLoadForm(DownLoadForm downloadForm);

        void updateDownLoadForm(HttpFileCollectionBase multipartFiles, DownLoadForm downloadForm);

        void deleteDownLoadForm(DownLoadForm downloadForm);
    }
}
