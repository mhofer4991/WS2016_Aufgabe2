using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogStorage.Models
{
    public class User : IdentityUser
    {
        public string Description { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
