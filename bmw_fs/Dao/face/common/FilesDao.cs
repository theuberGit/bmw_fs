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
            return Mapper.Instance().QueryForObject<Files>("files.findAllByIdx", files);
        }

        public int insertFile(Files files)
        {
            Mapper.Instance().Insert("files.insertFile", files);
            return files.fileIdx;
        }

        public void updateFile(Files files)
        {
            Mapper.Instance().Update("files.updateFile", files);
        }

        public void deleteFileByFileIdx(Files files)
        {
            Mapper.Instance().Delete("files.deleteFileByFileIdx", files);
        }

        public IList<Files> findAllByMasterIdx(Files files)
        {
            return Mapper.Instance().QueryForList<Files>("files.findAllByMasterIdx", files);
        }

        public IList<Files> findAllByMasterIdxAndType(Files files)
        {
            return Mapper.Instance().QueryForList<Files>("files.findAllByMasterIdxAndType", files);
        }

    }
}