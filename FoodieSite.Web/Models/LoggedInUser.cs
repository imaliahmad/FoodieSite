using System.Collections.Generic;

namespace FoodieSite.Web.Models
{
    public class LoggedInUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string ContactNo { get; set; }
        public long ImageMasterId { get; set; }
        public string ImageURL { get; set; }
        public string Gender { get; set; }
        public List<string> Roles { get; set; }
    }
}
