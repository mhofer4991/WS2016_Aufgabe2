using BlogStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
        }

        public UserVM(User user)
        {
            this.UserName = user.UserName;
            this.Description = user.Description;
        }

        public string UserName { get; set; }

        public string Description { get; set; }

        public bool CanModify { get; set; }
    }
}
