using FoodieSite.BLL;
using FoodieSite.ViewModels;
using FoodieSite.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IItemMasterBs objItemMasterBs;
        private IImageMasterBs objImageMasterBs;
        public HomeController(IItemMasterBs _objItemMasterBs, IImageMasterBs _objImageMasterBs)
        {
            objItemMasterBs = _objItemMasterBs;
            objImageMasterBs = _objImageMasterBs;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetMenuItems()
        {
            var items = objItemMasterBs.GetAll((int)ItemStatusEnums.All).ToList();

            //HttpContext.Session.SetObject("MenuList", items);

            return Json(items);
        }
        public async Task<IActionResult> GetImageByItemId(int imageId)
        {

            //var obj1 = HttpContext.Session.GetObject<List<ItemMasterVM>>("MenuList");

            ObjectVM returnObj = new ObjectVM();
            var obj=  objImageMasterBs.GetById(imageId);
            if (obj != null)
            {
				var imgObj = await ObjectStorageHelper.GetObject(obj.FilePath);
				if (imgObj != null && imgObj.fileBytes != null)
				{
					returnObj.FilePathStr = Convert.ToBase64String(imgObj.fileBytes);
				}
			}

            

            return Json(returnObj);
        }
    }
}
