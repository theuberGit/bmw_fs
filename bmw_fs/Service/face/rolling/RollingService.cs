using bmw_fs.Models.rollling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.rolling
{
    interface RollingService
    {
        void insertRolling(HttpFileCollectionBase multipartFiles, Rolling rolling);

        IList<Rolling> findAll(Rolling rolling);

        int findAllCount(Rolling rolling);

        Rolling findRolling(Rolling rolling);

        void updateRollingt(HttpFileCollectionBase multipartFiles, Rolling rolling);

        void deleteRolling(Rolling rolling);
    }
}
