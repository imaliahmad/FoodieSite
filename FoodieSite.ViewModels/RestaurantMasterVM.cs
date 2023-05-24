using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.ViewModels
{
    public class RestaurantMasterVM
    {
        public long RestaurantMasterId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
