using ClassLibraryGenTree;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DBPruebas : ICapaDatos
    {
        // id count that increment in each new record
        int idCount = 6;

        // list to store users
        private List<User> users = new List<User>();
        private Dictionary<string, List<Person>> userToPersons = new Dictionary<string, List<Person>>();

        public DBPruebas()
        {
            // Crear usuarios con diferentes roles
            User user1 = new User("1", "user1", "John", "Doe", "user1@example.com", "password123", Role.USER);
            User user2 = new User("2", "user2", "Jane", "Smith", "user2@example.com", "securepass", Role.USER);
            User adminUser = new User("3", "admin", "Admin", "User", "admin@example.com", "adminpass", Role.ADMINISTRATOR);
            User manager1 = new User("4", "manager1", "Manager", "One", "manager1@example.com", "managerpass1", Role.MANAGER);
            User manager2 = new User("5", "manager2", "Manager", "Two", "manager2@example.com", "managerpass2", Role.MANAGER);

            users.Add(user1);
            users.Add(user2);
            users.Add(adminUser);
            users.Add(manager1);
            users.Add(manager2);

            foreach (User user in users.FindAll(u => u.Role == Role.MANAGER))
            {
                userToPersons[user.Id] = new List<Person>();
            }

            Person paternalGrandfather = new Person("1", "Abuelo", "", "Paterno", "", new DateTime(1930, 1, 10));
            Person paternalGrandmother = new Person("2", "Abuela", "", "Paterna", "", new DateTime(1930, 3, 5));
            Person maternalGrandfather = new Person("3", "Abuelo", "", "Materno", "", new DateTime(1945, 11, 20));
            Person maternalGrandmother = new Person("4", "Abuela", "", "Materna", "", new DateTime(1945, 7, 15));
            Person father = new Person("5", "Padre", "", "Paterno", "", new DateTime(1975, 8, 25));
            Person mother = new Person("6", "Madre", "", "Materno", "", new DateTime(1975, 4, 30));
            Person son = new Person("7", "Hijo", "", "Paterno", "", new DateTime(2000, 12, 8));

            son.Father = father;
            son.Mother = mother;

            father.Father = paternalGrandfather;
            father.Mother = paternalGrandmother;

            mother.Father = maternalGrandfather;
            mother.Mother = maternalGrandmother;
        }

        public bool GuardaUsuario(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException("user", "the object user is null");
                }

                bool existUser = users.Exists(u => u.Email == user.Email);
                
                if(!existUser)
                {
                    user.Id = idCount++.ToString();
                    users.Add(user);
                    return true;
                }

                return false;

            }
            catch(ArgumentNullException err)
            {
                Console.WriteLine($"Error: ${err}");
                return false;
            }
            catch(Exception err)
            {
                Console.WriteLine($"Error: ${err}");
                throw new ApplicationException("Error al guardar un usuario", err);
            }
        }
    
        public User LeeUsuario(String email)
        {
            try
            {
                if(email == null)
                {
                    throw new ArgumentNullException("email", "the string object is null");
                }

                bool existUser = users.Exists(u => u.Email == email);
                
                if (!existUser) return null;

                return users.First(u => u.Email == email);
            } 
            catch(ArgumentNullException err)
            {
                Console.WriteLine($"Error: ${err}");
                return null;
            }
            catch(Exception err)
            {
                Console.WriteLine($"Error: ${err}");
                throw new ApplicationException("Error al leer un usuario", err);
            }
        }

        public bool ValidaUsuario(string email, string password)
        {
            try
            {
                if(email == null)
                {
                    throw new ArgumentNullException("email", "The string object is null");
                } else if (password  == null)
                {
                    throw new ArgumentNullException("password", "the string object is null");
                }

                bool existUser = users.Exists(u => u.Email == email);
                if (!existUser) return false;

                User user = users.First(u => u.Email == email);

                bool validatePassword = user.Password == Utils.Encriptar(password);

                return validatePassword;
            }
            catch(ArgumentNullException err)
            {
                Console.WriteLine($"Error: {err}");
                return false;
            }
            catch(Exception err)
            {
                Console.WriteLine($"Error: {err}");
                throw new ApplicationException("Error al validar usuario", err);
            }
        }

        public int NumUsuarios()
        {
            return users.Count();
        }

        public bool GuardaPersona(Person p, string userId)
        {
            try
            {
                if (p == null)
                {
                    throw new ArgumentNullException("p", "El person object is null");
                }

                bool validateManager = users.Exists(u => u.Id == userId && u.Role == Role.MANAGER);

                if (!validateManager)
                {
                    throw new InvalidOperationException("Not Authorized");
                }

                if(!userToPersons.ContainsKey(userId))
                {
                    userToPersons[userId] = new List<Person>();
                }

                userToPersons[userId].Add(p);

                return true;
            }
            catch (ArgumentNullException err)
            {
                Console.WriteLine($"Error: {err}");
                return false;
            }
            catch (InvalidOperationException err)
            {
                Console.WriteLine($"Error: {err}");
                return false;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Error: {err}");
                throw new ApplicationException("Error al guardar una persona", err);
            }
        }

        public Person LeePersona(string idPersona)
        {
            try
            {

                Person person = userToPersons.Values
                    .SelectMany(personList => personList)
                    .FirstOrDefault(p => p.Id == idPersona);
                return person;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Error: {err}");
                throw new ApplicationException("Error al leer una persona", err);
            }
        }


        public int NumPersonas()
        {
            return userToPersons.Values.SelectMany(personList => personList).ToList().Count();
        }

        public int NumPersonsByUserId(string userId)
        {
            try
            {
                bool existUser = users.Exists(u => u.Id == userId);
                if (!existUser)
                    return 0;

                return userToPersons[userId].Count();

            }
            catch (Exception err)
            {
                Console.WriteLine($"Error: {err}");
                throw new ApplicationException("Error al validar usuario", err);
            }
        }

        public List<Person> Ancestros(string idPersona)
        {
            try
            {
                Person person = LeePersona(idPersona);
                if(person != null)
                {
                    return person.GetGenTree();
                }

                return null;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Error: {err}");
                throw new ApplicationException("Error al leer una persona", err);
            }
        }
     
    }
}
