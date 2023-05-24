using FoodieSite.BLL;
using FoodieSite.BOL;
using FoodieSite.ViewModels;
using FoodieSite.Web.Helpers;
using FoodieSite.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserBs objUserBs;
        public UsersController(IUserBs _objUserBs)
        {
            objUserBs = _objUserBs;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var loggedInUserDetail = HttpContext.Session.GetObject<LoggedInUser>("LoggedInUser"); //Session Object
            var imgObj = loggedInUserDetail.ImageURL == null ? await ObjectStorageHelper.GetObject("~/images/no_image_available.jpg")
                                                                                        : await ObjectStorageHelper.GetObject(loggedInUserDetail.ImageURL); // Get Img Obj

            EditProfileVM editProfileVM = new EditProfileVM()
            {
                Id = loggedInUserDetail.Id,
                UserName = loggedInUserDetail.UserName,
                Email = loggedInUserDetail.Email,
                Password = loggedInUserDetail.Password,
                ConfirmPassword = loggedInUserDetail.Password,
                ContactNo = loggedInUserDetail.ContactNo,
                Gender = loggedInUserDetail.Gender,
                GenderList = new[] { "Male", "Female", "Unspecified" },
                ImageMasterId = loggedInUserDetail.ImageMasterId,
                ImageURL = loggedInUserDetail.ImageURL,
                ImageStr = Convert.ToBase64String(imgObj.fileBytes)
            };

            return View(editProfileVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileVM model)
        {
            try
            {

                var obj = await objUserBs.Update(model);


                //Reset Session Object
                var loggedInUserDetail = HttpContext.Session.GetObject<LoggedInUser>("LoggedInUser");
                loggedInUserDetail.UserName = obj.UserName;
                loggedInUserDetail.Email = obj.Email;
                loggedInUserDetail.Password = obj.Password;
                loggedInUserDetail.ContactNo = obj.ContactNo;
                loggedInUserDetail.ImageMasterId = (long) obj.ImageMasterId;
                loggedInUserDetail.ImageURL = model.ImageURL;
                loggedInUserDetail.Gender = obj.Gender;

                HttpContext.Session.SetObject("LoggedInUser", loggedInUserDetail); //Pass data to session object

                return Json(new { status = true, message = "Record saved successfully."});

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { status = false, message = msg });
            }

           // return View();
        }
    }
}
