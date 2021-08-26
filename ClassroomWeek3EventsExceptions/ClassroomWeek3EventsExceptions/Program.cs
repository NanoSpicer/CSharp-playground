using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek3EventsExceptions {
    class Program {

        static void Main(string[] args) {
            Console.WriteLine();
            Person mathew = new Person("Mathew");
            mathew.NameChanged += MyCustomHandler;
            mathew.Name = "John";
            try {
                mathew.Name = "Pancuato";
            }catch (TooUglyNameException tune) {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");   
                Console.WriteLine("Prevented an ugly name change! :>\n");
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
            } finally {
                Console.WriteLine("Restore Mathew's name...");
                mathew.Name = "Mathew";
            }
            
            exit();
        }

        private static void MyCustomHandler(Person p, string old_name) {
            Console.WriteLine(old_name + " changed its name to " + p.Name);
        }

        private static void exit() {
            Console.WriteLine("\nPress any key to exit this program...");
            Console.ReadKey();
        }
    }
}
