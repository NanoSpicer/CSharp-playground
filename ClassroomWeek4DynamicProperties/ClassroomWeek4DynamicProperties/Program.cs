using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek4DynamicProperties {
    class Program {
        static void Main(string[] args) {
            dynamic john = new ExpandoObject();
            john.Name = "John";
            john.Surname = "Cena";

            dynamic lilly = new ExpandoObject();
            lilly.Name = "Lilliam";
            lilly.Surname = "Foster";

            dynamic obama = new ExpandoObject();
            obama.Name = "Barack";
            obama.Surname = "Obama";

            dynamic mich = new ExpandoObject();
            mich.Name = "Michael";
            mich.Surname = "Jackson";

            List<dynamic> my_list = new List<dynamic>();
            my_list.Add(john);
            my_list.Add(lilly);
            my_list.Add(obama);
            my_list.Add(mich);


            var people_whose_names_has_A =
                from person in my_list
                where hasCharacter('a', person) || hasCharacter('A', person)
                select person;
            Console.WriteLine("People whose name has A or a:");
            foreach(dynamic person in people_whose_names_has_A) {
                Console.WriteLine("\t" + person.Name + " " + person.Surname);
            }
            
            exit();
        }

        private static bool hasCharacter(char charr, dynamic person) {
            
            foreach (char item in person.Name) {
                if (item == charr) return true;
            }
            return false;
        }
        private static void exit() {
            Console.Write("Press any key to exit the program...");
            Console.ReadKey();
        }
    }
}
