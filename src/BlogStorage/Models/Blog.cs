using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogStorage.Models
{
    public class Blog
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string UserID { get; set; }

        public User User { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
