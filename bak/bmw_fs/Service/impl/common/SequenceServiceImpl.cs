using bmw_fs.Dao.face.common;
using bmw_fs.Service.face.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Service.impl.common
{
    public class SequenceServiceImpl : SequenceService
    {
        SequenceDao sequenceDao = new SequenceDao();
        public int getSequence(String id)
        {
            return sequenceDao.getSequence(id);
        }

        public int getSequenceMasterIdx()
        {
            return sequenceDao.getSequence("master_idx");
        }
    }
}