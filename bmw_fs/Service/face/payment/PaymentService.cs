using bmw_fs.Models.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.payment
{
    interface PaymentService
    {
        void insertPayment(HttpFileCollectionBase multipartFiles, Payment payment);

        IList<Payment> findAll(Payment payment);

        int findAllCount(Payment payment);

        Payment findPayment(Payment payment);

        void updatePayment(HttpFileCollectionBase multipartFiles, Payment payment);

        void deletePayment(Payment payment);
    }
}
