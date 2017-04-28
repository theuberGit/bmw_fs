using bmw_fs.Models.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.legalNotice
{
    interface CreditService
    {
        void insertCredit(Credit credit);

        IList<Credit> findAll(Credit credit);

        int findAllCount(Credit credit);

        Credit findCredit(Credit credit);

        void updateCredit(Credit credit);

        void deleteCredit(Credit credit);
    }
}
