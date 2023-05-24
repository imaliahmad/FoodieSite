using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodieSite.Web.Controllers
{
    public class MenuListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
