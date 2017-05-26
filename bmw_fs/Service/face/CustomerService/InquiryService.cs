using bmw_fs.Models.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.CustomerService
{
    interface InquiryService
    {
        IList<Inquiry> findAll(Inquiry inquiry);

        int findAllCount(Inquiry inquiry);

        Inquiry findInquiry(Inquiry inquiry);

        void updateInquiry(Inquiry inquiry);

        void updateInquirySendMail(Inquiry inquiry);

        void deleteInquiry(Inquiry inquiry);
    }
}
