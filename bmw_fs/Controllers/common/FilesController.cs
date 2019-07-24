﻿using bmw_fs.Models.common;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace bmw_fs.Controllers.common
{
    [Authorize]
    public class FilesController : Controller
    {
        private string FILE_PATH = WebConfigurationManager.AppSettings["fileUploadPath"];
        FilesService filesService = new FilesServiceImpl();

        public FileResult fileDownload(Files files)
        {
            Files item = filesService.findAllByIdx(files);
            if(item != null)
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(FILE_PATH + "/" + item.savedFilename);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, item.originalFilename);
            }
            else
            {
                return null;
            }
            
        }

        public FileResult fileDownloadByMIdx(Files files)
        {
            IList<Files> items = filesService.findAllByMasterIdx(files);
            if (items != null)
            {
                Files item = items[0]; 
                byte[] fileBytes = System.IO.File.ReadAllBytes(FILE_PATH + "/" + item.savedFilename);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, item.originalFilename);
            }
            else
            {
                return null;
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult imageView(Files files)
        {
            Files item = filesService.findAllByIdx(files);
            if (item != null)
            {
                var path = FILE_PATH + "/" + item.savedFilename;
                return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
            }
            else
            {
                return null;
            }
        }        

        public ActionResult photoUpload(String callback_func)
        {
            HttpFileCollectionBase multipartRequest = Request.Files;
            String callback = "/resources/se/sample/photo_uploader/callback.html";
            Files files = filesService.filePhotoUpload(multipartRequest);
            
            String file_result = "&bNewLine=true&sFileName=" + files.originalFilename + "&sFileURL=/Files/imageView?fileIdx=" + files.fileIdx;
            return Redirect(callback + "?callback_func="+callback_func+file_result);
        }

        [HttpPost]
        public JsonResult fileDelete(String key)
        {
            Boolean isSuccess = filesService.deleteRealFilesAndDataByFileIdx(int.Parse(key));
            return Json(isSuccess);
        }

    }
}