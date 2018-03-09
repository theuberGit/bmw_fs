using bmw_fs.Common;
using bmw_fs.Dao.face.payment;
using bmw_fs.Models.payment;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.payment;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.payment
{
    public class CarImageServiceImpl : CarImageService
    {
        private CarImageDao carImageDao = new CarImageDao();
        private SequenceService sequenceService = new SequenceServiceImpl();
        private FilesService filesService = new FilesServiceImpl();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MvcApplication));

        public IList<Payment> findAll(Payment payment)
        {
            return carImageDao.findAll(payment);
        }

        public Payment findCarImage(Payment payment)
        {
            return carImageDao.findCarImage(payment);
        }

        public void insertCarImage(HttpFileCollectionBase multipartFiles, Payment payment)
        {            
            validation(payment);
            int masterIdx = sequenceService.getSequenceMasterIdx();
            payment.idx = masterIdx;
            try
            {
                Mapper.Instance().BeginTransaction();
                carImageDao.insertCarImage(payment);
                filesService.fileUpload(multipartFiles, "carImg", "png", 5 * 1024 * 1024, masterIdx, null);
                
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                Mapper.Instance().RollBackTransaction();
            }
        }
                
        public void updateCarImage(HttpFileCollectionBase multipartFiles, Payment payment)
        {            
            validation(payment);
            try
            {
                Mapper.Instance().BeginTransaction();
                carImageDao.updateCarImage(payment);
                filesService.deleteFileAndFileUpload(multipartFiles, "carImg", "png", 5 * 1024 * 1024, payment.idx, payment.fileIdxs);                
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void deleteCarImage(Payment payment)
        {            
            try
            {
                Mapper.Instance().BeginTransaction();
                carImageDao.deleteCarImage(payment);
                filesService.deleteRealFilesAndDataByFileMasterIdx(payment.idx);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                Mapper.Instance().RollBackTransaction();
            }
        }


        private void validation(Payment payment)
        {
            if (String.IsNullOrWhiteSpace(payment.brand)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(payment.series)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(payment.model)) throw new CustomException("필수 값이 없습니다.");
        }
    }
}