using FoodieSite.BOL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieSite.DAL
{
    public interface IUserDb
    {
        Task<AppUsers> Update(AppUsers obj);      
    }
    public class UserDb : IUserDb
    {
        private readonly FoodieSiteDbContext context;
        public UserDb(FoodieSiteDbContext _context)
        {
            context = _context;
        }

        public async Task<AppUsers> Update(AppUsers obj)
        {
            
            var user =await context.AppUsers.FirstOrDefaultAsync(u => u.Id == obj.Id);
            var hasher = new PasswordHasher<AppUsers>();

            user.Password = obj.Password == "Test@123" ? user.Password : obj.Password;
            user.PasswordHash = obj.Password == "Test@123" ? user.PasswordHash : hasher.HashPassword(null, obj.Password);
            user.Gender = obj.Gender;
            user.ContactNo = obj.ContactNo;
            user.ImageMasterId = obj.ImageMasterId;
            

            context.AppUsers.Update(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
