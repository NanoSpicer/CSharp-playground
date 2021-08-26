using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    C# Coding
        Open Visual Studio and create a C# Console Application Project

    * Create class Cell
        * add property MovementCost 
    * Create class Terrain, must be a 2D matrix (jagged array or multi-dimensional array?)
        o Constructor, cell read/write operations, get size...
    * Create class Position, must represent a position (zero based x-y coordinates) in the Terrain
    * Create Class Path, an object of class Path represents a sequence of positions that describe a path (a Path is linked always to a Terrain).
        o Constructor also receives a start position
        o Move, receives a direction (only four available directions: Left, Right, Up, Down). Don't check if it's a valid movement.
        o Indexer operator to access positions
        o foreach method must be valid
        o GetCost, returns the total cost of move through the terrain (take into account that first and last cell are half traveled )
    * Create an interface: IDangerous
        o float GetProbabilityDamage();
    * Create inherited classes from Cell class that implement this interface
    * Loop through a path and compute the total probability of damage

    Test each function when you type it 
*/

    // TO AUTOFORMAT CODE HIT THIS CMD SEQUENCE: "CTRL+K -> CTRL+D"
namespace ClassroomWeek2 {
    public class Program {

        public static readonly int SEED = 2308;
        public static Random DICE = new Random(SEED);

        public static void Main(string[] args) {
            
            // These while statements are necessary since there can't be
            // a [0,X] nor [X,0] nor [0,0] 2D array.
            int t_width;
            do {
                t_width = DICE.Next(Terrain.MIN_VALUE, Terrain.MAX_VALUE_X);
            } while (t_width == 0);

            int t_height;
            do {
                t_height = DICE.Next(Terrain.MIN_VALUE, Terrain.MAX_VALUE_Y);
            } while (t_height == 0);



            // Instantiate the terrain.
            Terrain my_terrain = new Terrain(t_width, t_height);

            // Instantiate the position
            int randomPosX = DICE.Next(Terrain.MIN_VALUE, my_terrain.GetWidth());
            int randomPosY = DICE.Next(Terrain.MIN_VALUE, my_terrain.GetHeight());
            Position starting_position = new Position(randomPosX, randomPosY);

            // Instantiate a Path. 
            Path my_path = new Path(my_terrain, starting_position);

            // Move a bit
            my_path.Move(Path.DIRECTION.DOWN);
            my_path.Move(Path.DIRECTION.DOWN);
            my_path.Move(Path.DIRECTION.LEFT);
            my_path.Move(Path.DIRECTION.LEFT);
            my_path.Move(Path.DIRECTION.LEFT);
            my_path.Move(Path.DIRECTION.LEFT);
            my_path.Move(Path.DIRECTION.UP);
            my_path.Move(Path.DIRECTION.UP);
            my_path.Move(Path.DIRECTION.UP);
            my_path.Move(Path.DIRECTION.UP);
            my_path.Move(Path.DIRECTION.RIGHT);

            // Show the results.
            Console.WriteLine("\n\n");
            Console.WriteLine(my_terrain.ToString());
            Console.WriteLine(my_path.ToString());
            Console.WriteLine("Path traveling cost: " + my_path.GetCost());
            Console.WriteLine("Probability of Damage: " + CalculateProbability(my_path));


            // This line prevents the console from exiting when the program is done...
            Console.WriteLine("\n");
            Console.Write("Press any key to exit the program...");
            Console.ReadKey();
        }

        // Static because this method is called in MAIN.
        private static float CalculateProbability(Path given) {

            float result = 0;
            foreach (Position pos in given) {
                IDangerous cell = given.PathOwner.GetCell(pos.X, pos.Y) as IDangerous;
                if (cell != null) result += cell.GetProbabilityDamage();
            }
            return result;
        }
    }
}
