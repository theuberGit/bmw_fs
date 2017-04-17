using bmw_fs.Config;
using bmw_fs.Models.common;
using bmw_fs.Service.face.common;
using bmw_fs.Service.impl.common;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace bmw_fs.Controllers
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

        public ActionResult imageView(Files files)
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
       
    }
}