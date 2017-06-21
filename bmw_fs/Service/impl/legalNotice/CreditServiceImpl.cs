using bmw_fs.Service.face.legalNotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bmw_fs.Models.legalNotice;
using bmw_fs.Dao.face.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using bmw_fs.Common;
using System.Text.RegularExpressions;

namespace bmw_fs.Service.impl.legalNotice
{
    public class CreditServiceImpl : CreditService
    {
        CreditDao creditDao = new CreditDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<Credit> findAll(Credit credit)
        {
            return creditDao.findAll(credit);
        }

        public int findAllCount(Credit credit)
        {
            return creditDao.findAllCount(credit);
        }

        public Credit findCredit(Credit credit)
        {
            return creditDao.findCredit(credit);
        }

        public void insertCredit(Credit credit)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            credit.idx = masterIdx;
            validation(credit);
            try { 
                Mapper.Instance().BeginTransaction();                
                creditDao.insertCredit(credit);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        public void updateCredit(Credit credit)
        {
            try { 
                Mapper.Instance().BeginTransaction();
                creditDao.updateCredit(credit);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }

        }
        
        public void deleteCredit(Credit credit)
        {
            try {
                Mapper.Instance().BeginTransaction();
                creditDao.deleteCredit(credit);
                Mapper.Instance().CommitTransaction();
            }
            catch (Exception e)
            {
                Mapper.Instance().RollBackTransaction();
            }
        }

        private void validation(Credit credit)
        {
            if (String.IsNullOrWhiteSpace(credit.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(Regex.Replace(credit.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
}