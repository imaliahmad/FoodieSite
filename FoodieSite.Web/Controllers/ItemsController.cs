using FoodieSite.BLL;
using FoodieSite.ViewModels;
using FoodieSite.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    public class ItemsController : Controller
    {
        private IItemMasterBs objItemMasterBs;
        private ICategoryMasterBs objCategoryBs;
        public ItemsController(IItemMasterBs _objItemMasterBs, ICategoryMasterBs _objCategoryBs)
        {
            objItemMasterBs = _objItemMasterBs;
            objCategoryBs = _objCategoryBs;
        }
        public IActionResult Index()
        {
            ViewBag.CategoryList = new SelectList(objCategoryBs.GetAll(), "CategoryMasterId","Name");
            return View();
        }
        [HttpGet]
        public IActionResult GetItemsList(int statusId)
        {
            try
            {
                var list = objItemMasterBs.GetAll(statusId);
                string listTitle = "";
                if (statusId == 0)
                    listTitle = "All Items";
                else if (statusId == 1)
                    listTitle = "Approved Items";
                else if (statusId == 2)
                    listTitle = "Rejected Items";
                else if (statusId == 3)
                    listTitle = "Pending Items";
                else
                    listTitle = "Items";

                return Json(new { status = true, title= listTitle,  data = list });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg });
            }
        }

        [HttpPost]
        public IActionResult SaveItem(ItemMasterVM modelVM)
        {
            try
            {
                if (modelVM.ItemMasterId > 0) //Edit
                {
                    var objVM = objItemMasterBs.Update(modelVM);
                    if (objVM != null)
                    {
                        return Json(new { status = true, message = "Record saved successfully.", data = objVM });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Could not get data after update." });
                    }
                }
                else //Insert
                {
                    var objVM = objItemMasterBs.Insert(modelVM);
                    if (objVM != null)
                    {
                        return Json(new { status = true, message = "Record saved successfully.", data = objVM });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Could not get data after insertion." });
                    }
                }
                
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg });
            }
        }
    }
}
