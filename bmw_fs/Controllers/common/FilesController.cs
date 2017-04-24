using bmw_fs.Config;
using bmw_fs.Models.common;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace bmw_fs.Controllers.common
{
    public class FilesController : Controller
    {
        FilesService filesService = new FilesServiceImpl();

        public FileResult fileDownload(Files files)
        {
            Files item = filesService.findAllByIdx(files);
            if(item != null)
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(StringProperties.FILE_PATH + "/" + item.savedFilename);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, item.originalFilename);
            }
            else
            {
                return null;
            }
            
        }

        public FileResult imageView(Files files)
        {
            Files item = filesService.findAllByIdx(files);
            if (item != null)
            {
                return File(StringProperties.FILE_PATH + "/" + item.savedFilename, "image/jpg");
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

    }
}