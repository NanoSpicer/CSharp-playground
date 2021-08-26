using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek2 {
    class WaterCell : Cell, IDangerous {
        public WaterCell(int cost) : base(cost) {

        }

        public float GetProbabilityDamage() => 0.1f;
        public override string ToString() => "[W]";
    }
}
