using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek2 {
    class FireCell : Cell, IDangerous {

        public FireCell(int cost) : base(cost) { }

        public float GetProbabilityDamage() => 0.5f;
        public override string ToString() => "[F]";
    }
}
