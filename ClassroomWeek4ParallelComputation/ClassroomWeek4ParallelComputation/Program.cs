using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek4ParallelComputation {
    class Program {
        public static readonly int ARRAY_SIZE = 10;

        static void Main(string[] args) {
            int[] my_array = new int[ARRAY_SIZE];
            for (int i = 0; i < my_array.Length; i++) {
                my_array[i] = i;
            }

            
            foreach(int i in my_array) {
                Console.Write("[" + i + "]");
            }
            Console.WriteLine();

            Parallel.ForEach(my_array, (int i) => {
                Console.Write("[" + i + "]");
            });
            Console.WriteLine(); Console.WriteLine();
            exit();
        }

        private static void exit() {
            Console.Write("Press any key to exit the program...");
            Console.ReadKey();
        }
    }
}
