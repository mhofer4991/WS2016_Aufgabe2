using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogStorage.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BlogStorage;
using HoferBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoferBlog.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private BlogRepository repository;

        private UserManager<User> _userManager;

        private IHttpContextAccessor _hto;

        private IAuthorizationService _authorizationService;

        public BlogsController(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            this.repository = new BlogRepository();
            _userManager = userManager;
            _hto = httpContextAccessor;
            this._authorizationService = authorizationService;
        }
        
        [HttpGet("{id}")]
        public async Task<BlogVM> Get(int id)
        {
            Blog found = repository.GetBlog(id);

            if (found != null)
            {
                return new BlogVM(found)
                {
                    CanModify = await _authorizationService.AuthorizeAsync(User, found, Operations.Update)
                };
            }

            return null;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BlogVM newBlog)
        {
            Blog created = new Blog() { Name = newBlog.Name };

            if (await _authorizationService.AuthorizeAsync(User, created, Operations.Create))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                created.UserID = user.Id;

                Blog added = repository.AddBlog(created);

                return StatusCode(200, new BlogVM(added));
            }

            return StatusCode(500, new ResponseVM() { Text = "Blog could not be created!" });
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BlogVM value)
        {
            Blog blog = repository.GetBlog(id);
            blog.Name = value.Name;

            if (await _authorizationService.AuthorizeAsync(User, blog, Operations.Update))
            {
                Blog changed = repository.UpdateBlog(blog);

                return StatusCode(200, new BlogVM(changed));
            }

            return StatusCode(500, new ResponseVM() { Text = "Blog could not be updated!" });
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = repository.GetBlog(id);

            if (await _authorizationService.AuthorizeAsync(User, blog, Operations.Delete))
            {
                Blog deleted = repository.DeleteBlog(id);

                return StatusCode(200, new BlogVM(deleted));
            }

            return StatusCode(500, new ResponseVM() { Text = "Blog could not be deleted!" });
        }
        
        // posts per blog
        
        [HttpGet("{id}/posts")]
        public IEnumerable<PostVM> GetPosts(int id)
        {
            if (id > 0)
            {
                List<Post> posts = repository.GetPostsByBlog(id);

                if (posts != null)
                {
                    return posts.Select(x => new PostVM(x) { }).ToList();
                }
            }

            return new List<PostVM>();
        }
    }
}
