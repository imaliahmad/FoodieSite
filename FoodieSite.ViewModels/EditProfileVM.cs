using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodieSite.ViewModels
{
    public class EditProfileVM
    {
        public string Id { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string PasswordHash { get; set; }

        public string ContactNo { get; set; }
        public string Gender { get; set; }
        public string[] GenderList { get; set; }
        public long? ImageMasterId { get; set; }
        public string ImageURL { get; set; }
        public string ImageStr { get; set; }
    }
}
