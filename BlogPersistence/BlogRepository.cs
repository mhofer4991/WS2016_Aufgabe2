using BlogPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPersistence
{
    class BlogRepository
    {
        private BlogContext context;

        public BlogRepository(BlogContext context)
        {
            this.context = context;
        }

        public IList<Post> GetAllPosts()
        {
            var posts = from post in context.Posts
                        select post;

            return posts.ToList();
        }
    }
}
