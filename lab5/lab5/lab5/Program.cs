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

    public abstract class Sportman : Human
    {
        int countMedal = 0;
        Titul titul;
        public enum Titul
        {
           Razryad3,
           Razryad2,
           Razryad1,
           KMS,
           MS,
           Champion

        }
        public Sportman(string name, string surname, int age, int cM, Titul titul) : base(name, surname, age)
        {
            this.titul = titul;
            countMedal = cM;
        }
        public override string ToString()
        {
            string info = base.ToString();
            return info + $", count medals: { countMedal}, titul: {titul}";
        }

        public abstract void play();
        public abstract void play(String city);
    }

    public class Biatlonist : Sportman
    {
        double speed;
        double metkost;
        public Biatlonist(string name, string surname, int age, int cM, Titul titul, double speed, double m) : base(name, surname, age, cM, titul)
        {
            this.speed = speed;
            metkost = m;
        }

        public override void play()
        {
            Console.WriteLine("i am playing biatlon");
        }

        public override void play(string city)
        {
            Console.WriteLine("i am playing biatlon in " + city);
        }

        public override string ToString()
        {
            string info = base.ToString();
            return info + $", speed: {speed}, metkost: {metkost}";
        }
    }
    public class Chessman : Sportman
    {
        int IQ;
        public Chessman(string name, string surname, int age, int cM,Titul titul, int IQ) : base(name, surname, age, cM, titul)
        {
            this.IQ = IQ;
        }
        public override void play()
        {
            Console.WriteLine("i am playing chess");
        }

        public override void play(string city)
        {
            Console.WriteLine("i am playing chess in " + city);
        }

        public override string ToString()
        {
            string info = base.ToString();
            return info + $", IQ: {IQ}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Human.Population);
            Human person1 = new Human("Daniil", "Stepanov", 139);
            Human person2 = new Human("Ivan", "Ivanovich", "Ivanov", 20);
            Sportman biatlonist = new Biatlonist("Daria", "Domracheva", 35, 23,Sportman.Titul.Champion ,12, 99.9);
            Sportman chessman = new Chessman("Garry", "Casparov", 68, 101, Sportman.Titul.MS ,158);
            Console.WriteLine(Human.Population);
            Console.WriteLine(person1);
            Console.WriteLine(person2["Name"]);
            Console.WriteLine(biatlonist);
            Console.WriteLine(chessman);
            biatlonist.play();
            chessman.play();
            biatlonist.play("Minsk");
            chessman.play("Berlin");
            Console.ReadKey();


        }
    }
}