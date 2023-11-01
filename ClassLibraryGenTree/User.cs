using System;
using System.CodeDom;
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
        private int id;
        private string username;
        private string first_name;
        private string last_name;
        private string email;
        private string password;
        private Role role;
        private bool isActive;


        // create constructor
        public User(int id, string username, string first_name, string last_name, string email, string password, Role role, bool isActive)
        {
            this.Id = id;
            this.Username = username;
            this.First_name = first_name;
            this.Last_name = last_name;
            this.Email = email;
            this.Password = Utils.Encriptar(password);
            this.Role = role;
            this.IsActive = isActive;
        }

        // create setters and getters
        public int Id
        {
            get => id;
            set => id = value;
        }
        public string Username
        {
            get => username;
            set
            {
                username = value;

            }
        }
        public string First_name
        {
            get => first_name;
            set
            {
                first_name = value;
            }
        }
        public string Last_name
        {
            get => last_name;
            set
            {
                last_name = value;

            }
        }
        public string Email
        {
            get => email;
            set
            {
                try
                {
                    if (!Utils.EsEMail(value))
                    {
                        throw new Exception("Email inválido");
                    }
                    email = value;

                }
                catch (Exception err)
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

            }
        }
        public Role Role
        {
            get => role;
            set
            {
                role = value;

            }
        }

        public bool IsActive { get => isActive; set => isActive = value; }

        public bool IsValidPassword(string password)
        {
            if (this.password == Utils.Encriptar(password))
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   id == user.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + EqualityComparer<int>.Default.GetHashCode(id);
        }
    }
}
