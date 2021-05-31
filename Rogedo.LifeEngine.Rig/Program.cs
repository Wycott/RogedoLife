using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;
using Rogedo.LifeEngine.Tools;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Rogedo.LifeEngine.Rig
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimensions = 0;
            if (args.Length == 1)
                dimensions = Convert.ToInt32(args[0]);
            //Runner();
            //Finder(dimensions);
            Demo();
        }

        static void Demo()
        {
            while (true)
            {
                Runner();
                System.Threading.Thread.Sleep(5000);
            }
        }

        static void Finder(int dimension)
        {
            const int breakAt = 500;
            int bestGenerations = 0;
            //int dimension = 4;
            int runs = 0;
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Console.Clear();
            while (true)
            {
                IArena gameArena = new Arena();
                gameArena.Initialise(dimension);

                var dataPoints = new RandomArenaGenerator().Execute(dimension, 10);

                foreach (var dataPoint in dataPoints)
                {
                    gameArena.Seed(dataPoint.X, dataPoint.Y);
                }

                gameArena.Pad();

                bool bail = false;
                bool breakOut = false;
                while (gameArena.GetPopulation() > 0 && !gameArena.Repeating && !bail && !breakOut
                    )
                {
                    int currentTot = gameArena.GetPopulation();
                    gameArena.MakeNextGeneration();
                    if (gameArena.GetGeneration() == breakAt)
                    {
                        //Console.WriteLine("Bailing...");
                        breakOut = true;
                    }
                    int newTot = gameArena.GetPopulation();

                    if (currentTot == newTot)
                      bail = true;                    
                }

                runs++;

                if (!breakOut)
                {
                    int generations = gameArena.GetGeneration();
                    if (generations > bestGenerations)
                    {
                        bestGenerations = generations;
                        Console.WriteLine($"Best generation: {generations} after {runs} runs, dimension: {dimension}, cells: {dataPoints.Count}");
                        foreach (var p in dataPoints)
                        {
                            Console.Write($"{p.X},{p.Y} ");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }

                //if (runs % 10000 == 0)
                //{
                //    Console.WriteLine($"Runs: {runs}, Elapsed: {sw.ElapsedMilliseconds / 1000 / 60} mins");
                //}

                if (runs % 100000 == 0)
                {
                    Console.WriteLine($"Runs: {runs}, Elapsed: {sw.ElapsedMilliseconds/1000/60} mins");
                    return;
                }
            }
        }

        static void Runner()
        {
            int dimension = 4;

            IArena gameArena = new Arena();
            gameArena.Initialise(dimension);

            // R pentomino
            //gameArena.Seed(1, 0);
            //gameArena.Seed(1, 1);
            //gameArena.Seed(1, 2);
            //gameArena.Seed(0, 1);
            //gameArena.Seed(2, 0);

            // S pentomino
            //gameArena.Seed(2, 1);
            //gameArena.Seed(3, 1);
            //gameArena.Seed(0, 2);
            //gameArena.Seed(1, 2);
            //gameArena.Seed(2, 2);

            // U pentomino
            //gameArena.Seed(0, 0);
            //gameArena.Seed(0, 1);
            //gameArena.Seed(1, 1);
            //gameArena.Seed(2, 0);
            //gameArena.Seed(2, 1);


            // Hexomino 1
            //gameArena.Seed(0, 0);
            //gameArena.Seed(1, 0);
            //gameArena.Seed(2, 0);
            //gameArena.Seed(3, 0);
            //gameArena.Seed(4, 0);
            //gameArena.Seed(4, 1);

            // Hexomino 2
            //gameArena.Seed(0, 0);
            //gameArena.Seed(0, 1);
            //gameArena.Seed(1, 1);
            //gameArena.Seed(2, 1);
            //gameArena.Seed(3, 1);
            //gameArena.Seed(3, 2);

            // Long runner
            //gameArena.Seed(6, 0);
            //gameArena.Seed(0, 1);
            //gameArena.Seed(1, 1);


            //gameArena.Seed(1, 2);
            //gameArena.Seed(5, 2);
            //gameArena.Seed(6, 2);
            //gameArena.Seed(7, 2);

            //Random

            //for (var c = 0; c < 9; c++)
            //{
            //    var point = GetNextPoint();
            //    gameArena.Seed(point.X, point.Y);
            //}

            // Found by me 1
            gameArena.Seed(1, 1);
            gameArena.Seed(3, 0);
            gameArena.Seed(2, 3);
            gameArena.Seed(3, 2);
            gameArena.Seed(0, 1);
            gameArena.Seed(3, 1);
            gameArena.Seed(2, 2);

            //gameArena.InitialiseRandomly(dimension);

            gameArena.Pad();

            Console.Clear();
            Console.CursorVisible = false;
            bool bail = false;
            while (gameArena.GetPopulation() > 0 && !gameArena.Repeating //&& !bail
                )
            {
                int currentTot = gameArena.GetPopulation();
                //Console.Clear();
                PrintArena(gameArena, gameArena.CurrentDimension);
                gameArena.MakeNextGeneration();
                //System.Threading.Thread.Sleep(10000);
                int newTot = gameArena.GetPopulation();

                if (currentTot == newTot)
                  bail = true;
            }

            Console.CursorVisible = true;
        }

        static void PrintArena(IArena arena, int dimension)
        {
            ConsoleColor defaultColour = Console.ForegroundColor;

            if (arena.CurrentDimension <= 50)
            {
                int currentCell = 0;
                int x;
                int y;
                foreach (var c in arena.ArenaCells)
                {
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
                Console.SetCursorPosition(0, dimension);
                Console.WriteLine($"Generation: {arena.GetGeneration()} ");
                Console.SetCursorPosition(0, dimension + 1);
                Console.WriteLine($"Population: {arena.GetPopulation()} ");
                Console.SetCursorPosition(0, dimension + 2);
                Console.WriteLine($"Dimension: {arena.CurrentDimension} ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"Generation: {arena.GetGeneration()}");
                Console.WriteLine($"Population: {arena.GetPopulation()} ");
                Console.WriteLine($"Dimension: {arena.CurrentDimension} ");
                Console.WriteLine();
            }


            Console.ForegroundColor = defaultColour;
        }

        private static Point GetNextPoint()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            guid = guid.Replace("a", "");
            guid = guid.Replace("b", "");
            guid = guid.Replace("c", "");
            guid = guid.Replace("d", "");
            guid = guid.Replace("e", "");
            guid = guid.Replace("f", "");
            int x = Convert.ToInt32(guid.Substring(0, 1));
            int y = Convert.ToInt32(guid.Substring(1, 1));
            return new Point(x, y);
        }

    }
}
