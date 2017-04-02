using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class ChangeEmailVM : SecureChangeVM
    {
        [Display(Name = "New email"), Required, DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }
    }
}
