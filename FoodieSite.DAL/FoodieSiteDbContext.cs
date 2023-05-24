using FoodieSite.BOL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodieSite.DAL
{
    public class FoodieSiteDbContext:IdentityDbContext
    {
        public FoodieSiteDbContext(DbContextOptions<FoodieSiteDbContext> options): base(options)
        {
          
        }


        //DbSet
        public DbSet<RestaurantMaster> RestaurantMaster{ get; set; }
        public DbSet<CategoryMaster> CategoryMaster { get; set; }
        public DbSet<ItemMaster> ItemMaster { get; set; }
        public DbSet<ImageMaster> ImageMaster { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
    }
}
