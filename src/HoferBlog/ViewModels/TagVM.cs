using BlogStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class TagVM
    {
        public TagVM()
        {
        }

        public TagVM(Tag tag)
        {
            if (tag != null)
            {
                this.Name = tag.Name;
            }
        }

        public string Name { get; set; }
    }
}
