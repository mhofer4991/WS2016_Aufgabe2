using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogStorage.Models
{
    public class Tag
    {
        public string Name { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Tag)
            {
                Tag tag = (Tag)obj;

                return tag.Name == this.Name;
            }

            return false;
        }
    }
}
