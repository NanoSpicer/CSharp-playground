using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek3EventsExceptions {
    class TooUglyNameException : Exception {

        public string message { get; private set; }
        public TooUglyNameException(string message) {
            this.message = message;
        }
    }
}
