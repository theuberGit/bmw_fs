
using bmw_fs.Common;
using bmw_fs.Dao.face.legalNotice;
using bmw_fs.Models.legalNotice;
using bmw_fs.Service.face.common;
using bmw_fs.Service.face.legalNotice;
using bmw_fs.Service.impl.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace bmw_fs.Service.impl.legalNotice
{
    public class GeneralServiceImpl : GeneralService
    {
        GeneralDao generalDao = new GeneralDao();
        SequenceService sequenceService = new SequenceServiceImpl();

        public IList<General> findAll(General general)
        {
            return generalDao.findAll(general);
        }

        public int findAllCount(General general)
        {
            return generalDao.findAllCount(general);
        }

        public General findGeneral(General general)
        {
           return generalDao.findGeneral(general);
        }

        public void insertGeneral(General general)
        {
            int masterIdx = sequenceService.getSequenceMasterIdx();
            general.idx = masterIdx;
            Mapper.Instance().BeginTransaction();
            generalDao.insertGeneral(general);
            Mapper.Instance().CommitTransaction();
        }

        public void updateGeneral(General general)
        {
            Mapper.Instance().BeginTransaction();
            generalDao.updateGeneral(general);
            Mapper.Instance().CommitTransaction();
        }

        public void deleteGeneral(General general)
        {
            Mapper.Instance().BeginTransaction();
            generalDao.deleteGeneral(general);
            Mapper.Instance().CommitTransaction();
        }

        private void validation(General general)
        {
            if (String.IsNullOrWhiteSpace(general.title)) throw new CustomException("필수 값이 없습니다.");
            if (String.IsNullOrWhiteSpace(Regex.Replace(general.contents, "<.*?>", string.Empty))) throw new CustomException("필수 값이 없습니다.");
        }
    }
}