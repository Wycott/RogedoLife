using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;
using Rogedo.LifeEngine.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using static System.Console;

namespace Rogedo.LifeEngine.Rig;

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

    private static void Demo()
    {
        while (true)
        {
            Runner();
            System.Threading.Thread.Sleep(5000);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static void Finder(int dimension)
    {
        const int BreakAt = 500;

        var bestGenerations = 0;
        var runs = 0;

        var sw = new Stopwatch();
        sw.Start();

        Clear();

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
                {
                    breakOut = true;
                }
            }

            runs++;

            var generations = gameArena.GetGeneration();

            if (generations > bestGenerations && !breakOut)
            {
                bestGenerations = generations;
                WriteLine($"Best generation: {generations} after {runs} runs, dimension: {dimension}, cells: {dataPoints.Count}");
                DumpDataPoints(dataPoints);
            }

            if (runs % 100000 == 0)
            {
                WriteLine($"Runs: {runs}, Elapsed: {sw.ElapsedMilliseconds / 1000 / 60} mins");
                return;
            }
        }
    }

    private static void DumpDataPoints(List<Point> dataPoints)
    {
        foreach (var p in dataPoints)
        {
            Write($"{p.X},{p.Y} ");
        }

        WriteLine();
        WriteLine();
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

    private static void Runner()
    {
        var dimension = 4;

        var gameArena = InitialiseArena(dimension);

        Clear();

        CursorVisible = false;
        while (gameArena.GetPopulation() > 0 && !gameArena.Repeating)
        {
            PrintArena(gameArena, gameArena.CurrentDimension);
            gameArena.MakeNextGeneration();
        }
        CursorVisible = true;
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

    private static void PrintArena(IArena arena, int dimension)
    {
        var defaultColour = ForegroundColor;

        if (arena.CurrentDimension <= 50)
        {
            DisplayCells(arena, dimension, defaultColour);
            DisplayDynamicStatistics(arena, dimension);
        }
        else
        {
            DisplayStaticStatistics(arena);
        }

        ForegroundColor = defaultColour;
    }

    private static void DisplayCells(IArena arena, int dimension, ConsoleColor defaultColour)
    {
        var currentCell = 0;

        foreach (var c in arena.ArenaCells)
        {
            var x = (currentCell % dimension) * 2;
            var y = currentCell / dimension;

            SetCursorPosition(x, y);

            if (c.Generation == Types.CellGeneration.Dead)
            {
                ForegroundColor = defaultColour;
                Write(" .");
            }
            else
            {
                ForegroundColor = ConsoleColor.Yellow;
                Write(" O");
                ForegroundColor = defaultColour;
            }

            currentCell++;
        }
    }

    private static void DisplayDynamicStatistics(IArena arena, int dimension)
    {
        ForegroundColor = ConsoleColor.White;
        SetCursorPosition(0, dimension);
        WriteLine($"Generation: {arena.GetGeneration()} ");
        SetCursorPosition(0, dimension + 1);
        WriteLine($"Population: {arena.GetPopulation()} ");
        SetCursorPosition(0, dimension + 2);
        WriteLine($"Dimension: {arena.CurrentDimension} ");
        System.Threading.Thread.Sleep(100);
    }

    private static void DisplayStaticStatistics(IArena arena)
    {
        ForegroundColor = ConsoleColor.White;

        WriteLine($"Generation: {arena.GetGeneration()}");
        WriteLine($"Population: {arena.GetPopulation()} ");
        WriteLine($"Dimension: {arena.CurrentDimension} ");
        WriteLine();
    }
}