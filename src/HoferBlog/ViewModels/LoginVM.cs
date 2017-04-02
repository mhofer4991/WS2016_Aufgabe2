using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class LoginVM
    {
        public string Username { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
