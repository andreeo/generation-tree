using ClassLibraryGenTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public class DB : IDB
    {
        private readonly Dictionary<String, User> TBLUser = new Dictionary<String, User>();
        private readonly Dictionary<int, Person> TBLPerson = new Dictionary<int, Person>();
        private int nextUserId = 1;
        private int nextPersonId = 1;

        public DB()
        {
            // Crear usuarios con diferentes roles
            User user1 = new User(nextUserId++, "user1", "John", "Doe", "user1@example.com", "User1245test@", Role.USER, true);
            User user2 = new User(nextUserId++, "user2", "Jane", "Smith", "user2@example.com", "securepass", Role.USER, true);
            User adminUser = new User(nextUserId++, "admin", "Admin", "User", "admin@example.com", "adminpass", Role.ADMINISTRATOR, true);
            User manager1 = new User(nextUserId++, "manager1", "Manager", "One", "manager1@example.com", "managerpass1", Role.MANAGER, true);
            User manager2 = new User(nextUserId++, "manager2", "Manager", "Two", "manager2@example.com", "managerpass2", Role.MANAGER, true);

            TBLUser.Add(user1.Email, user1);
            TBLUser.Add(user2.Email, user2);
            TBLUser.Add(adminUser.Email, adminUser);
            TBLUser.Add(manager1.Email, manager1);
            TBLUser.Add(manager2.Email, manager2);

            // Pre-carga personas
            Person paternalGrandfather = new Person(nextPersonId++, "Abuelo", "", "Paterno", "", new DateTime(1930, 1, 10));
            TBLPerson.Add(paternalGrandfather.Id, paternalGrandfather);

            Person paternalGrandmother = new Person(nextPersonId++, "Abuela", "", "Paterna", "", new DateTime(1930, 3, 5));
            TBLPerson.Add(paternalGrandmother.Id, paternalGrandmother);

            Person maternalGrandfather = new Person(nextPersonId++, "Abuelo", "", "Materno", "", new DateTime(1945, 11, 20));
            TBLPerson.Add(maternalGrandfather.Id, maternalGrandfather);

            Person maternalGrandmother = new Person(nextPersonId++, "Abuela", "", "Materna", "", new DateTime(1945, 7, 15));
            TBLPerson.Add(maternalGrandmother.Id, maternalGrandmother);

            Person father = new Person(nextPersonId++, "Padre", "", "Paterno", "", new DateTime(1975, 8, 25));
            father.Father = paternalGrandfather;
            father.Mother = paternalGrandmother;
            TBLPerson.Add(father.Id, father);

            Person mother = new Person(nextPersonId++, "Madre", "", "Materno", "", new DateTime(1975, 4, 30));
            mother.Father = maternalGrandfather;
            mother.Mother = maternalGrandmother;
            TBLPerson.Add(mother.Id, mother);

            Person son = new Person(nextPersonId++, "Hijo", "", "Paterno", "", new DateTime(2000, 12, 8));
            son.Father = father;
            son.Mother = mother;
            TBLPerson.Add(son.Id, son);
        }

        public bool GuardaUsuario(User u)
        {
            try
            {
                if (u == null)
                {
                    throw new ArgumentNullException("user", "the object user is null");
                }

                // Verificar si el usuario ya existe en la lista
                TBLUser.TryGetValue(u.Email, out User existingUser);

                if (existingUser != null)
                {
                    // Actualizar el usuario
                    existingUser = u;
                }
                else
                {
                    // Agregar un nuevo usuario
                    u.Id = nextUserId++;
                    TBLUser.Add(u.Email, u);
                }

                return true;
            }
            catch (ArgumentNullException err)
            {
                Console.WriteLine($"Error: ${err}");
                return false;
            }
        }

        public User LeeUsuario(string email)
        {
            TBLUser.TryGetValue(email, out User existingUser);
            return existingUser;
        }

        public bool ValidaUsuario(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email", "The string object is null");
            }
            else if (password == null)
            {
                throw new ArgumentNullException("password", "the string object is null");
            }
            User u = LeeUsuario(email);
            if (u == null)
            {
                return false;
            }
            return u.IsValidPassword(password);
        }

        public int NumUsuarios()
        {
            return TBLUser.Count();
        }


        public int NumUsuariosActivos()
        {
            return TBLUser.Count(emailUserKeypair => emailUserKeypair.Value.IsActive);
        }

        // Metodos para Personas

        public bool GuardaPersona(Person p)
        {
            try
            {
                if (p == null)
                {
                    throw new ArgumentNullException("person", "the object user is null");
                }

                // Verificar si la persona ya existe en la lista
                TBLPerson.TryGetValue(p.Id, out Person existingPerson);

                if (existingPerson != null)
                {
                    // Actualizar la persona
                    existingPerson = p;
                }
                else
                {
                    // Agregar un nueva persona
                    p.Id = nextPersonId++;
                    TBLPerson.Add(nextPersonId, p);

                }

                return true;
            }
            catch (ArgumentNullException err)
            {
                Console.WriteLine($"Error: ${err}");
                return false;
            }
        }

        public Person LeePersona(int idPersona)
        {
            TBLPerson.TryGetValue(idPersona, out Person existingPerson);
            return existingPerson;
        }

        public int NumPersonas()
        {
            return TBLPerson.Count();
        }

        public Person Ancestros(int idPersona, int altura)
        {
            return TBLPerson.FirstOrDefault(idPersonKeypair => idPersonKeypair.Key == idPersona).Value;
        }

        public List<Person> LeePersonas()
        {
            return TBLPerson.Values.ToList();
        }
    }
}
