using System;
using System.Collections.Generic;
using System.Text;

namespace lab7
{
    class Number : IComparable<Number>, IEquatable<Number>
    {
        protected int integer; // Целый числитель
        protected int natural; // Натуральный знаменатель

        // Конструктор:
        public Number(int numerator, int denominator)
        {
            integer = numerator;
            if (denominator > 0)
            {
                natural = denominator;
            }
            else if (denominator < 0)
            {
                natural = -denominator;
            }
            else
            {
                Console.WriteLine("Знаменатель не может быть равен нулю!");
                natural = 1;
            }
        }

        // Перегрузка мат. операторов:
        public static Number operator +(Number n1, Number n2) //для двух Number
        {
            if (n1.natural == n2.natural) // Если одинаковые знаменатели
                return new Number(n1.integer + n2.integer, n1.natural);
            else
                return new Number((n1.integer * n2.natural) + (n2.integer * n1.natural), n1.natural * n2.natural);
        }

        public static Number operator -(Number n1, Number n2) //для двух Number
        {
            if (n1.natural == n2.natural) // Если одинаковые знаменатели
                return new Number(n1.integer - n2.integer, n1.natural);
            else
                return new Number((n1.integer * n2.natural) - (n2.integer * n1.natural), n1.natural * n2.natural);
        }

        public static Number operator *(Number n1, Number n2) //для двух Number
        {
            return new Number(n1.integer * n2.integer, n1.natural * n2.natural);
        }

        public static Number operator /(Number n1, Number n2) //для двух Number
        {
            return new Number(n1.integer * n2.natural, n1.natural * n2.integer);
        }

        public static Number operator +(Number n1, int integer) //для Number и int 
        {
            Number n2 = new Number(integer, 1);
            return n1 + n2;
        }

        public static Number operator -(Number n1, int integer) //для Number и int
        {
            Number n2 = new Number(integer, 1);
            return n1 - n2;
        }

        public static Number operator *(Number n1, int integer) //для Number и int
        {
            Number n2 = new Number(integer, 1);
            return n1 * n2;
        }

        public static Number operator /(Number n1, int integer) //для Number и int
        {
            Number n2 = new Number(integer, 1);
            return n1 / n2;
        }

        // Перегрузка операторов сравнения:
        public static bool operator >(Number n1, Number n2)
        {
            if (n1.integer == n2.integer && n1.natural == n2.natural) // Если числ. и знам. равны
                return false;
            else if (n1.natural == n2.natural && n1.integer > n2.integer) // Если знам. равны а числ.1 > числ.2
                return true;
            else if (n1.integer == n2.integer && n1.natural < n2.natural) //Если числ. равны а знам.1 < знам.2
                return true;
            else if (n1.integer != n2.integer && n1.natural != n2.natural) //Если числ. не равны и знам. не равны
            {
                Number resultOne = new Number(n1.integer * n2.natural, n1.natural * n2.natural); // Приводим к общ. знаменат.
                Number resultTwo = new Number(n2.integer * n1.natural, n1.natural * n2.natural); // Приводим к общ. знаменат.
                // и сравниваем числители:
                if (resultOne.integer > resultTwo.integer)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool operator <(Number n1, Number n2)
        {
            if (n1.integer == n2.integer && n1.natural == n2.natural) // Если числ. и знам. равны
                return false;
            else if (n1.natural == n2.natural && n1.integer < n2.integer) // Если знам. равны а числ.1 < числ.2
                return true;
            else if (n1.integer == n2.integer && n1.natural > n2.natural) //Если числ. равны а знам.1 > знам.2
                return true;
            else if (n1.integer != n2.integer && n1.natural != n2.natural) //Если числ. не равны и знам. не равны
            {
                Number resultOne = new Number(n1.integer * n2.natural, n1.natural * n2.natural); // Приводим к общ. знаменат.
                Number resultTwo = new Number(n2.integer * n1.natural, n1.natural * n2.natural); // Приводим к общ. знаменат.
                // и сравниваем числители:
                if (resultOne.integer < resultTwo.integer)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool operator ==(Number n1, Number n2)
        {
            if (n1.Equals(n2))
                return true;
            else
                return false;
        }

        public static bool operator !=(Number n1, Number n2)
        {
            if (n1.Equals(n2))
                return false;
            else
                return true;
        }

        //Методы:
        public int CompareTo(Number other) //Переопределение метода CompareTo<Number> (для двух Number)
        {
            if (other is Number)
            {
                double r1 = integer / (double)natural; // текущее число
                double r2 = other.integer / (double)other.natural; // число, переданное в кач. параметра
                return r1.CompareTo(r2); // Вызвать стандартный метод для сравнения двух объектов double
            }
            else
            {
                throw new ArgumentException("Unable to compare objects!");
            }
        }

        public bool Equals(Number other) //Переопределение метода Equals<Number> (для двух Number)
        {
            if (other is Number)
            {
                double r1 = this.integer / (double)this.natural; // текущее число
                double r2 = other.integer / (double)other.natural; // число, переданное в кач. параметра
                return r1.Equals(r2); // Вызвать стандартный метод для сравнения двух объектов double
            }
            else
            {
                throw new ArgumentException("Unable to compare objects!");
            }
        }

        public static Number Create(string str) // Создать Number из строки
        {
            int numerator;
            int denominator;
            string[] s = str.Split('/');
            if (s.Length == 2 && Int32.TryParse(s[0], out numerator) && Int32.TryParse(s[1], out denominator))
            {
                return new Number(numerator, denominator);
            }
            else
            {
                return null;
            }
        }

        public override string ToString() // Вывести в виде строки
        {
            return $"{integer}/{natural}";
        }

        //Перегрузка операций преобразования типов:
        public static implicit operator Number(string str) // Неявно преобразовать string к Number
        {
            return Number.Create(str);
        }

        public static implicit operator string(Number number) // Неявно преобразовать Number к string
        {
            return number.ToString();
        }

        public static explicit operator double(Number number) // Явно преобразовать Number к double
        {
            return number.integer / (double)number.natural;
        }
    }

        class Program
    {
        static void Main()
        {
            Number a = new Number(9, 3);    // Создание с помощью конструктора
            Number b = Number.Create("8/19");   // Создание из строки с помощью метода Create
            Number c = "2/3";   // Создание с помощью неявного преобразования string к Number

            Console.WriteLine($"{ a.ToString() }");     // Вывод с помощью переопределенного метода ToString()
            Console.WriteLine($"{ b }");    // Вывод как строку (неявное преобразование типов)
            Console.WriteLine($"{ (double)c }");    // Вывод как double (явное преобразование типов)

            Console.WriteLine($"{a} + {b} = {a + b}");   // Number + Number
            Console.WriteLine($"{a} / {c} = {a / c}");   // Number / Number
            Console.WriteLine($"{b} + 3 = {b + 3}");    // Number + int

            if (b > c)      //Сравнение двух Number
                Console.WriteLine($"{b} > {c}");
            if (a != c)     //Сравнение двух Number
                Console.WriteLine($"{(double)a} != {(double)c}");

            var n1 = new Number(10, 22);
            var n2 = new Number(5, 11);

            Console.WriteLine($"{ n1.CompareTo(n2) }"); // Метод вернет 0
            Console.WriteLine($"{ n1.Equals(n2) }"); // Метод вернет true
            if (n1 == n2) 
            Console.WriteLine("n1 == n2");
            Console.ReadKey();
        }
    }
}