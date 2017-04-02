using BlogStorage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogStorage
{
    public class BlogRepository
    {
        private BlogContext context;

        public BlogRepository()
        {
            this.context = new BlogContext();
            this.context.Database.EnsureCreated();
        }

        // -----------
        // User
        // -----------

        public User GetUser(string username)
        {
            var query = context.Users
                .Where(x => x.UserName.Equals(username))
                .FirstOrDefault();

            return query;
        }

        public List<Blog> GetBlogsByUser(string username)
        {
            User user = GetUser(username);

            if (user == null)
            {
                return new List<Blog>();
            }

            var query = context.Blogs.Include("Posts")
                .Where(x => x.UserID.Equals(user.Id))
                .ToList();

            return query;
        }

        public List<Post> GetPostsByUser(string username)
        {
            User user = GetUser(username);

            if (user == null)
            {
                return new List<Post>();
            }

            var query = context.Posts.Include("Blog").Include("PostTags.Tag")
                .Where(x => x.Blog.UserID.Equals(user.Id))
                .ToList();

            return query;
        }

        // -----------
        // Blog
        // -----------

        public Blog AddBlog(Blog blog)
        {
            var added = context.Blogs.Add(blog);
            context.SaveChanges();

            return added.Entity;
        }

        public Blog GetBlog(int id)
        {
            var query = from b in context.Blogs.Include("Posts").Include("User")
                        where b.ID == id
                        select b;
            
            if (query.Count() == 1)
            {
                return query.First();
            }

            return null;
        }

        public List<Blog> GetAllBlogs()
        {
            var blogs = from blog in context.Blogs.Include("Posts")
                        select blog;

            return blogs.ToList();
        }

        public Blog UpdateBlog(Blog blog)
        {
            var changed = context.Update(blog);

            context.SaveChanges();

            return changed.Entity;
        }

        public Blog DeleteBlog(int id)
        {
            return this.DeleteBlog(GetBlog(id));
        }

        public Blog DeleteBlog(Blog blog)
        {
            if (blog == null)
            {
                return null;
            }

            List<Post> posts = GetPostsByBlog(blog.ID);

            foreach (Post post in posts)
            {
                this.DeletePost(post);
            }

            var deleted = this.context.Blogs.Remove(blog);

            this.context.SaveChanges();

            return deleted.Entity;
        }

        // -----------
        // Post
        // -----------

        public Post AddPostWithTags(Post post, List<Tag> tags)
        {
            tags = tags.GroupBy(x => x.Name).Select(y => y.First()).ToList();

            this.AddTags(tags);

            post.PostTags = tags.Select(x => new PostTag() { PostID = post.ID, TagName = x.Name }).ToList();

            var added = this.AddPost(post);

            //this.AddPostTags(tags.Select(x => new PostTag() { PostID = post.ID, TagName = x.Name }).ToList());

            return added;
        }

        public Post GetPost(int id)
        {
            var query = from p in context.Posts.Include("PostTags.Tag").Include("Blog")
                        where p.ID == id
                        select p;

            if (query.Count() == 1)
            {
                return query.First();
            }

            return null;
        }

        public List<Post> GetAllPosts()
        {
            var posts = from post in context.Posts.Include("PostTags.Tag")
                        select post;

            return posts.ToList();
        }

        public Post UpdatePostWithTags(Post post, List<Tag> tags)
        {
            tags = tags.GroupBy(x => x.Name).Select(y => y.First()).ToList();

            this.AddTags(tags);
            var changed = context.Update(post);

            //List<PostTag> oldPostTags = context.PostTags.Where(x => x.PostID == post.ID).ToList();
            //List<PostTag> newPostTags = tags.Select(x => new PostTag() { PostID = post.ID, TagName = x.Name }).ToList();

            List<PostTag> newTags = tags.Select(x => new PostTag() { PostID = post.ID, TagName = x.Name }).ToList();

            List<PostTag> deleted = context.PostTags
                .Where(x => x.PostID == post.ID)
                .Except(newTags)
                .ToList();

            List<PostTag> added = newTags
                .Except(context.PostTags)
                .ToList();

            context.PostTags.RemoveRange(deleted);
            context.PostTags.AddRange(added);

            context.SaveChanges();

            return changed.Entity;
        }

        public Post DeletePost(int id)
        {
            return this.DeletePost(GetPost(id));
        }

        public Post DeletePost(Post post)
        {
            if (post == null)
            {
                return null;
            }

            List<PostTag> oldPostTags = context.PostTags
                .Where(x => x.PostID == post.ID)
                .ToList();

            context.PostTags.RemoveRange(oldPostTags);

            var deleted = context.Posts.Remove(post);

            context.SaveChanges();

            return deleted.Entity;
        }

        private void AddTags(List<Tag> tags)
        {
            foreach (Tag tag in tags)
            {
                this.AddTag(tag);
            }
        }

        private void AddTag(Tag tag)
        {
            if (context.Tags.Where(x => tag == x).Count() == 0)
            {
                context.Tags.Add(tag);
                context.SaveChanges();
            }
        }

        private Post AddPost(Post post)
        {
            var added = context.Posts.Add(post);
            context.SaveChanges();

            return added.Entity;
        }

        private void AddPostTags(List<PostTag> postTags)
        {
            foreach (PostTag tag in postTags)
            {
                context.PostTags.Add(tag);
            }

            context.SaveChanges();
        }

        
        public List<Post> GetArchive(int year, int month)
        {
            var query = context.Posts
                .Include("PostTags.Tag")
                .Where(x => x.Time.Year == year && x.Time.Month == month)
                .ToList();

            return query;
        }

        public List<Post> GetPostsByTag(string tag)
        {
            var query = context.Posts
                .Include("PostTags.Tag")
                .Where(x => x.PostTags.Any(y => y.Tag.Name.Equals(tag, StringComparison.CurrentCultureIgnoreCase)))
                .ToList();

            return query;
        }

        public List<Post> GetPostsByBlog(int blogID)
        {
            Blog blog = this.GetBlog(blogID);

            if (blog == null)
            {
                return new List<Post>();
            }

            var query = context.Posts
                .Include("PostTags.Tag")
                .Where(x => x.BlogID == blogID)
                .ToList();

            return query;
        }

        public List<Post> Search(string term)
        {
            var query = context.Posts
                .Include("PostTags.Tag")
                .Where(x => x.Text.ToLower().Contains(term.ToLower()) || x.Title.ToLower().Contains(term.ToLower()))
                .ToList();

            return query;
        }

        public User GetByEmail(string email)
        {
            var query = context.Users
                .Where(x => x.Email.Equals(email))
                .FirstOrDefault();

            return query;
        }

        public List<Post> GetLatestPosts(int count)
        {
            var latest = context.Posts
                .Include("PostTags.Tag")
                .OrderBy(x => x.Time)
                .Take(count);

            return latest.ToList();
        }
    }
}
