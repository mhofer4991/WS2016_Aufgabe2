using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlogStorage.Models
{
    public class Post
    {
        public int ID { get; set; }

        public int BlogID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public string FriendlyUrl { get; set; }

        public Blog Blog { get; set; }

        public ICollection<PostTag> PostTags { get; set; }
    }
}
