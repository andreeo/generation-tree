using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryGenTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGenTree.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void PersonTest()
        {
            Person person = new Person("1", "fistName", "secondName", "firstSurname", "secondSurname", new DateTime(2015, 12, 25));
            Assert.AreEqual(person.Id, "1");
            Assert.AreEqual(person.First_name, "fistName");
            Assert.AreEqual(person.Second_name, "secondName");
            Assert.AreEqual(person.First_surname, "firstSurname");
            Assert.AreEqual(person.Second_surname, "secondSurname");
            Assert.AreEqual(person.Birth_day, new DateTime(2015, 12, 25));

            person.Id = "2";
            Assert.AreEqual(person.Id, "2");

            person.First_name = "updatedfistName";
            Assert.AreEqual(person.First_name, "updatedfistName");

            person.Second_name = "updatedSecondName";
            Assert.AreEqual(person.Second_name, "updatedSecondName");

            person.First_surname = "updatedfirstSurname";
            Assert.AreEqual(person.First_surname, "updatedfirstSurname");

            person.Second_surname = "updatedSecondSurname";
            Assert.AreEqual(person.Second_surname, "updatedSecondSurname");

            person.Birth_day = new DateTime(2020, 12, 10);
            Assert.AreEqual(person.Birth_day, new DateTime(2020, 12, 10));

        }

        [TestMethod()]
        public void ToStringTest()
        {
            Person person = new Person("1", "fistName", "secondName", "firstSurname", "secondSurname", new DateTime(2015, 12, 25));
            Assert.AreEqual(person.ToString(), "Id: 1, First Name: fistName, Second Name: secondName, First Surname: firstSurname, Second Surname: secondSurname, Birth Day: 25/12/2015 0:00:00");
        }

        [TestMethod()]
        public void GetGenTreeTest()
        {

            Person father = new Person("1", "fatherFistName", "fatherSecondName", "fatherFirstSurname", "fathersecondSurname", new DateTime(1980, 12, 25));
            Person mother = new Person("1", "motherFistName", "motherSecondName", "motherFirstSurname", "motherSecondSurname", new DateTime(1987, 12, 25));

            Person person = new Person("1", "fistName", "secondName", "firstSurname", "secondSurname", new DateTime(2020, 12, 25), father, mother);
            List<Person> tree = person.GetGenTree();
            Assert.IsTrue(tree.Count == 1);
        }
    }
}