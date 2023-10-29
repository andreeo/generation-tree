using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGenTree
{
    public enum Role
    {
        ADMINISTRATOR,
        MANAGER,
        NORMAL
    }
    public class User
    {
        // declarate variables
        private string username;
        private string first_name;
        private string last_name;
        private string email;
        private string password;
        private Role role;
        private DateTime createdAt;
        private DateTime updatedAt;
        private DateTime accessedAt;

        // create constructor
        public User(string username, string first_name, string last_name, string email, string password, Role role)
        {
            this.Username = username;
            this.First_name = first_name;
            this.Last_name = last_name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }

        // create setters and getters
        public string Username { get => username; set => username = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public Role Role { get => role; set => role = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public DateTime AccessedAt { get => accessedAt; set => accessedAt = value; }
    }
}
