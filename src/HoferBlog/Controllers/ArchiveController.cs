using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoferBlog.ViewModels;
using BlogStorage.Models;
using BlogStorage;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoferBlog.Controllers
{
    [Route("api/[controller]")]
    public class ArchiveController : Controller
    {
        private BlogRepository repository;

        public ArchiveController()
        {
            this.repository = new BlogRepository();
        }

        // GET api/values/5
        [HttpGet("{year}/{month}")]
        public List<PostVM> Get(string year, string month)
        {
            int y;
            int m;

            if (int.TryParse(year, out y))
            {
                if (int.TryParse(month, out m))
                {
                    List<Post> posts = repository.GetArchive(y, m);

                    return posts.Select(x => new PostVM(x)).ToList();
                }
            }

            return new List<PostVM>();
        }
    }
}
