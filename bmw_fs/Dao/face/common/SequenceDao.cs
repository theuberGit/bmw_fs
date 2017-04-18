using bmw_fs.Models.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.common
{
    public class SequenceDao
    {
        public int getSequence(String id)
        {
            Sequence seq = new Sequence();
            seq.id = id;
            Mapper.Instance().QueryForObject<int>("seq.getSequence", seq);
            return seq.seq;
        }
    }
}