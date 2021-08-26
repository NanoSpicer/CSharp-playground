using System;

namespace ClassroomWeek2 {
    public class Cell {

        public static readonly int MIN_MOVEMENT_COST = 1;
        public static readonly int MAX_MOVEMENT_COST = 5;

        public int MovementCost { get; private set; }

        public Cell(int cost) {
            this.MovementCost = cost;
        }

        public override string ToString() => "[R]";
    }
}

