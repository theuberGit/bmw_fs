﻿using bmw_fs.Config;
using bmw_fs.Dao.face.common;
using bmw_fs.Models.common;
using bmw_fs.Service.face.common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace bmw_fs.Service.impl.common
{
    public class FilesServiceImpl : FilesService
    {
        FilesDao filesDao = new FilesDao();

        public Files findAllByIdx(Files files)
        {
            return filesDao.findAllByIdx(files);
        }

        public int insertFile(Files files)
        {
            return filesDao.insertFile(files);
        }

        public void updateFile(Files files)
        {
            filesDao.updateFile(files);
        }

        public void deleteFileByFileIdx(Files files)
        {
            filesDao.deleteFileByFileIdx(files);
        }

        public IList<Files> findAllByMasterIdx(Files files)
        {
            return filesDao.findAllByMasterIdx(files);
        }

        public IList<Files> findAllByMasterIdxAndType(Files files)
        {
            return filesDao.findAllByMasterIdxAndType(files);
        }

        public IList<Files> findAllByMasterIdxAndType(int masterIdx, String type)
        {
            Files files = new Files();
            files.masterIdx = masterIdx;
            files.type = type;
            return filesDao.findAllByMasterIdxAndType(files);
        }

        public IList<Files> findAllByMasterIdxAndTypeForUpload(int masterIdx, String type)
        {
            Files files = new Files();
            files.masterIdx = masterIdx;
            files.type = type;
            IList<Files> list = filesDao.findAllByMasterIdxAndType(files);
            if(list.Count == 0)
            {
                list.Add(new Files());
            }
            return list;
        }

        public Files fileUpload(HttpFileCollectionBase multipartFiles, String inputFileName, String allowType, long fileSize, int masterIdx, IList<int> fileIdxs)
        {
            IList<HttpPostedFileBase> files = multipartFiles.GetMultiple(inputFileName);
            Files fileItem = new Files();
            int idx = 0;
            foreach (var file in files)
            {
                if(file.ContentLength != 0)
                {
                    String fileName = file.FileName;
                    int typeIndex = fileName.LastIndexOf(".")+1;
                    String fileType = fileName.Substring(typeIndex, fileName.Length - typeIndex);
                    validationFileUpload(file, allowType, fileSize, fileType); //file type 및 size 체크
                    String replaceName = replaceFileName();//파일 이름 변환
                   
                    String path = Path.Combine(StringProperties.FILE_PATH, replaceName);
                    //폴더가 없으면 생성
                    if (!Directory.Exists(StringProperties.FILE_PATH))
                    {
                        Directory.CreateDirectory(StringProperties.FILE_PATH);
                    }
                    file.SaveAs(path);
                    fileItem.originalFilename = fileName;
                    fileItem.savedFilename = replaceName;
                    fileItem.masterIdx = masterIdx;
                    fileItem.type = inputFileName;
                    
                    if (fileIdxs == null || fileIdxs.Count == 0 || fileIdxs.ElementAt(idx) == 0)
                    {//새로 올리는 file인 경우, db에 file 정보 insert
                        filesDao.insertFile(fileItem);
                    }
                    else
                    {//기존 file을 수정하는 경우, 해당 fileIdx로 update
                        fileItem.fileIdx = fileIdxs.ElementAt(idx);
                        filesDao.updateFile(fileItem);
                    }
                }
                idx++;
            }
            return fileItem;
        }

        public void deleteFileAndFileUpload(HttpFileCollectionBase multipartFiles, String inputFileName, String allowType, long fileSize, int masterIdx, IList<int> fileIdxs)
        {
            IList<HttpPostedFileBase> files = multipartFiles.GetMultiple(inputFileName);
            for(int i = 0, size = files.Count; i <size; i++)
            {
                if(files[i].ContentLength > 0 && fileIdxs.Count != 0 && fileIdxs.ElementAt(i) != 0) //파일이 교체됬을경우 : 파일있고 fileIdxs도 있는경우
                {
                    deleteRealFilesByFileIdx(fileIdxs.ElementAt(i));
                }
            }
            fileUpload(multipartFiles, inputFileName, allowType, fileSize, masterIdx, fileIdxs);
        }

        public Boolean deleteRealFilesAndDataByFileMasterIdx(int masterIdx)
        {
            Files files = new Files();
            files.masterIdx = masterIdx;
            IList<Files> fileList = filesDao.findAllByMasterIdx(files);
            if(fileList.Count == 0)
            {
                return false;
            }
            else
            {
                foreach(var item in fileList){
                    files.fileIdx = item.fileIdx;
                    filesDao.deleteFileByFileIdx(files);
                    deleteRealFile(files);
                }
                return true;
            }
            
        }

        public Files filePhotoUpload(HttpFileCollectionBase multipartFiles)
        {
            return fileUpload(multipartFiles, "Filedata", "jpg|png|gif|bmp", 10 * 1024 * 1024, -1, null);
        }

        private void validationFileUpload(HttpPostedFileBase file, String allowType, long fileSize, String fileType)
        {
            Debug.WriteLine(allowType.IndexOf(fileType.ToLower()));
            if (allowType == null || allowType.IndexOf(fileType.ToLower()) == -1)
            {
                throw new Exception("File Type Error");
            }
            if (fileSize != 0 && file.ContentLength > fileSize)
            {
                throw new Exception("File Size Error");
            }
        }

        private String replaceFileName()
        {
            Random random = new Random();
            String copyFileName = ""+ random.Next(Int32.MaxValue) + random.Next(Int32.MaxValue) + random.Next(Int32.MaxValue);
            String replaceName = copyFileName + ".tmp";
            return replaceName;
        }

        private Boolean deleteRealFile(Files files)
        {
            String path = Path.Combine(StringProperties.FILE_PATH, files.savedFilename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool deleteRealFilesByFileIdx(int idx)
        {
            Files files = new Files();
            files.fileIdx = idx;
            Files item = filesDao.findAllByIdx(files);
            if(item == null)
            {
                return false;
            }
            else
            {
                return deleteRealFile(item);
            }
            
        }

    }
}