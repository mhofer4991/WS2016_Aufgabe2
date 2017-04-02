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
    public class TagController : Controller
    {
        private BlogRepository repository;

        public TagController()
        {
            this.repository = new BlogRepository();
        }
        
        [HttpGet("{tagName}")]
        public List<PostVM> Get(string tagName)
        {
            if (!string.IsNullOrWhiteSpace(tagName))
            {
                List<Post> posts = repository.GetPostsByTag(tagName);

                return posts.Select(x => new PostVM(x)).ToList();
            }

            return new List<PostVM>();
        }
    }
}
