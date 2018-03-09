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
using OfficeOpenXml;

namespace bmw_fs.Service.impl.payment
{
    public class PaymentServiceImpl : PaymentService
    {
        PaymentDao paymentDao = new PaymentDao();
        FilesService filesService = new FilesServiceImpl();
        SequenceService sequenceService = new SequenceServiceImpl();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MvcApplication));

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

        public void loadExcelByFileAndUpdateToDB(HttpFileCollectionBase multipartFiles, string regId)
        {            
            var file = multipartFiles["fileExcel"];
            List<Payment> list = this.loadExcelByFile(file);
            if (list.Count == 0) return;            
            try
            {
                Mapper.Instance().BeginTransaction();
                //Fileupload - update
                int masterIdx = 900000;
                filesService.deleteRealFilesAndDataByFileMasterIdx(masterIdx);
                filesService.fileUpload(multipartFiles, "fileExcel", "xlsx", 5 * 1024 * 1024, masterIdx, null);
                
                //Update Db Data
                paymentDao.deletePaymentAll();
                foreach(var payment in list)
                {
                    payment.regId = regId;
                    paymentDao.insertPayment(payment);
                }                
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                Mapper.Instance().RollBackTransaction();
            }            
        }

        private List<Payment> loadExcelByFile(HttpPostedFileBase file)
        {
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet[1];

                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;

                    List<Payment> list = new List<Payment>();
                    for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                    {
                        try
                        {
                            Payment item = new Payment();                            
                            item.idx = parseExcelColumnInt(workSheet, rowIterator, 1);
                            item.brand = parseExcelColumnStr(workSheet, rowIterator, 2);
                            item.series = parseExcelColumnStr(workSheet, rowIterator, 3);
                            item.model = parseExcelColumnStr(workSheet, rowIterator, 4);
                            item.modelName = parseExcelColumnStr(workSheet, rowIterator, 5);
                            item.price = parseExcelColumnMoney(workSheet, rowIterator, 6);
                            item.program = parseExcelColumnStr(workSheet, rowIterator, 8);
                            item.zhZanga = parseExcelColumnInt(workSheet, rowIterator, 9);
                            item.zhPay1 = parseExcelColumnMoney(workSheet, rowIterator, 10);
                            item.zhPay2 = parseExcelColumnMoney(workSheet, rowIterator, 11);
                            item.zhPay3 = parseExcelColumnMoney(workSheet, rowIterator, 12);
                            item.zlZanga = parseExcelColumnInt(workSheet, rowIterator, 13);
                            item.zlPay1 = parseExcelColumnMoney(workSheet, rowIterator, 14);
                            item.zlPay2 = parseExcelColumnMoney(workSheet, rowIterator, 15);
                            item.zlPay3 = parseExcelColumnMoney(workSheet, rowIterator, 16);
                            item.ghPay1 = parseExcelColumnMoney(workSheet, rowIterator, 17);
                            item.ghPay2 = parseExcelColumnMoney(workSheet, rowIterator, 18);
                            item.ghPay3 = parseExcelColumnMoney(workSheet, rowIterator, 19);
                            item.rtPay1 = parseExcelColumnMoney(workSheet, rowIterator, 20);
                            item.rtPay2 = parseExcelColumnMoney(workSheet, rowIterator, 21);
                            item.rtPay3 = parseExcelColumnMoney(workSheet, rowIterator, 22);
                            item.ppRate = parseExcelColumnInt(workSheet, rowIterator, 23);
                            item.ppPay1 = parseExcelColumnMoney(workSheet, rowIterator, 24);
                            item.ppPay2 = parseExcelColumnMoney(workSheet, rowIterator, 25);
                            item.ppPay3 = parseExcelColumnMoney(workSheet, rowIterator, 26);
                            item.deployYn = parseExcelColumnStr(workSheet, rowIterator, 7);
                            if(item.idx != 0)
                            {
                                list.Add(item);
                            }                            
                        }
                        catch (CustomFormatException cfe)
                        {
                            logger.Debug(cfe.Message);
                            Debug.WriteLine(cfe.Message);
                        }
                    }
                    return list;
                }
            }
            return null;
        }

        private string parseExcelColumnStr(ExcelWorksheet workSheet, int row, int col)
        {
            object item = workSheet.Cells[row, col].Value;
            if (item != null)
            {
                return item.ToString();
            }
            return "";
        }

        private string parseExcelColumnMoney(ExcelWorksheet workSheet, int row, int col)
        {
            object item = workSheet.Cells[row, col].Value;
            if (item != null)
            {
                try
                {
                    return Decimal.Parse(item.ToString()).ToString("#,###0");
                }
                catch (FormatException fe)
                {
                    throw new CustomFormatException("format exception " + row + ", " + col);
                }
            }
            return "";
        }

        private int parseExcelColumnInt(ExcelWorksheet workSheet, int row, int col)
        {
            object item = workSheet.Cells[row, col].Value;
            if (item != null)
            {
                try
                {
                    return int.Parse(item.ToString());
                }
                catch (FormatException fe)
                {
                    throw new CustomFormatException("format exception " + row + ", " + col);
                }
            }
            return 0;
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