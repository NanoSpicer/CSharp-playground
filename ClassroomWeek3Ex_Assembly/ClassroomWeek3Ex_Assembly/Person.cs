using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek3Ex_Assembly {
    class Person {

        public static readonly string UGLY_NAME = "Pancuato";

        public delegate void NameChangedEvent(Person p, string old_name);
        public event NameChangedEvent NameChanged;

        private string _name;

        public string Name {
            get { return _name; }
            set {
                string aux = _name;
                if (value != UGLY_NAME) {
                    _name = value;
                    if (NameChanged != null) NameChanged(this, aux);
                } else {
                    throw new TooUglyNameException("Name was too ugly: " + UGLY_NAME);
                }
                
            }
        }

        /// <summary>
        /// Pass the person's name.
        /// </summary>
        /// <param name="name"></param>
        public Person(string name) {
            Name = name;
        }

        public override string ToString() {
            return "This person name is " + this._name;
        }

    }
}
