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
        IList<Payment> findAll(Payment payment);

        int findAllCount(Payment payment);

        /*
        void insertPayment(HttpFileCollectionBase multipartFiles, Payment payment);

        Payment findPayment(Payment payment);

        void updatePayment(HttpFileCollectionBase multipartFiles, Payment payment);

        void deletePayment(Payment payment);
        */

        void loadExcelByFileAndUpdateToDB(HttpFileCollectionBase multipartFiles, string regId);      

    }
}
