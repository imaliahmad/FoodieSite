using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodieSite.BOL
{
    public class AppUsers:IdentityUser
    {
        public string Password { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public long? ImageMasterId { get; set; } // foreign key



        // Foreign Keys or Navigation Properties
        [ForeignKey("ImageMasterId")] public virtual ImageMaster ImageMaster { get; set; }

        
        [InverseProperty(nameof(CategoryMaster.CreatedUser))]
        public virtual ICollection<CategoryMaster> CreatedCategorys { get; set; }


        [InverseProperty(nameof(CategoryMaster.ModifiedUser))]
        public virtual ICollection<CategoryMaster> ModifiedCategorys { get; set; }
    }
}
