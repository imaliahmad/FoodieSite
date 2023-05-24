using FoodieSite.BOL;
using FoodieSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FoodieSite.Web.Helpers;
using FoodieSite.Web.Models;
using FoodieSite.BLL;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Collections.Generic;

namespace FoodieSite.Web.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        UserManager<AppUsers> userManager;
        SignInManager<AppUsers> signInManager;
        IImageMasterBs imgMasterBs;
        public SecurityController(UserManager<AppUsers> _userManager, SignInManager<AppUsers> _signInManager, IImageMasterBs _imgMasterBs)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            imgMasterBs = _imgMasterBs;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUsers user = new AppUsers()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password
                    };

                    bool flag = false;

                    var resultCreate = await userManager.CreateAsync(user, model.Password);
                    if (resultCreate.Succeeded)
                    {
                        var resultRoleAssign = await userManager.AddToRoleAsync(user, "Customer");
                        if (resultRoleAssign.Succeeded)
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        var resultSignIn = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                        if (resultSignIn.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }


                return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var resultSignIn = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                    if (resultSignIn.Succeeded)
                    {
                        LoggedInUser loggedInUser = new LoggedInUser();

                        
                        var objUser = await userManager.FindByNameAsync(model.UserName); //Get User Detail
                        if (objUser.ImageMasterId != null)
                        {
                            var imgObj = imgMasterBs.GetById((long)objUser.ImageMasterId); //Get Image Path
                            loggedInUser.ImageMasterId = (long) objUser.ImageMasterId;
                            loggedInUser.ImageURL = imgObj == null ? String.Empty : imgObj.FilePath;
                        }
                        else
                        {
                            loggedInUser.ImageURL = string.Empty;
                        }

                        

                        var userRoles = await userManager.GetRolesAsync(objUser) as List<string>;
                        loggedInUser.Roles = userRoles;

                        loggedInUser.Id = objUser.Id;
                        loggedInUser.UserName = objUser.UserName;
                        loggedInUser.Email = objUser.Email;
                        loggedInUser.Password = objUser.Password ?? "";
                        loggedInUser.PasswordHash = objUser.PasswordHash ?? "";
                        loggedInUser.ContactNo = objUser.ContactNo ?? "";
                        loggedInUser.Gender = objUser.Gender ?? "";

                        HttpContext.Session.SetObject("LoggedInUser", loggedInUser); //Pass data to session object
                        
                        //var logged = HttpContext.Session.GetObject<LoggedInUser>("LoggedInUser");

                        
                        return RedirectToAction("Index", "Home");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                HttpContext.Session.SetObject("LoggedInUser", null);
                return RedirectToAction("Login", "Security");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
