using bmw_fs.Models.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.legalNotice
{
    interface BusinessService
    {
        void insertBusiness(HttpFileCollectionBase multipartFiles, Business business);

        IList<Business> findAll(Business business);

        int findAllCount(Business business);

        Business findBusiness(Business business);

        void updateBusiness(HttpFileCollectionBase multipartFiles, Business business);

        void deleteBusiness(Business business);
    }
}
