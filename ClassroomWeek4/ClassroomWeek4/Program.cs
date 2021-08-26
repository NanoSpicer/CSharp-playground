using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek4 {
    class Program {
        static void Main(string[] args) {

            List<Person> people = new List<Person>();
            people.Add(new Person("John Cena", new DateTime(1980, 01, 13), Person.Sex.MALE));
            people.Add(new Person("Michael Rosen", new DateTime(1973, 08, 21), Person.Sex.MALE));
            people.Add(new Person("Albert Einstein", new DateTime(1911, 12, 25), Person.Sex.MALE));
            people.Add(new Person("Donald Duck", new DateTime(1900, 05, 14), Person.Sex.MALE));
            people.Add(new Person("Barack Obama", new DateTime(1985, 09, 04), Person.Sex.MALE));

            people.Add(new Person("Iris", new DateTime(1995, 10, 28), Person.Sex.FEMALE));
            people.Add(new Person("Marge Simpson", new DateTime(1991, 08, 31), Person.Sex.FEMALE));
            people.Add(new Person("Mona Lisa", new DateTime(1845, 02, 18), Person.Sex.FEMALE));
            people.Add(new Person("Ada Lovelace", new DateTime(1899, 7, 21), Person.Sex.FEMALE));
            people.Add(new Person("Iria", new DateTime(2002, 11, 14), Person.Sex.FEMALE));

            people.Add(new Person("Groudon", new DateTime(1982, 08, 21), Person.Sex.UNDEFINED));
            people.Add(new Person("Alex", new DateTime(1945, 03, 21), Person.Sex.UNDEFINED));
            people.Add(new Person("Rusty", new DateTime(1933, 04, 21), Person.Sex.UNDEFINED));
            people.Add(new Person("RED", new DateTime(1911, 02, 10), Person.Sex.UNDEFINED));
            people.Add(new Person("BLUE", new DateTime(1975, 05, 29), Person.Sex.UNDEFINED));
            //people.Add(new Person("BLUE", Person.Sex.UNDEFINED));

            Console.WriteLine(); Console.WriteLine();
            reportFemales(people);
            Console.WriteLine("----------------------------");
            pplGT18(people);
            Console.WriteLine("----------------------------");
            countPplGT21(people);
            Console.WriteLine("----------------------------");
            oldestPerson(people);
            Console.WriteLine("----------------------------");
            ageDiff(people);
            Console.WriteLine("----------------------------");
            averageAge(people);
            Console.WriteLine("----------------------------");
            averageAgeBySex(people);
            Console.WriteLine("----------------------------");
            malesSorted(people);
            Console.WriteLine("----------------------------");
            checkBDayFilled(people);

            Console.WriteLine(); Console.WriteLine();

            exit();
        }

        private static void reportFemales(List<Person> list) {

            var female_people =
                from person in list
                where (person.Gender == Person.Sex.FEMALE)
                select person;

            Console.WriteLine("Females are:");
            foreach(Person p in female_people) {
                Console.WriteLine("\t-" + p.Name);
            }
        }

        private static void pplGT18(List<Person> list) {
            var people_over_18 =
                from person in list
                where (person.Birthday.Year <= (DateTime.Today.Year - 18))
                select person;
            Console.WriteLine("People over 18 are:");
            foreach(Person p in people_over_18) {
                Console.WriteLine("\t-" + p.Name+"        "+p.Birthday);
            }
        }

        private static void countPplGT21(List<Person> list) {
            var people_over_21 =
                from person in list
                where (person.Birthday.Year <= (DateTime.Today.Year - 21))
                select person;

            Console.WriteLine("There are " + people_over_21.Count() + " people over 21.");
        }


        private static void oldestPerson(List<Person> list) {
            DateTime minimum_birthday = (from person in list select person.Birthday).Min();
            var oldest_person =
                from person in list
                where(person.Birthday == minimum_birthday)
                select person;

            Console.WriteLine("The oldest people are:");
            foreach(Person p in oldest_person) {
                Console.WriteLine("\t-" + p +"     "+p.Birthday);
            }

        }

        /// <summary>
        /// Gives the age difference between the youngest and the oldest person.
        /// </summary>
        /// <param name="list"></param>
        private static void ageDiff(List<Person> list) {
            DateTime minimum_birthday = (from person in list select person.Birthday).Min();
            DateTime maximum_birthday = (from person in list select person.Birthday).Max();
            Console.WriteLine("The age difference between the youngest and the oldest is " + (maximum_birthday.Year - minimum_birthday.Year)+" years");
        }
        private static void averageAge(List<Person> list) {
            var result =
                from person in list
                select (DateTime.Today.Year- person.Birthday.Year);

            Console.WriteLine("The average age is " + result.Average() + " years");
        }
        private static void averageAgeBySex(List<Person> list) {
            foreach(Person.Sex p in Enum.GetValues(typeof(Person.Sex))) {
                var query_result = 
                    from person in list
                    where (person.Gender==p)
                    select (DateTime.Today.Year - person.Birthday.Year);

                Console.WriteLine("Average age for " + p + ": " + query_result.Average());
            }
        }

        /// <summary>
        /// Males sorted from youngest to oldest
        /// </summary>
        /// <param name="list"></param>
        private static void malesSorted(List<Person> list) {
            var query_result =
                from person in list
                where (person.Gender == Person.Sex.MALE)
                orderby  person.Birthday descending
                select person;
            Console.WriteLine("Males sorted:");
            foreach(Person p in query_result) {
                Console.WriteLine("\t" + p + "     " + p.Birthday);
            }
        }

        private static void checkBDayFilled(List<Person> list) {
            DateTime default__empty_date = new DateTime();
            //!!! FIX ME !!! person.Birthday != null. Now Comparing with std created DateTime
            var all_bdays =
                from person in list
                where (person.Birthday != default__empty_date)
                select person;
            if (all_bdays.Count() == list.Count) Console.WriteLine("checkBDayFilled: True");
            else Console.WriteLine("checkBDayFilled: False");
        }

        private static void exit() {
            Console.WriteLine("Hit any key to exit the program...");
            Console.ReadKey();
        }
    }
}
