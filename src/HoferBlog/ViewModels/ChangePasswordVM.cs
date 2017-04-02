using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HoferBlog.ViewModels
{
    public class ChangePasswordVM : SecureChangeVM
    {
        [Display(Name = "New password"), Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm password"), DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
