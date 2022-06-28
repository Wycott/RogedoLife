using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;
using Rogedo.LifeEngine.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Rogedo.LifeEngine.Rig
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var flag = args[0];

                switch (flag)
                {
                    case "-r":
                        Runner();
                        break;
                    case "-d":
                        Demo();
                        break;
                    case "-f":
                        var dimensions = Convert.ToInt32(args[1]);
                        Finder(dimensions);
                        break;
                }
            }
        }

        static void Demo()
        {
            while (true)
            {
                Runner();
                System.Threading.Thread.Sleep(5000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        static void Finder(int dimension)
        {
            const int BreakAt = 500;

            var bestGenerations = 0;
            var runs = 0;

            var sw = new Stopwatch();
            sw.Start();

            Console.Clear();

            while (true)
            {
                GetRandomArena(dimension, out var gameArena, out var dataPoints);

                var breakOut = false;

                while (gameArena.GetPopulation() > 0 && !gameArena.Repeating && !breakOut)
                {
                    var currentTot = gameArena.GetPopulation();
                    gameArena.MakeNextGeneration();
                    var newTot = gameArena.GetPopulation();

                    if (currentTot == newTot || gameArena.GetGeneration() == BreakAt)
                        breakOut = true;
                }

                runs++;

                var generations = gameArena.GetGeneration();

                if (generations > bestGenerations && !breakOut)
                {
                    bestGenerations = generations;
                    Console.WriteLine($"Best generation: {generations} after {runs} runs, dimension: {dimension}, cells: {dataPoints.Count}");
                    DumpDataPoints(dataPoints);
                }

                if (runs % 100000 == 0)
                {
                    Console.WriteLine($"Runs: {runs}, Elapsed: {sw.ElapsedMilliseconds / 1000 / 60} mins");
                    return;
                }
            }
        }

        private static void DumpDataPoints(List<Point> dataPoints)
        {
            foreach (var p in dataPoints)
            {
                Console.Write($"{p.X},{p.Y} ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static void GetRandomArena(int dimension, out IArena gameArena, out List<Point> dataPoints)
        {
            gameArena = new Arena();
            gameArena.Initialise(dimension);

            dataPoints = new RandomArenaGenerator().Execute(dimension, 10);

            foreach (var dataPoint in dataPoints)
            {
                gameArena.Seed(dataPoint.X, dataPoint.Y);
            }

            gameArena.Pad();
        }

        static void Runner()
        {
            var dimension = 4;

            var gameArena = InitialiseArena(dimension);

            Console.Clear();

            Console.CursorVisible = false;
            while (gameArena.GetPopulation() > 0 && !gameArena.Repeating)
            {
                PrintArena(gameArena, gameArena.CurrentDimension);
                gameArena.MakeNextGeneration();
            }
            Console.CursorVisible = true;
        }

        private static IArena InitialiseArena(int dimension)
        {
            IArena gameArena = new Arena();

            gameArena.Initialise(dimension);
            gameArena.Seed(1, 1);
            gameArena.Seed(3, 0);
            gameArena.Seed(2, 3);
            gameArena.Seed(3, 2);
            gameArena.Seed(0, 1);
            gameArena.Seed(3, 1);
            gameArena.Seed(2, 2);
            gameArena.Pad();

            return gameArena;
        }

        static void PrintArena(IArena arena, int dimension)
        {
            var defaultColour = Console.ForegroundColor;

            if (arena.CurrentDimension <= 50)
            {
                var currentCell = 0;
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
                System.Threading.Thread.Sleep(100);
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
    }
}
