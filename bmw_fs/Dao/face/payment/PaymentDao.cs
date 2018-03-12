using bmw_fs.Models.payment;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.payment
{
    public class PaymentDao
    {
        public void insertPayment(Payment payment)
        {
            Mapper.Instance().Insert("payment.insertPayment", payment);
        }

        public IList<Payment> findAll(Payment payment)
        {
            return Mapper.Instance().QueryForList<Payment>("payment.findAll", payment);
        }

        public int findAllCount(Payment payment)
        {
            return Mapper.Instance().QueryForObject<int>("payment.findAllCount", payment);
        }

        public Payment findPayment(Payment payment)
        {
            return Mapper.Instance().QueryForObject<Payment>("payment.findPayment", payment);
        }

        public void updatePayment(Payment payment)
        {
            Mapper.Instance().Update("payment.updatePayment", payment);
        }

        public void deletePayment(Payment payment)
        {
            Mapper.Instance().Delete("payment.deletePayment", payment);
        }

        public void deletePaymentAll()
        {
            Mapper.Instance().Delete("payment.deletePaymentAll", null);
        }

        public IList<string> findModel(Payment payment)
        {
            return Mapper.Instance().QueryForList<string>("payment.findModel", payment);
        }

        public IList<string> findSeries(Payment payment)
        {
            return Mapper.Instance().QueryForList<string>("payment.findSeries", payment);
        }

    }
}