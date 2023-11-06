using ClassLibraryGenTree;
using DBLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibTests
{
    [TestClass()]
    public class DBTests
    {
        [TestMethod()]
        public void SaveNewUserTest()
        {

            DB db = new DB();
            User u = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, true);

            Assert.IsTrue(db.GuardaUsuario(u));
        }

        [TestMethod()]
        public void ShouldNotSaveNullUserTest()
        {
            DB db = new DB();
            Assert.IsFalse(db.GuardaUsuario(null));
        }

        [TestMethod()]
        public void ReadUserTest()
        {
            DB db = new DB();
            User foundUser = db.LeeUsuario("user1@example.com");

            Assert.AreEqual("user1", foundUser.Username);
        }

        [TestMethod()]
        public void NotFoundUserTest()
        {
            DB db = new DB();
            User foundUser = db.LeeUsuario("notInDB@example.com");

            Assert.IsNull(foundUser);
        }

        [TestMethod()]
        public void UserValidationTest()
        {
            DB db = new DB();
            Assert.IsTrue(db.ValidaUsuario("user1@example.com", "User1245test@"));
        }

        [TestMethod()]
        public void InvalidUserPasswordValidationTest()
        {
            DB db = new DB();
            Assert.IsFalse(db.ValidaUsuario("user1@example.com", "invalidPass"));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Null password.")]
        public void NullUserPasswordValidationTest()
        {
            DB db = new DB();

            Assert.IsFalse(db.ValidaUsuario("user1@example.com", null));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Null Email.")]
        public void NullUserEmailValidationTest()
        {
            DB db = new DB();

            Assert.IsFalse(db.ValidaUsuario(null, "invalidPass"));
        }

        [TestMethod()]
        public void CountActiveUsersTest()
        {
            DB db = new DB();
            User u = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, false);
            Assert.IsTrue(db.GuardaUsuario(u));

            Assert.AreEqual(5, db.NumUsuariosActivos());
        }

        [TestMethod()]
        public void CountAllUsersTest()
        {
            DB db = new DB();
            User u = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, false);
            Assert.IsTrue(db.GuardaUsuario(u));
            Assert.AreEqual(6, db.NumUsuarios());
        }

        [TestMethod()]
        public void SaveNewPersonTest()
        {
            DB db = new DB();
            Person person = new Person(8, "fistName", "secondName", "firstSurname", "secondSurname", new DateTime(2020, 12, 25));
            Assert.IsTrue(db.GuardaPersona(person));
        }

        [TestMethod()]
        public void ShouldNotSaveNullPersonTest()
        {
            DB db = new DB();
            Assert.IsFalse(db.GuardaPersona(null));
        }

        [TestMethod()]
        public void ReadPersonTest()
        {
            DB db = new DB();
            Person foundPerson = db.LeePersona(1);

            Assert.AreEqual("Abuelo", foundPerson.First_name);
        }

        [TestMethod()]
        public void NotFoundPersonTest()
        {
            DB db = new DB();
            Person foundPerson = db.LeePersona(100);

            Assert.IsNull(foundPerson);
        }

        [TestMethod()]
        public void CountAllPersonsTest()
        {
            DB db = new DB();
            Assert.AreEqual(7, db.NumPersonas());
        }

        [TestMethod()]
        public void ReturnListOfPersons()
        {
            DB db = new DB();
            var listPersons = db.LeePersonas();
            Assert.AreEqual(7, listPersons.Count());
        }

    }
}
