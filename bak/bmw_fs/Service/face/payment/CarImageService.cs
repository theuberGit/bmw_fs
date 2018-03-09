using bmw_fs.Models.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.payment
{
    interface CarImageService
    {
        IList<Payment> findAll(Payment payment);

        Payment findCarImage(Payment payment);

        void insertCarImage(HttpFileCollectionBase multipartFiles, Payment payment);
        
        void updateCarImage(HttpFileCollectionBase multipartFiles, Payment payment);

        void deleteCarImage(Payment payment);
        
    }
}
