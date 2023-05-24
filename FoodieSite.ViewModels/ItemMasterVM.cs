using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.ViewModels
{
    public class ItemMasterVM
    {
        public long ItemMasterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public int Status { get; set; }
        public long? ImageMasterId { get; set; }
        public long CategoryMasterId { get; set; }

        public virtual ImageMasterVM ImageMaster { get; set; }
        public virtual CategoryMasterVM CategoryMaster { get; set; }
    }
}
