using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek3Generics {
    class Person : IComparable {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Constructor where the "name" param is for the Person's name
        /// and the bday is the birthday of the given person.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bday"></param>
        public Person(string name, DateTime bday) {
            this.Name = name;
            this.Birthday = bday;
        }

        public override string ToString() {
            return Name + " (" + Birthday.ToString() + ")";
        }

        public override bool Equals(object obj) {
            Person given = obj as Person;
            // If given is != null, then, we can compare the "obj" object.
            if (given != null) return given.Name.Equals(this.Name) && given.Birthday.Equals(this.Birthday);
            // If not, we return false since they won't be equal since we can't compare them.
            return false;
        }

        public override int GetHashCode() {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// This methods compares two objects "Person" by its name.
        /// If the two names happened to be equal, then it would compare them
        /// by its Birthday attribute.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj) {
            Person given = obj as Person;
            int result = this.Name.CompareTo(given.Name);
            if (result != 0) return result;
            return this.Birthday.CompareTo(given.Birthday);
        }
    }
}
