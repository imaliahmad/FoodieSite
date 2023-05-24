using FoodieSite.BLL;
using FoodieSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    public class CommonController : Controller
    {
        private static List<string> ImageExtensions = new List<string>() { ".png", ".jpg", ".jpeg" };
        private IImageMasterBs objImageMasterBs;
        public CommonController(IImageMasterBs _objImageMasterBs)
        {
            objImageMasterBs = _objImageMasterBs;
        }
        public async Task<IActionResult> UploadSingleFile()
        {
            try
            {
                var file = Request.Form.Files[0];

                string filePath = "";
                string fullPath = "";
                ObjectVM returnObj = new ObjectVM();

                string filename = file.FileName;
                long fileSize = file.Length;

                string extension = Path.GetExtension(filename);

                bool isImageValid = false;
                bool isPdfValid = false;
                if (ImageExtensions.Contains(extension))
                {
                    isImageValid = true;
                }

                filePath = "FoodieSite/" + (isImageValid ? "Images/" : "Files/") + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-ff") + "_" + filename;

                string basePath = "C://ObjectStorage/";
                fullPath = Path.Combine(basePath, filePath);

                //local drive
                bool isLocallySaved = await ObjectStorageHelper.PutObject(filePath, file);
                if (isLocallySaved)
                {
                    //Saved in Database
                    ImageMasterVM objImageVM = new ImageMasterVM() { FileName = filename, FilePath = filePath, FileSize = fileSize.ToString(), FileExtension = extension };
                    objImageVM = objImageMasterBs.Insert(objImageVM);

                    var obj = await ObjectStorageHelper.GetObject(filePath);
                    if (obj != null && obj.fileBytes != null)
                    {
                        returnObj.ImageId = objImageVM.ImageMasterId;
                        returnObj.FilePathURL = filePath;
                        returnObj.FilePathStr = Convert.ToBase64String(obj.fileBytes);
                    }
                    // Image objUser = new UserImages() { FileName = filename, FilePath = filePath, FileSize = "1000", FileExtension = extension };
                }

                return Json(new { status = true, data = returnObj });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, messag = msg });
            }
        }
    }
}
