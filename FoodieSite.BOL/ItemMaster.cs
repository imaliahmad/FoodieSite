using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodieSite.BOL
{
    public class ItemMaster
    {
        [Key]
        public long ItemMasterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public int Status { get; set; }
        public long? ImageMasterId { get; set; } //Foreign Key
        public long CategoryMasterId { get; set; } //Foreign Key
        public string CreatedBy { get; set; } 
        public string ModifiedBy { get; set; } 
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }



        ////Foreign Keys
        [ForeignKey("ImageMasterId")] public virtual ImageMaster ImageMaster { get; set; }
        [ForeignKey("CategoryMasterId")] public virtual CategoryMaster CategoryMaster { get; set; }

    }
}
