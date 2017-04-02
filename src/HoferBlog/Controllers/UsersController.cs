using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoferBlog.ViewModels;
using BlogStorage;
using BlogStorage.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoferBlog.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private BlogRepository repository;

        private IAuthorizationService _authorizationService;

        public UsersController(IAuthorizationService authorizationService)
        {
            this.repository = new BlogRepository();
            this._authorizationService = authorizationService;
        }

        [HttpGet("{username}")]
        public async Task<UserVM> Get(string username)
        {
            User found = repository.GetUser(username);

            if (found != null)
            {
                UserVM user = new UserVM(found) { CanModify = await _authorizationService.AuthorizeAsync(User, found, Operations.Update) };

                return user;
            }

            return null;
        }

        [HttpGet("{username}/blogs")]
        public IList<BlogVM> GetBlogs(string username)
        {
            List<Blog> found = repository.GetBlogsByUser(username);

            return found.Select(x => new BlogVM(x)).ToList();
        }

        [HttpGet("{username}/posts")]
        public IList<PostVM> GetPosts(string username)
        {
            List<Post> found = repository.GetPostsByUser(username);

            return found.Select(x => new PostVM(x)).ToList();
        }
    }
}
