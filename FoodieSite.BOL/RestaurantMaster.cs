using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodieSite.BOL
{
    public class RestaurantMaster
    {
        [Key]
        public long RestaurantMasterId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
