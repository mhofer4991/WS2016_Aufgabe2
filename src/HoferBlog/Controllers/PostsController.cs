using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogStorage.Models;
using BlogStorage;
using HoferBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HoferBlog.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private BlogRepository repository;

        private IAuthorizationService _authorizationService;

        public PostsController(IAuthorizationService authorizationService)
        {
            this.repository = new BlogRepository();
            this._authorizationService = authorizationService;
        }
        
        [HttpGet("{id}")]
        public async Task<PostVM> Get(int id)
        {
            Post post = repository.GetPost(id);
            //List<Tag> tags = repository.GetTagsByPostID(id);

            if (post != null)
            {
                PostVM vpost = new PostVM(post) { CanModify = await _authorizationService.AuthorizeAsync(User, post, Operations.Update) };

                return vpost;
            }

            return null;
        }

        [HttpGet("latest")]
        public IEnumerable<PostVM> GetLatest()
        {
            List<Post> latest = repository.GetLatestPosts(25);

            if (latest != null)
            {
                return latest.Select(x => new PostVM(x) { }).ToList();
            }

            return new List<PostVM>();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostVM newPost)
        {
            Post post = new Post() { BlogID = newPost.BlogID, Text = newPost.Text, Title = newPost.Title };
            post.Blog = repository.GetBlog(post.BlogID);
            post.Time = DateTime.Now;
            post.FriendlyUrl = GetFriendlyUrl(post);

            if (await _authorizationService.AuthorizeAsync(User, post, Operations.Create))
            {
                List<Tag> tags = newPost.Tags.Select(x => new Tag() { Name = x.Name }).ToList();

                Post added = repository.AddPostWithTags(post, tags);

                return StatusCode(200, new PostVM(added));
            }

            return StatusCode(500, new ResponseVM() { Text = "Post could not be created!" });
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]PostVM newPost)
        {
            Post post = repository.GetPost(id);
            post.Title = newPost.Title;
            post.Text = newPost.Text;
            post.Blog = repository.GetBlog(post.BlogID);
            post.Time = DateTime.Now;
            post.FriendlyUrl = GetFriendlyUrl(post);

            List<Tag> tags = newPost.Tags.Select(x => new Tag() { Name = x.Name }).ToList();

            if (await _authorizationService.AuthorizeAsync(User, post, Operations.Update))
            {
                Post changed = repository.UpdatePostWithTags(post, tags);

                return StatusCode(200, new PostVM(changed));
            }

            return StatusCode(500, new ResponseVM() { Text = "Post could not be updated!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Post post = repository.GetPost(id);
            post.Blog = repository.GetBlog(post.BlogID);

            if (await _authorizationService.AuthorizeAsync(User, post, Operations.Delete))
            {
                Post deleted = repository.DeletePost(id);

                return StatusCode(200, new PostVM(deleted));
            }

            return StatusCode(500, new ResponseVM() { Text = "Post could not be deleted!" });
        }

        private string GetFriendlyUrl(Post post)
        {
            return System.Net.WebUtility.UrlEncode(post.Title);
        }
    }
}
