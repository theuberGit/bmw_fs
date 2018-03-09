using bmw_fs.Service.face.payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.payment;
using bmw_fs.Dao.face.payment;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace bmw_fs.Service.impl.payment
{
    public class PaymentServiceImpl : PaymentService
    {
        PaymentDao paymentDao = new PaymentDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();

        public void deletePayment(Payment payment)
        {
            try {
                Mapper.Instance().BeginTransaction();
                this.paymentDao.deletePayment(payment);
                filesService.deleteRealFilesAndDataByFileMasterIdx(payment.idx);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public IList<Payment> findAll(Payment payment)
        {
            return paymentDao.findAll(payment);
        }

        public int findAllCount(Payment payment)
        {
            return paymentDao.findAllCount(payment);
        }

        public Payment findPayment(Payment payment)
        {
            return paymentDao.findPayment(payment);
        }

        public void insertPayment(HttpFileCollectionBase multipartFiles, Payment payment)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            payment.idx = masterIdx;
            String programs = "";
            if(payment.programs.Count!= 0)
            {
                for (int i= 0; i < payment.programs.Count; i++)
                {
                    if (i == 0)
                    {
                        programs = payment.programs[i];
                    }
                    else

                    {
                        programs = programs + "," + payment.programs[i];
                    }
                }
                payment.program = programs;
            }
            validation(payment);
            try {                 
                Mapper.Instance().BeginTransaction();                
                paymentDao.insertPayment(payment);
                filesService.fileUpload(multipartFiles, "carImg", "jpg|png|gif", 10 * 1024 * 1024, masterIdx, null);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void updatePayment(HttpFileCollectionBase multipartFiles, Payment payment)
        {
            String programs = "";
            if (payment.programs.Count != 0)
            {
                for (int i = 0; i < payment.programs.Count; i++)
                {
                    if (i == 0)
                    {
                        programs = payment.programs[i];
                    }
                    else
                    {
                        programs = programs + "," + payment.programs[i];
                    }
                }
                payment.program = programs;
            }
            validation(payment);
            try {
                Mapper.Instance().BeginTransaction();
                filesService.deleteFileAndFileUpload(multipartFiles, "carImg", "jpg|png|gif", 10 * 1024 * 1024, payment.idx, payment.carIdxs);                
                this.paymentDao.updatePayment(payment);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }
            private void validation(Payment payment)
        {
            if (String.IsNullOrWhiteSpace(payment.brand)) throw new CustomException("필수 값이 없습니다.(브랜드)");
            if (String.IsNullOrWhiteSpace(payment.series)) throw new CustomException("필수 값이 없습니다.(시리즈)");
            if (String.IsNullOrWhiteSpace(payment.modelName)) throw new CustomException("필수 값이 없습니다.(모델명)");
            if (String.IsNullOrWhiteSpace(payment.price)) throw new CustomException("필수 값이 없습니다.(가격)");
            if (String.IsNullOrWhiteSpace(payment.deployYn)) throw new CustomException("필수 값이 없습니다.(배포)");
            if (String.IsNullOrWhiteSpace(payment.program)) throw new CustomException("필수 값이 없습니다.(프로그램)");

        }
    }
}