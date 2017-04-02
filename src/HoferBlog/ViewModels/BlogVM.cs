using BlogStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class BlogVM
    {
        public BlogVM()
        {
            this.Posts = new List<PostVM>();
        }

        public BlogVM(Blog blog) : this()
        {
            this.ID = blog.ID;
            this.Name = blog.Name;

            if (blog.User != null)
            {
                this.UserName = blog.User.UserName;
            }

            if (blog.Posts != null)
            {
                this.Posts = blog.Posts.Select(x => new PostVM(x)).ToList();
                this.PostsCount = this.Posts.Count;

                if (this.PostsCount > 0)
                {
                    this.LastEdited = blog.Posts.OrderBy(x => x.Time).First().Time;
                }
            }
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public DateTime LastEdited { get; set; }

        public List<PostVM> Posts { get; set; }

        public int PostsCount { get; set; }

        public bool CanModify { get; set; }
    }
}
