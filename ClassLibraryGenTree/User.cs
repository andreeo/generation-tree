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
        USER
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

        // create constructor
        public User(string username, string first_name, string last_name, string email, string password, Role role)
        {
            this.Username = username;
            this.First_name = first_name;
            this.Last_name = last_name;
            this.Email = email;
            this.Password = Utils.Encriptar(password);
            this.Role = role;
            this.CreatedAt = DateTime.Now;
        }

        // create setters and getters
        public string Username
        {
            get => username; 
            set
            { 
                username = value;
                this.UpdatedAt = DateTime.Now;
            } 
        }
        public string First_name
        {
            get => first_name;
            set
            {
                first_name = value;
                this.UpdatedAt = DateTime.Now;
            }
        }
        public string Last_name
        { 
            get => last_name; 
            set 
            {
                last_name = value;
                this.UpdatedAt = DateTime.Now;
            } 
        }
        public string Email
        { 
            get => email;
            set
            {
                try
                {
                    if(!Utils.EsEMail(email))
                    {
                        throw new Exception("Email inválido");
                    }
                    email = value;
                    this.UpdatedAt = DateTime.Now;
                }
                catch(Exception err)
                {
                    throw err;
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                this.UpdatedAt = DateTime.Now;
            }
        }
        public Role Role
        {
            get => role;
            set 
            {
                role = value;
                this.UpdatedAt = DateTime.Now;
            }
        }
        public DateTime CreatedAt
        { 
            get => createdAt;
            set {
                createdAt = value;
                this.UpdatedAt = DateTime.Now;
            }
        }
        public DateTime UpdatedAt
        { 
            get => updatedAt; 
            set {
                updatedAt = value;
                this.UpdatedAt = DateTime.Now;
            }
        }
        public DateTime AccessedAt { get; set; }
    }
}
