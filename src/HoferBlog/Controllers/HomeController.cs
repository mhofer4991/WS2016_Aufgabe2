using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogStorage;
using BlogStorage.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoferBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogRepository repository;

        public HomeController()
        {
            this.repository = new BlogRepository();
        }
            
        public IActionResult Index()
        {
            ViewBag.Title = "Blog";

            return View();
        }
    }
}
