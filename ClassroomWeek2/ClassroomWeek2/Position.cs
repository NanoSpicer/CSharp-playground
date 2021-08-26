using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek2 {
    class Position {

        public int X { get; private set; }
        public int Y { get; private set; }

        /**
         * Creates a Position object; as default at (0,0). 
         * 
         * If values are less than 0 then it'll throw an exception
         * */
        public Position(int x = 0, int y = 0) {
            if (x < Terrain.MIN_VALUE || y < Terrain.MIN_VALUE || x>= Terrain.MAX_VALUE_X || y>= Terrain.MAX_VALUE_X) 
                throw new IndexOutOfRangeException("Indique un número de coordenadas adecuado");
            this.X = x;
            this.Y = y;
        }

        public override string ToString() {
            return "[" + X + "," + Y + "]";
        }

    }
}
