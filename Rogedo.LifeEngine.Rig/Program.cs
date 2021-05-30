using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;
using System;

namespace Rogedo.LifeEngine.Rig
{
    class Program
    {
        static void Main(string[] args)
        {
            const int dimension = 50;

            //IArena gameArena = new Arena();
            //gameArena.Initialise(dimension);
            //gameArena.Seed(1, 0);
            //gameArena.Seed(2, 1);
            //gameArena.Seed(0, 2);
            //gameArena.Seed(1, 2);
            //gameArena.Seed(2, 2);

            IArena gameArena = new Arena();
            gameArena.InitialiseRandomly(dimension);

            Console.Clear();
            Console.CursorVisible = false;
            while (gameArena.GetPopulation() > 0 && !gameArena.Repeating)
            {
                //Console.Clear();
                PrintArena(gameArena, dimension);
                gameArena.MakeNextGeneration();
                //System.Threading.Thread.Sleep(250);                
            }
            Console.CursorVisible = true;
        }

        static void PrintArena(IArena arena, int dimension)
        {
            ConsoleColor defaultColour = Console.ForegroundColor;

            int currentCell = 0;
            int x = 0;
            int y = 0;
            foreach (var c in arena.ArenaCells)
            {
                //currentCell++;
                //if (c.Generation == Interfaces.Types.CellGeneration.Dead)
                //{
                //    Console.ForegroundColor = defaultColour;
                //    Console.Write(" .");
                //}
                //else
                //{
                //    Console.ForegroundColor = ConsoleColor.Yellow;
                //    Console.Write(" o");
                //    Console.ForegroundColor = defaultColour;
                //}

                //if (currentCell % dimension == 0)
                //    Console.WriteLine();                

                x = (currentCell % dimension) * 2;
                y = currentCell / dimension;
                Console.SetCursorPosition(x, y);
                if (c.Generation == Interfaces.Types.CellGeneration.Dead)
                {
                    Console.ForegroundColor = defaultColour;
                    Console.Write(" .");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" O");
                    Console.ForegroundColor = defaultColour;
                }
                currentCell++;

            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, dimension );
            Console.WriteLine($"Generation: {arena.GetGeneration()}");
            Console.SetCursorPosition(0, dimension + 1);
            Console.WriteLine($"Population: {arena.GetPopulation()}         ");
            Console.ForegroundColor = defaultColour;
        }

    }
}
