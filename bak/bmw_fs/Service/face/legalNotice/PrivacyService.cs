using bmw_fs.Models.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.privacy
{
    interface PrivacyService
    {
        void insertPrivacy(Privacy privacy);

        IList<Privacy> findAll(Privacy privacy);

        int findAllCount(Privacy privacy);

        Privacy findPrivacy(Privacy privacy);

        void updatePrivacy(Privacy privacy);

        void deletePrivacy(Privacy privacy);
    }
}
