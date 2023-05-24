using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.ViewModels
{
    public class CategoryMasterVM
    {
        public long CategoryMasterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<ItemMasterVM> ItemsList { get; set; }
    }
}
