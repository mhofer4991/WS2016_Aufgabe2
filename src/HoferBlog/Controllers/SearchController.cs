using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoferBlog.ViewModels;
using BlogStorage;
using BlogStorage.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoferBlog.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private BlogRepository repository;

        public SearchController()
        {
            this.repository = new BlogRepository();
        }
        
        [HttpGet("{term}")]
        public IEnumerable<PostVM> Get(string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                List<Post> posts = repository.Search(term);

                return posts.Select(x => new PostVM(x)).ToList();
            }

            return new List<PostVM>();
        }
    }
}
