using System;
using System.Collections.Generic;
using System.Collections;


namespace lab8
{
    //интерфейсы
    interface IFoo
    {
        void GetInfo();
    }

    interface IComparison<T>
    {
        void AgeComparison(T o1, T o2);

        void CourseComparison(T o1, T o2);

        void GpaComparison(T o1, T o2);
    }

    public struct Proverka
    {
        private int age;
        private int course;
        private int gpa;
        public int Age
        {
            get { return age; }
            set
            {
                if (value == 0 || value > 120)
                {
                    Console.WriteLine("It's false, try again");
                }
                else
                {
                    age = value;
                }
            }
        }
        public int Course
        {
            get { return course; }
            set
            {
                if (value == 0 || value > 5)
                {
                    Console.WriteLine("It's false, try again");
                }
                else
                {
                    course = value;
                }
            }
        }
        public int Gpa
        {
            get { return gpa; }
            set
            {
                if (value == 0 || value > 100)
                {
                    Console.WriteLine("It's false, try again");
                }
                else
                {
                    gpa = value;
                }
            }
        }
    }

    class HumanComparison : IComparison<Human>
    {
        public void AgeComparison(Human o1, Human o2)//неявная реализация метода
        {
            if (o1.proverka.Age > o2.proverka.Age)
            {
                Console.WriteLine($"{o1.name} older, than {o2.name}");
            }
            else
            {
                if (o2.proverka.Age > o1.proverka.Age)
                {
                    Console.WriteLine($"{o2.name} older, than {o1.name}");
                }
                else
                {
                    Console.WriteLine($"{o1.name} same age {o2.name}");
                }
            }
        }

        public void CourseComparison(Human o1, Human o2)//неявная реализация метода
        {
            if (o1.proverka.Course > o2.proverka.Course)
            {
                Console.WriteLine($"{o1.name} higher course, than  {o2.name}");
            }
            else
            {
                if (o2.proverka.Course > o1.proverka.Course)
                {
                    Console.WriteLine($"{o2.name} higher course, than  {o1.name}");
                }
                else
                {
                    Console.WriteLine($"{o1.name} same course {o2.name}");
                }
            }
        }

        public void GpaComparison(Human o1, Human o2)//неявная реализация метода
        {
            if (o1.proverka.Gpa > o2.proverka.Gpa)
            {
                Console.WriteLine($" {o1.name} GPA biger, than  {o2.name}");
            }
            else
            {
                if (o2.proverka.Gpa > o1.proverka.Gpa)
                {
                    Console.WriteLine($" {o2.name} GPA biger than {o1.name}");
                }
                else
                {
                    Console.WriteLine($" {o1.name} same GPA {o2.name}");
                }
            }
        }
    }

    class HumanComparer : IComparer<Human>
    {
        public int Compare(Human p1, Human p2) =>
        p1.name.Length.CompareTo(p2.name.Length);
    }
    class Grant
    {
        public delegate void SalaryHandler(string message);// объявление делегата
        public event SalaryHandler Notify;  // определение события
        public int Balance1;

        public void Balance(int balance)
        {
            Balance1 = balance;
            Notify?.Invoke($"Grant: {balance}");   // вызов события 
        }

        public void Put(int balance)
        {
            Balance1 += balance;
            Notify?.Invoke($"Replenishment: {balance}");   // вызов события 
        }

        public void Take(int balance)
        {
            Balance1 -= balance;
            Notify?.Invoke($"Take off: {balance}");   // вызов события
        }
    }
    class Human : Grant, IFoo, IComparable
    {
        public string name;
        public Proverka proverka;

        void IFoo.GetInfo()
        {
            Console.WriteLine($"{name} \nAge: {proverka.Age} \nGrant: {Balance1} ");
        }

        public Human(string name, int age, int course, int gpa, int balance)
        {
            this.name = name;
            proverka.Age = age;
            proverka.Course = course;
            proverka.Gpa = gpa;
            this.Balance1 = balance;
        }
        //реализация интерфейса IComparable
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Human otherStudent = obj as Human;
            if (otherStudent != null)
                return this.proverka.Age.CompareTo(otherStudent.proverka.Age);
            else
                throw new ArgumentException("Error incomparable");
        }
    }

    class Program
    {   //делегаты
        delegate void Show();
        delegate void MenuHandler(string message);

        private static void Welcome()
        {
            Console.WriteLine("Welcom to university");
        }

        private static void Bye()
        {
            Console.WriteLine("Bye, come later");
        }
        static void Main(string[] args)
        {
            Show show = Welcome;//создаем переменную делегата и присваем ей адрес метода
            show();
            int balance = 170;
            int age = 0;
            string name = "";
            try
            {
                Console.WriteLine("Enter your name:");
                name = Console.ReadLine();
                Console.WriteLine("Enter your age: ");
                age = int.Parse(Console.ReadLine());
            }
            catch (FormatException)//обработка исключения
            {
                Console.WriteLine("\nError:\"age\" you haven't entered a number");
            }
            Console.WriteLine("The best students's list:");
            Human student1 = new Human("Arthur", 18, 1, 98, balance);
            Human student2 = new Human("Gregori", 20, 3, 38, balance);
            Human student3 = new Human("Lili", 20, 4, 67, balance);
            Human student4 = new Human("Selena", 21, 2, 85, balance);
            IFoo foo = student1;
            foo.GetInfo();
            IFoo foo2 = student2;
            foo2.GetInfo();
            IFoo foo3 = student3;
            foo3.GetInfo();
            IFoo foo4 = student4;
            foo4.GetInfo();
            Grant grant = new Grant();
            grant.Notify += delegate (string mes) //устоановка анонимного метода в качестве обработчика
            {
                Console.WriteLine(mes);
            };
            MenuHandler menu = messege => Console.WriteLine(messege);//Лямбда-выражение
            menu("enter 1, if you want to compare the men's parameters");
            menu("enter 2, if you want to compare the women's parameters");
            menu("enter 3, if you want to sort these people by age");
            menu("enter 4, if you want to sort these people by name length");
            menu("enter 5, if you want to lift a grant");
            menu("enter 6, if you want to lower a grant");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            switch (n)
            {
                case 1:
                    {
                        HumanComparison personComparison = new HumanComparison();
                        Console.WriteLine("Men's compare: ");
                        personComparison.AgeComparison(student1, student2);
                        personComparison.CourseComparison(student1, student2);
                        personComparison.GpaComparison(student1, student2);
                        break;
                    }
                case 2:
                    {
                        HumanComparison personComparison = new HumanComparison();
                        Console.WriteLine("Women's compare: ");
                        personComparison.AgeComparison(student3, student4);
                        personComparison.CourseComparison(student3, student4);
                        personComparison.GpaComparison(student3, student4);
                        break;
                    }
                case 3:
                    {
                        Human[] people = new Human[] { student1, student2, student3, student4 };
                        Array.Sort(people);
                        foreach (Human p in people)
                        {
                            Console.WriteLine(p.name + "(" + p.proverka.Age + ")");
                        }
                        break;
                    }
                case 4:
                    {
                        Human[] people = new Human[] { student1, student2, student3, student4 };
                        Array.Sort(people, new HumanComparer());
                        foreach (Human p in people)
                        {
                            Console.WriteLine(p.name + "(" + p.proverka.Age + ")");
                        }
                        break;
                    }
                case 5:
                    {
                        grant.Balance(balance);
                        Console.WriteLine($"How much?");
                        int put = Convert.ToInt32(Console.ReadLine());
                        grant.Put(put);
                        Console.WriteLine($"Changed, a new grant is: {grant.Balance1}");
                        break;
                    }
                case 6:
                    {
                        grant.Balance(balance);
                        Console.WriteLine($"How much?");
                        int take = Convert.ToInt32(Console.ReadLine());
                        grant.Take(take);
                        Console.WriteLine($"Changed, a new grant is: {grant.Balance1}");
                        break;
                    }
                    show -= Welcome;// убираем обработчик
                    show += Bye;// добавляем обработчик
                    show(); // вызываем метод
                    Console.ReadKey();
            }
        }
    }
}