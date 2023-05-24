using FoodieSite.BLL;
using FoodieSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantMasterBs restaurantMasterBs;
        public RestaurantController(IRestaurantMasterBs _restaurantMasterBs)
        {
            restaurantMasterBs = _restaurantMasterBs;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRestaurantList()
        {
            try
            {
                var list = restaurantMasterBs.GetAll();
                return Json(new { status = true, data = list });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg});
            }
        }
        [HttpPost]
        public IActionResult SaveRestuarant(RestaurantMasterVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.RestaurantMasterId > 0) //Edit
                    {
                        model = restaurantMasterBs.Update(model);
                    }
                    else //Insert
                    {
                        model = restaurantMasterBs.Insert(model);
                    }
                    return Json(new { status = true, message = "Record saved successfully." });
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
        public IActionResult Edit(long rid)
        {
            try
            {
                var obj = restaurantMasterBs.GetById(rid);
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
