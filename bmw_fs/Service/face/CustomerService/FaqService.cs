using bmw_fs.Models.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.CustomerService
{
    interface FaqService
    {
        IList<Faq> findAll(Faq faq);

        int findAllCount(Faq faq);

        void insertFaq(Faq faq);

        Faq findFaq(Faq faq);

        void updateFaq(Faq faq);

        void deleteFaq(Faq faq);
    }
}
