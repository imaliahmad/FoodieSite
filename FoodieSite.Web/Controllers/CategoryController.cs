using FoodieSite.BLL;
using FoodieSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    
    public class CategoryController : Controller
    {
        private ICategoryMasterBs categoryMasterBs;
        public CategoryController(ICategoryMasterBs _categoryMasterBs)
        {
            categoryMasterBs = _categoryMasterBs;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCategorysList()
        {
            try
            {
                var list = categoryMasterBs.GetAll();
                return Json(new { status = true, data = list });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg });
            }
        }
        [HttpPost]
        public IActionResult SaveCategory(CategoryMasterVM model)
        {
            try
            {
                ModelState.Remove("CategoryMasterId");
                if (ModelState.IsValid)
                {
                    if (model.CategoryMasterId > 0) //Edit
                    {
                        model = categoryMasterBs.Update(model);
                    }
                    else //Insert
                    {
                        model = categoryMasterBs.Insert(model);
                    }
                    return Json(new { status = true, data = model, message = "Record saved successfully." });
                }
                else
                {
                    foreach (var item in ModelState.Values)
                    {
                        ModelState.AddModelError("", item.Errors[0].ErrorMessage.ToString());
                    }
                    return Json(ModelState);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg });
            }
        }

        [HttpGet]
        public IActionResult Edit(long cid)
        {
            try
            {
                var obj = categoryMasterBs.GetById(cid);
                return Json(new { status = true, data = obj });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg });
            }
        }
    }
}
