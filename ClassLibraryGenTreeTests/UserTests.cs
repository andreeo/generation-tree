﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryGenTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGenTree.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void UserTest()
        {
            User u = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, true);
            Assert.AreEqual(u.Id, 1);
            Assert.AreEqual(u.First_name, "fakeFirstName");
            Assert.AreEqual(u.Last_name, "fakeLastName");
            Assert.AreEqual(u.Email, "email@test.com");
            Assert.IsNotNull(u.Password);
            Assert.AreEqual(u.Role, Role.USER);
            Assert.IsTrue(u.IsActive);

            u.Id = 2;
            Assert.AreEqual(u.Id, 2);

            u.First_name = "updatedFirstName";
            Assert.AreEqual(u.First_name, "updatedFirstName");

            u.Last_name = "updatedLastName";
            Assert.AreEqual(u.Last_name, "updatedLastName");

            u.Email = "new@test.com";
            Assert.AreEqual(u.Email, "new@test.com");

            u.Role = Role.ADMINISTRATOR;
            Assert.AreEqual(u.Role, Role.ADMINISTRATOR);

            u.IsActive = false;
            Assert.IsFalse(u.IsActive);
        }


        [TestMethod()]
        [ExpectedException(typeof(Exception), "An email is invalid.")]
        public void ShouldThrowWhenUserSetInvalidMailTest()
        {
            User u = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "test@test.com", "strongSecurePassword", Role.USER, true);
            u.Email = "notAnEmail";
        }

        [TestMethod()]
        public void EqualsTest()
        {
            User u1 = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, true);
            User u2 = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, true);
            Assert.AreEqual(u1, u2);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            User u1 = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, true);
            User u2 = new User(1, "fakeUserName", "fakeFirstName", "fakeLastName", "email@test.com", "strongSecurePassword", Role.USER, true);
            Assert.AreEqual(u1.GetHashCode(), u2.GetHashCode());
        }
    }
}