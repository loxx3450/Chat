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
        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public DateOnly Birthday { get; set; }

        public bool? IsMale { get; set; }

        public DateTime CreatedAt { get; set; }


        [JsonConstructor]
        public User(string login, string password, string fullName, DateOnly birthday, bool? isMale = null)
        {
            Login = login;
            Password = password;
            FullName = fullName;
            Birthday = birthday;
            IsMale = isMale;
        }

        public User(User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.FullName = user.FullName;
            this.Birthday = user.Birthday;
            this.IsMale = user.IsMale;
            this.CreatedAt = user.CreatedAt;
        }
    }
}
