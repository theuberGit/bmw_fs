using bmw_fs.Models.payment;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.payment
{
    public class CarImageDao
    {
        public void insertCarImage(Payment payment)
        {
            Mapper.Instance().Insert("carImage.insertCarImage", payment);
        }

        public IList<Payment> findAll(Payment payment)
        {
            return Mapper.Instance().QueryForList<Payment>("carImage.findAll", payment);
        }        

        public Payment findCarImage(Payment payment)
        {
            return Mapper.Instance().QueryForObject<Payment>("carImage.findCarImage", payment);
        }

        public void updateCarImage(Payment payment)
        {
            Mapper.Instance().Update("carImage.updateCarImage", payment);
        }

        public void deleteCarImage(Payment payment)
        {
            Mapper.Instance().Delete("carImage.deleteCarImage", payment);
        }
    }
}