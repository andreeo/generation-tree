using System;
using System.Collections.Generic;

namespace ClassLibraryGenTree
{
    public class Person
    {
        // declarate variables
        private int id;
        private string first_name;
        private string second_name;
        private string first_surname;
        private string second_surname;
        private DateTime birth_day;
        private Person father;
        private Person mother;

        // constructors
        public Person(
            int id, string first_name, string second_name, string first_surname,
            string second_surname, DateTime birth_day
            )
        {
            this.id = id;
            this.first_name = first_name;
            this.second_name = second_name;
            this.first_surname = first_surname;
            this.second_surname = second_surname;
            this.birth_day = birth_day;
        }

        // getters and setters
        public int Id { get => this.id; set => this.id = value; }
        public string First_name { get => this.first_name; set => this.first_name = value; }
        public string Second_name { get => this.second_name; set => this.second_name = value; }
        public string First_surname { get => this.first_surname; set => this.first_surname = value; }
        public string Second_surname { get => this.second_surname; set => this.second_surname = value; }
        public DateTime Birth_day { get => this.birth_day; set => this.birth_day = value; }
        public Person Father
        {
            get => this.father;
            set
            {

                if (value != null)
                {
                    if (
                        value.Birth_day.Year < this.Birth_day.Year &&
                        (this.Birth_day.Year - value.Birth_day.Year) >= 12)
                    {
                        father = value;
                    }
                    else
                    {
                        throw new ApplicationException("No se cumple la restricción para ser ancestro");
                    }
                }
                else 
                {
                    throw new ApplicationException("No se puede asignar un acestro null");
                }
            }
        }
        public Person Mother
        {
            get => this.mother;
            set
            {

                if (value != null)
                {
                    if (
                        value.Birth_day.Year < this.Birth_day.Year &&
                        (this.Birth_day.Year - value.Birth_day.Year) >= 12)
                    {
                        mother = value;
                    }
                    else
                    {
                        throw new ApplicationException("No se cumple la restricción para ser ancestro");
                    }
                }
                else
                {
                    throw new ApplicationException("No se puede asignar un acestro null");
                }
            }

        }

        public override string ToString()
        {
            return $"Id: {Id}, First Name: {First_name}, Second Name: {Second_name}, " +
                       $"First Surname: {First_surname}, Second Surname: {Second_surname}, " +
                       $"Birth Day: {Birth_day}";
        }

        public List<Person> GetGenTree()
        {
            List<Person> genTree = new List<Person>();
            GetGenTreeRecursive(this, genTree);
            return genTree;
        }

        static void GetGenTreeRecursive(Person person, List<Person> genTree)
        {


            if (person != null)
            {
                genTree.Add(person);
                GetGenTreeRecursive(person.Father, genTree);
                GetGenTreeRecursive(person.Mother, genTree);
            }
        }

    }
}
