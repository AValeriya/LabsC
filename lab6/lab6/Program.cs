using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    interface ICompetitions
    {
        void Dope();
        void Payment();
    }

    enum Rank
    {
        Junior3 = 1,
        Junior2,
        Junior1,
        Adult3,
        Adult2,
        Adult1,
        CMS,
        MS,
        IMS,
        HMS
    }

    abstract class Sport : Person
    {
        public int Age { get; set; }
        public Rank Rank { get; set; }

        public Sport() : base()
        {
            Age = 17;
            Rank = Rank.CMS;
        }

        public Sport(string n, string c, int a, Rank r) : base(n, c)
        {
            Age = a;
            Rank = r;
        }

        public override string ToString() => base.ToString() + $"\nAge: {Age}\nRank: {Rank}";
    }

    class Sportsman : Sport, IComparable, ICloneable, ICompetitions
    {
        public string Sport { get; set; }

        public struct Comp
        {
            public DateTime date;
            public string name;
        }

        public Comp cmp;

        public Sportsman() : base()
        {
            Sport = "Canoeing";
            cmp.date = new DateTime(2020, 07, 01);
            cmp.name = "Championship";
        }

        public Sportsman(string n = "", string c = "", int a = 0, string s = "", Rank r = default, string cn = "", DateTime date = default) : base(n, c, a, r)
        {
            Sport = s;
            cmp.name = cn;
            cmp.date = date;
        }

        public override string ToString() => base.ToString() + $"\nSport: {Sport}\nCompetitions: {cmp.name}\t{cmp.date.ToString("d")}\n";

        int IComparable.CompareTo(object obj)
        {
            if (this.Age > ((Sportsman)obj).Age) return 1;
            if (this.Age < ((Sportsman)obj).Age) return -1;
            else return 0;
        }

        public object Clone() => (Sportsman)this.MemberwiseClone();

        public void Dope()
        {
            Name = Country = ID = Sport = cmp.name = "Disqualified";
            Age = 0;
            Rank = 0;
            cmp.date = default;
        }

        public void Payment()
        {
            Name = Country = ID = Sport = cmp.name = "Not allowed";
            Age = 0;
            Rank = 0;
            cmp.date = default;
        }
    }

    abstract class Person
    {
        public string Name { get; set; }
        public string Country { get; set; }
        protected string ID { get; set; }

        public Person()
        {
            Name = "Alexander";
            Country = "Belarus";
            ID = GenID();
        }

        public Person(string n, string c)
        {
            Name = n;
            Country = c;
            ID = GenID();
        }

        protected static string GenID() => Guid.NewGuid().ToString();

        public override string ToString() => $"Name: {Name}\nCountry: {Country}\nID: {ID}";
    }

    class Program
    {
        public static void Waiting()
        {
            Console.Write("Waiting input: ");
            Console.ReadKey();
            Console.WriteLine("\n");
        }

        public static int CheckAge()
        {
            int a;
            while (!int.TryParse(Console.ReadLine(), out a) || a <= 0)
                Console.Write("Incorrect input, repeat: ");
            return a;
        }

        public static int CheckRank()
        {
            int a;
            while (!int.TryParse(Console.ReadLine(), out a) || a < 1 || a > 10)
                Console.Write("Incorrect input, repeat: ");
            return a;
        }

        public static DateTime CheckDate()
        {
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
                Console.Write("Incorrect input, repeat: ");
            return date;
        }

        public static int CheckCompetitions()
        {
            int comp;
            while (!int.TryParse(Console.ReadLine(), out comp) || comp < 0 || comp > 1)
                Console.Write("Incorrect input, repeat: ");
            return comp;
        }

        public static Sportsman SetSportsman()
        {
            Sportsman obj = new Sportsman();
            Console.Write("Enter name: ");
            obj.Name = Console.ReadLine();
            Console.Write("Enter the country: ");
            obj.Country = Console.ReadLine();
            Console.Write("Enter the age: ");
            obj.Age = CheckAge();
            Console.Write("Enter the sport: ");
            obj.Sport = Console.ReadLine();
            Console.Write("Enter the sport rank(1 - Junior3, 2 - Junior2, 3 - Junior1, 4 - Adult3, 5 - Adult2, 6 - Adult1, 7 - CMS, 8 - MS, 9 - IMS, 10 - HMS): ");
            obj.Rank = (Rank)CheckRank();
            Console.Write("Enter the name of competitions: ");
            obj.cmp.name = Console.ReadLine();
            Console.Write("Enter the date of copmetitions: ");
            obj.cmp.date = CheckDate();
            Console.Write("Make a contribution? (0 - no, 1 - yes): ");
            int cont = CheckCompetitions();
            if (cont == 0)
            {
                obj.Payment();
                return obj;
            }
            Console.Write("Take a dope? (0 - no,  1 - yes): ");
            int dope = CheckCompetitions();
            if (dope == 0) return obj;
            else
            {
                obj.Dope();
                return obj;
            }
        }

        public static void SetList(Sportsman[] list)
        {
            for (int i = 0; i < list.Length; i++)
                list[i] = SetSportsman();
            Console.Clear();
            Console.WriteLine("List of spotsmen:");
            for (int i = 0; i < list.Length; i++)
                Console.WriteLine(list[i]);
        }

        static void Main(string[] args)
        {
            Sportsman sp = new Sportsman();
            Console.WriteLine(sp);
            Waiting();
            Sportsman[] list = new Sportsman[2];
            SetList(list);
            Waiting();
            Console.Clear();
            Console.WriteLine("Sorted list:");
            Array.Sort(list);
            foreach (Sportsman s in list)
                Console.WriteLine(s);
            Waiting();
            Console.WriteLine("Copy of the first sportsman in the list:");
            Sportsman obj = (Sportsman)list[0].Clone();
            Console.WriteLine(obj);
        }
    }
}
