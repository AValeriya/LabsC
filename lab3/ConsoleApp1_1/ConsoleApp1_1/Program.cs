using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class Human
    {
        private static int next_id = 0;
        public int Id { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public static int Population { get; private set; } = 0;
        public Human(string name, string surname, int age)
        {
            Population++;
            Name = name;
            Surname = surname;
            Age = age;
            MiddleName = default;
            Id = ++next_id;

        }

        public Human(string name, string middleName, string surname, int age)
        {
            Population++;
            Name = name;
            MiddleName = middleName;
            Surname = surname;
            Age = age;
            Id = ++next_id;
        }

        public override string ToString()
        {
            string info;
            if (MiddleName == default)
            {
                info = $"ID: {Id}, Name: {Name}, surname: {Surname}, age: {Age}";
            }
            else
            {
                info = $"ID: {Id}, Name: {Name}, middle name: {MiddleName}, surname: {Surname}, age: {Age}";
            }
            return info;
        }

        public string this[string field]
        {
            get
            {
                switch (field)
                {
                    case "Name":
                        return Name;
                    case "MiddleName":
                        return MiddleName;
                    case "Surname":
                        return Surname;
                    case "Age":
                        return Age.ToString();
                    default:
                        return null;

                }
            }
        }


    }

    public class Sportman : Human
    {
        int countMedal = 0;
        public Sportman(string name, string surname, int age, int cM): base(name, surname, age)
        {
            countMedal = cM;
        }
        public override string ToString()
        {
            string info = base.ToString();
            return info + $", count medals: { countMedal}";
        }
    }

    public class Biatlonist : Sportman
    {
        double speed;
        double metkost;
        public Biatlonist(string name, string surname, int age, int cM, double speed, double m) : base(name, surname, age, cM)
        {
            this.speed = speed;
            metkost = m;
        }
        public override string ToString()
        {
            string info = base.ToString();
            return info +$", speed: {speed}, metkost: {metkost}";
        }
    }
    public class Chessman : Sportman
    {
        int IQ;
        public Chessman(string name, string surname, int age, int cM, int IQ) : base(name, surname, age, cM)
        {
            this.IQ = IQ;
        }
        public override string ToString()
        {
            string info = base.ToString();
            return info +$", IQ: {IQ}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Human.Population);
            Human person1 = new Human("Daniil", "Stepanov", 139);
            Human person2 = new Human("Ivan", "Ivanovich", "Ivanov", 20);
            Sportman sportman = new Sportman("Grisha", "Pypkin", 33, 123);
            Biatlonist biatlonist = new Biatlonist("Daria", "Domracheva", 35, 23, 12, 99.9);
            Chessman chessman = new Chessman("Garry", "Casparov", 68, 101, 158);
            Console.WriteLine(Human.Population);
            Console.WriteLine(person1);
            Console.WriteLine(person2["Name"]);
            Console.WriteLine(sportman);
            Console.WriteLine(biatlonist);
            Console.WriteLine(chessman);
            Console.ReadKey();


        }
    }
}