using bmw_fs.Models.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.legalNotice
{
    interface GeneralService
    {
        IList<General> findAll(General general);

        int findAllCount(General general);

        General findGeneral(General general);

        void insertGeneral(General general);

        void updateGeneral(General general);

        void deleteGeneral(General general);
    }
}
