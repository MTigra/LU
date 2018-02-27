using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sr
{
    class Program
    {
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            do
            {
                int n = InputInt("Выедите количество желаемое кол-во записей:");
                Person[] characters = new Person[n];
                for (int i = 0; i < characters.Length; i++)
                {
                    characters[i] =
                        new Person(GetRandomString(rnd.Next(4, 9)),
                            GetRandomString(rnd.Next(6, 12)),
                            rnd.Next(10, 66));
                }

                Console.WriteLine("\n***До сортировки:***");
                ShowArray(characters);

                Array.Sort(characters);

                Console.WriteLine("\n***После сортировки:***");
                ShowArray(characters);
                Console.WriteLine("Что угодно чтобы повторить решение заново. Esc чтобы выйти.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Prints info about evey element in Person's array
        /// </summary>
        /// <param name="arr"></param>
        static void ShowArray(Person[] arr)
        {
            for (int index = 0; index < arr.Length; index++)
            {
                Console.WriteLine(arr[index]);
            }
        }

        /// <summary>
        /// Innovative method to get random string.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static string GetRandomString(int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                    stringBuilder.Append((char)rnd.Next('А', 'Я' + 1));
                stringBuilder.Append((char)rnd.Next('а', 'я' + 1));
            }
            return stringBuilder.ToString();

        }

        /// <summary>
        /// Method to read and parse user's input.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static private int InputInt(string s)
        {
            int n;
            Console.WriteLine(s);
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.WriteLine("Вы ошиблись. Разрешено вводить только положительные числа.\n" + s);
            }
            return n;
        }
    }

    internal struct Person : IComparable<Person>
    {
        //Fields
        private string name;
        private int age;
        private string lastname;

        //Ctor
        public Person(string name, string lastname, int age)
        {
            
            this.name = name;
            this.lastname = lastname;
            this.age = age;
        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        ///<summary>
        /// Realization of Generic Icomparable
        /// </summary>
        public int CompareTo(Person obj)
        {
            return Age.CompareTo(obj.Age);
        }

        /// <summary>
        /// Overriden ToString().
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format($"Name: {Name} Lastname:{Lastname} Age:{Age}");
        }
    }



}
