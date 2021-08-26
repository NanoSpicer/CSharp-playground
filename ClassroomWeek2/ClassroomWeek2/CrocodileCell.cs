using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek2 {
    class CrocodileCell : Cell, IDangerous {
        public CrocodileCell(int cost) : base(cost) {
            
        }

        public float GetProbabilityDamage() => 0.6f;
        public override string ToString() => "[C]";
    }
}
