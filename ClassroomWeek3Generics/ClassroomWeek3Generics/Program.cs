using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek3Generics {
    class Program {
        static void Main(string[] args) {

            List<Person> persons = new List<Person>();
            persons.Add(new Person("John", new DateTime(1995, 1, 20)));
            persons.Add(new Person("Delilah", new DateTime(1986, 4, 12)));
            persons.Add(new Person("Kevin", new DateTime(2002, 8, 23)));

            Person john = new Person("John", new DateTime(1995, 1, 20));
            if (persons.Contains(john)) Console.WriteLine(john.ToString() + " is in the list.");
            else Console.WriteLine(john.ToString() + " is NOT in the list.");

            /*
             *  At first, it did not work because the "Equals" method was not overriden.
             *  As per what my first statement says, the program was probably comparing
             *  the references to the objects and not the content of the objects themselves
             *  and, since the references were different (because there were 2 new statements)
             *  the method "Contains" """didn't work""" 
             *  
             *  (Actually it worked, but we didn't expect that thing coming, eh?).
             * */

            Console.WriteLine("------------------------------------------------------");
            List<Person> to_be_sorted = new List<Person>();
            to_be_sorted.Add(new Person("John", new DateTime(1920, 8, 22)));
            to_be_sorted.Add(new Person("Alvin", new DateTime(1920, 8, 22)));
            to_be_sorted.Add(new Person("Alvin", new DateTime(1921, 8, 22)));
            to_be_sorted.Add(new Person("Alvin", new DateTime(1920, 8, 22)));

            Console.WriteLine("Before sorting: ");
            foreach (var item in to_be_sorted) {
                Console.WriteLine("\t"+item);
            }
            to_be_sorted.Sort();
            Console.WriteLine("After sorting: ");
            foreach (var item in to_be_sorted) {
                Console.WriteLine("\t" + item);
            }

            exit();
        }

        private static void exit() {
            Console.WriteLine("\n\n");
            Console.WriteLine("Hit any key to exit this program.");
            Console.ReadKey();
        }
    }
}
