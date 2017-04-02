using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogStorage.Models
{
    public class PostTag
    {
        public int PostID { get; set; }

        public string TagName { get; set; }

        public Post Post { get; set; }

        public Tag Tag { get; set; }

        public override int GetHashCode()
        {
            return (this.PostID.ToString() + this.TagName).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is PostTag)
            {
                PostTag pt = (PostTag)obj;

                return pt.PostID == this.PostID && pt.TagName == this.TagName;
            }

            return false;
        }
    }
}
