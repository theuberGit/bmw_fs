using bmw_fs.Models.common;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmw_fs.Dao.face.common
{
    public class FilesDao
    {
        public Files findAllByIdx(Files files)
        {
            return Mapper.Instance().QueryForObject<Files>("findAllByIdx", files);
        }

        public int insertFile(Files files)
        {
            Mapper.Instance().Insert("insertFile", files);
            return files.fileIdx;
        }

        public void updateFile(Files files)
        {
            Mapper.Instance().Update("updateFile", files);
        }

        public void deleteFileByFileIdx(Files files)
        {
            Mapper.Instance().Delete("deleteFileByFileIdx", files);
        }

        public IList<Files> findAllByMasterIdx(Files files)
        {
            return Mapper.Instance().QueryForList<Files>("findAllByMasterIdx", files);
        }

        public IList<Files> findAllByMasterIdxAndType(Files files)
        {
            return Mapper.Instance().QueryForList<Files>("findAllByMasterIdxAndType", files);
        }

    }
}