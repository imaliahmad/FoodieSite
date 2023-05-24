using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodieSite.BOL
{
    public class CategoryMaster
    {
        [Key]
        public long CategoryMasterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } // Foreign Key
        public string ModifiedBy { get; set; } // Foreign Key
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }



        //Navigation Property or Related Entity
        public virtual IEnumerable<ItemMaster> ItemsList { get; set; }

        [ForeignKey("CreatedBy")] public virtual AppUsers CreatedUser { get; set; }
        [ForeignKey("ModifiedBy")] public virtual AppUsers ModifiedUser { get; set; }
    }
}
