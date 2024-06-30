using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? FullName { get; set; }

        public DateOnly? Birthday { get; set; }

        public bool? IsMale { get; set; }


        [JsonConstructor]
        public User(string username, string email, string password, string? fullName = null, DateOnly? birthday = null, bool? isMale = null)
        {
            Username = username;
            Email = email;
            Password = password;
            FullName = fullName;
            Birthday = birthday;
            IsMale = isMale;
        }

        public User(User user)
        {
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.FullName = user.FullName;
            this.Birthday = user.Birthday;
            this.IsMale = user.IsMale;
        }
    }
}
