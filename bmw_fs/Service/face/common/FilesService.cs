using bmw_fs.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bmw_fs.Service.face.common
{
    interface FilesService
    {
        Files findAllByIdx(Files files);

        int insertFile(Files files);

        void updateFile(Files files);

        void deleteFileByFileIdx(Files files);

        IList<Files> findAllByMasterIdx(Files files);

        IList<Files> findAllByMasterIdxAndType(Files files);

        IList<Files> findAllByMasterIdxAndType(int masterIdx, String type);

        IList<Files> findAllByMasterIdxAndTypeForUpload(int masterIdx, String type);

        Files fileUpload(HttpFileCollectionBase multipartFiles, String inputFileName, String allowType, long fileSize, int masterIdx, IList<int> fileIdxs);

        void deleteFileAndFileUpload(HttpFileCollectionBase multipartFiles, String inputFileName, String allowType, long fileSize, int masterIdx, IList<int> fileIdxs);

        Boolean deleteRealFilesAndDataByFileMasterIdx(int masterIdx);
    }
}
