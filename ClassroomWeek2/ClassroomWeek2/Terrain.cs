using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassroomWeek2 {

    class Terrain {

        // This value indicates the minimum indexing value for this terrain. (which is 0, as terrain will be an array)
        public static readonly int MIN_VALUE = 0;

        // These values indicate the maximum indexing value for this terrain. (These values are modifiable by the program tester).
        public static readonly int MAX_VALUE_X = 20;
        public static readonly int MAX_VALUE_Y = MAX_VALUE_X;

        // The data that this terrain holds. 2D array.
        private Cell[,] terrain_itself;

        /**
         * Constructs a Terrain of (width x height) cells
         * */
        public Terrain(int width, int height) {
            this.terrain_itself = new Cell[width, height];

            for (int i = 0; i < terrain_itself.GetLength(0); i++) {
                for (int j = 0; j < terrain_itself.GetLength(1); j++) {
                    // Randomize cell's type.
                    Cell auxiliar;
                    switch (Program.DICE.Next(0, 4)) {
                        case 0:
                            auxiliar = new Cell(Cell.MIN_MOVEMENT_COST);
                            break;
                        case 1:
                            auxiliar = new FireCell(2);
                            break;
                        case 2:
                            auxiliar = new WaterCell(3);
                            break;
                        case 3:
                            auxiliar = new CrocodileCell(Cell.MAX_MOVEMENT_COST);
                            break;
                        default:
                            auxiliar = null;
                            break;
                    }
                    terrain_itself[i, j] = auxiliar;
                }
            }
        }

        /**
         * Returns the size of the table.
         * Not its dimension, but the amount of cells that this terrain contains
         * */
        public int GetSize() {
            return terrain_itself.Length;
        }

        public int GetWidth() {
            return terrain_itself.GetLength(0);
        }
        /// <summary>
        /// This method returns the amount of Cells of height that this terrain has.
        /// </summary>
        /// <returns>
        /// An integer with the amount of Cells of height of the 2D Cell array.
        /// </returns>
        public int GetHeight() {
            return terrain_itself.GetLength(1);
        }

        public void SetCell(int x, int y, Cell new_cell) {
            this.terrain_itself[x, y] = new_cell;
        }

        public Cell GetCell(int x, int y) {
            return this.terrain_itself[x, y];
        }

        public override string ToString() {

            Console.WriteLine("This terrain's dimension is: " + this.GetWidth() + "x" + this.GetHeight());
            string result = "\n";
            
            result += " Y\\X ";
            for (int i = 0; i < this.terrain_itself.GetLength(0); i++) {
                result += " " + i + "   ";
            }
            result += "\n";
            for (int j = 0; j < this.terrain_itself.GetLength(1); j++) {
                for (int i = 0; i < this.terrain_itself.GetLength(0); i++) {
                    if (i == 0) result += " "+j+"  ";
                    result += " " + terrain_itself[i, j].ToString() + " ";
                    
                }
                result += "\n";
            }
            result += "\nCheat sheet:\n [R] = regular_cell; [F] = fire_cell;\n [W] = water_cell; [C] = croco_cell; ";
            result += "\n";
            return result;
        }
    }
}
