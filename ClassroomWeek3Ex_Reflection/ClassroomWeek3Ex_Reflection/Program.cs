using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek3Ex_Reflection {
    class Program {
        private static readonly string ASSEMBLY_PATH = "C:\\Users\\Miquel\\source\\repos\\ClassroomWeek3Ex_Assembly\\ClassroomWeek3Ex_Assembly\\bin\\Debug\\ClassroomWeek3Ex_Assembly.dll";
        static void Main(string[] args) {
            Assembly assembly = Assembly.LoadFile(ASSEMBLY_PATH);
            foreach (var item in assembly.GetLoadedModules()) {
                Console.WriteLine("Module: {0}", item.ToString());
            }
            foreach (Type item in assembly.GetTypes()) {
                Console.WriteLine("---------------------------");
                Console.WriteLine(item.ToString());
                foreach (PropertyInfo prop in item.GetProperties()) {
                    Console.WriteLine("\t-" + prop.ToString());
                }
                Console.WriteLine("---------------------------");
            }

            Type my_type = assembly.GetType("ClassroomWeek3Ex_Assembly.Person");
            var my_object = Activator.CreateInstance(my_type, "John Smith");
            MethodInfo method = my_type.GetMethod("ToString");

            Console.WriteLine(method.Invoke(my_object, null));

            exit();
        }

        private static void exit() {
            Console.WriteLine("Hit any key to exit the program...");
            Console.ReadKey();
        }
    }
}
