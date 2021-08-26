using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek4 {
    class Person {

        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public Sex Gender { get; private set; }
        public enum Sex{ MALE, FEMALE, UNDEFINED }

        public Person(string name, DateTime bday, Sex g) {
            Name = name;
            Birthday = bday;
            Gender = g;
        }
        public Person(string name, Sex g) {
            Name = name;
            Gender = g;
        }

        public override string ToString() {
            return this.Name + "(" + this.Gender + ")";
        }
    }
}
