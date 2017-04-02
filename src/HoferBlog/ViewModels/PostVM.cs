using BlogStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class PostVM
    {
        public PostVM()
        {
            this.Tags = new List<TagVM>();
        }

        public PostVM(Post post) : this()
        {
            this.ID = post.ID;
            this.BlogID = post.BlogID;
            this.Title = post.Title;
            this.Text = post.Text;
            this.Time = post.Time;
            this.FriendlyUrl = post.FriendlyUrl;

            if (post.PostTags != null)
            {
                this.Tags = post.PostTags.Select(x => new TagVM(x.Tag)).ToList();
            }
        }

        public int ID { get; set; }

        public int BlogID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public ICollection<TagVM> Tags { get; set; }

        public string FriendlyUrl { get; set; }

        public bool CanModify { get; set; }
    }
}
