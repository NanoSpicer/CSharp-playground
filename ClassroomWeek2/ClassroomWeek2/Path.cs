using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomWeek2 {
    class Path : IEnumerable {

        public Terrain PathOwner { get; }
        private List<Position> path;

        public enum DIRECTION : byte { UP, DOWN, LEFT, RIGHT };

        public Path(Terrain path_owner, Position start_position) {
            path = new List<Position> { start_position };
            this.PathOwner = path_owner;
        }

        public void Move(DIRECTION dir) {

            Position last_position = path[path.Count - 1];
            Position new_position = null;

            switch (dir) {

                case DIRECTION.UP:
                    if((last_position.Y - 1)>=Terrain.MIN_VALUE)
                        new_position = new Position(last_position.X,last_position.Y-1);
                    break;
                case DIRECTION.DOWN:
                    if((last_position.Y+1) < this.PathOwner.GetHeight()) 
                        new_position = new Position(last_position.X, last_position.Y + 1);
                    break;
                case DIRECTION.LEFT:
                    if ((last_position.X - 1) >= Terrain.MIN_VALUE)
                        new_position = new Position(last_position.X - 1, last_position.Y);
                    break;
                case DIRECTION.RIGHT:
                    if ((last_position.X + 1) < this.PathOwner.GetWidth())
                        new_position = new Position(last_position.X + 1, last_position.Y);
                    break;
            }

            if(new_position!=null) path.Add(new_position);
        }

        public Position this[int idx] {
            get { return path[idx]; }
            set { path[idx] = value; }
        }

        /**
         * Tells the cost of following this path.
         * */
        public float GetCost() {
            float total_cost = 0;

            for (int i = 0; i < this.path.Count; i++) {
                /*
                 * The following line gets the cost of the given cell from the terrain 
                 * that's assigned to this path given the "i's" indexed position
                 **/
                int actualCost = this.PathOwner.GetCell(this[i].X, this[i].Y).MovementCost;

                // If it's the last or the first cell in the path, the movement cost is halved.
                if (i == 0 || i == this.path.Count - 1) actualCost = actualCost / 2;

                total_cost += actualCost;
            }

            return total_cost;
        }

        // IEnumerable interface implementation
        public IEnumerator GetEnumerator() {
            return path.GetEnumerator();
        }

        public override string ToString() {

            string result = "The path is as follows: ";

            foreach(Position pos in this.path) {
                result += pos.ToString() + ", ";
            }

            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}
