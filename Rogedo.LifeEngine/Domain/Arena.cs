using Rogedo.LifeEngine.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System;
using Rogedo.LifeEngine.Tools;
using Generation = Rogedo.LifeEngine.Types.CellGeneration;

namespace Rogedo.LifeEngine.Domain;

public class Arena : IArena
{
    private int Dimension { get; set; }
    private List<string> Signatures { get; }

    public List<ICell> DummyArenaCells { get; private set; }
    public List<ICell> ArenaCells { get; private set; }
    public int CellGeneration { get; private set; }

    public bool Repeating => CellGeneration != Signatures.Count;

    public int CurrentDimension => Dimension;

    public Arena()
    {
        ArenaCells = new List<ICell>();
        DummyArenaCells = new List<ICell>();
        Signatures = new List<string>();
    }

    public int GetArenaSize()
    {
        return ArenaCells.Count;
    }

    public void Initialise(int dimension)
    {
        Dimension = dimension;
        ArenaCells = new List<ICell>();

        for (var x = 0; x < dimension; x++)
        {
            for (var y = 0; y < dimension; y++)
            {
                var cell = new Cell();
                ArenaCells.Add(cell);
            }
        }
    }

    public void InitialiseRandomly(int dimension)
    {
        Dimension = dimension;
        ArenaCells = new List<ICell>();

        for (var x = 0; x < dimension; x++)
        {
            for (var y = 0; y < dimension; y++)
            {
                var cell = new Cell();

                if (PopulateCell())
                {
                    cell.SetGeneration(Generation.Current);
                }

                ArenaCells.Add(cell);
            }
        }
    }

    public void Pad()
    {
        CheckTopLeftPadding();
        CheckBottomRightPadding();
    }

    public string GetSignatureHash()
    {
        return Hash.GetHashString(GetSignature());
    }

    public string GetSignature()
    {
        var sb = new StringBuilder();

        foreach (var cell in ArenaCells)
        {
            var simpleCell = cell.Generation == Generation.Dead ? "0" : "1";
            sb.Append(simpleCell);
        }

        return sb.ToString();
    }

    public void Seed(int x, int y)
    {
        var index = GetIndex(x, y);
        ArenaCells[index].SetGeneration(Generation.Current);
    }

    public ICell GetCellAt(int x, int y)
    {
        if (x < 0 || x >= Dimension || y < 0 || y >= Dimension)
        {
            return new Cell();
        }

        return ArenaCells[GetIndex(x, y)];
    }

    public void MakeNextGeneration()
    {
        var dying = new List<Point>();

        for (var x = 0; x < Dimension; x++)
        {
            for (var y = 0; y < Dimension; y++)
            {
                var cellProcessed = false;
                var cell = GetCellAt(x, y);

                // TODO: Look at this
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                cellProcessed = CheckForSurvival(cellProcessed, x, y, cell);
                cellProcessed = CheckForBirth(cellProcessed, x, y, cell);
                CheckForDeath(dying, cellProcessed, x, y);
            }
        }

        SetGeneration();

        ClearTheDead(dying);

        CellGeneration++;

        StoreSignature();

        Pad();
    }

    public int GetPopulation()
    {
        return ArenaCells.Count(x => x.Generation == Generation.Current);
    }

    public int GetGeneration()
    {
        return CellGeneration + 1;
    }

    private static bool PopulateCell()
    {
        var wins = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7' };
        var guid = Guid.NewGuid().ToString();
        var candidate = Convert.ToChar(guid.Substring(0, 1));

        return wins.Contains(candidate);
    }

    private static void CheckForDeath(List<Point> dying, bool cellProcessed, int x, int y)
    {
        if (!cellProcessed)
        {
            var point = new Point(x, y);
            dying.Add(point);
        }
    }

    private bool CheckForBirth(bool cellProcessed, int x, int y, ICell cell)
    {
        if (cell.Generation == Generation.Dead) // Dead cell
        {
            var neighbourCount = GetLiveNeighbourCount(x, y);

            if (neighbourCount == 3)
            {
                cell.SetGeneration(Generation.Next);
                cellProcessed = true; // Born
            }
        }

        return cellProcessed;
    }

    private bool CheckForSurvival(bool cellProcessed, int x, int y, ICell cell)
    {
        if (cell.Generation == Generation.Current) // Live cell
        {
            var neighbourCount = GetLiveNeighbourCount(x, y);

            if (neighbourCount == 2 || neighbourCount == 3)
            {
                cellProcessed = true; // Survives
            }
        }

        return cellProcessed;
    }

    private void SetGeneration()
    {
        foreach (var cell in ArenaCells)
        {
            if (cell.Generation == Generation.Next)
            {
                cell.SetGeneration(Generation.Current);
            }
        }
    }

    private void ClearTheDead(List<Point> dying)
    {
        foreach (var p in dying)
        {
            GetCellAt(p.X, p.Y).SetGeneration(Generation.Dead);
        }
    }

    private void StoreSignature()
    {
        var signature = GetSignatureHash();

        if (!Signatures.Contains(signature))
        {
            Signatures.Add(signature);
        }
    }

    private int GetIndex(int x, int y)
    {
        return (x % Dimension) + (y * Dimension);
    }

    private int GetLiveNeighbourCount(int cx, int cy)
    {
        var liveTally = 0;

        for (var x = -1; x < 2; x++)
        {
            for (var y = -1; y < 2; y++)
            {
                var currentX = cx + x;
                var currentY = cy + y;

                if ((x != 0 || y != 0) && GetCellAt(currentX, currentY).Generation == Generation.Current)
                {
                    liveTally++;
                }
            }
        }

        return liveTally;
    }

    private void CheckTopLeftPadding()
    {
        var anyAtTop = false;
        var anyAtLeft = false;

        for (var x = 0; x < Dimension; x++)
        {
            var cell1 = GetCellAt(x, 0);

            if (cell1.Generation == Generation.Current)
            {
                anyAtTop = true;
                break;
            }
        }

        for (var x = 0; x < Dimension; x++)
        {
            var cell3 = GetCellAt(0, x);

            if (cell3.Generation == Generation.Current)
            {
                anyAtLeft = true;
                break;
            }
        }

        if (anyAtTop || anyAtLeft)
        {
            PadTopOrLeft();
        }
    }

    private void CheckBottomRightPadding()
    {
        var anyAtBottom = false;
        var anyAtRight = false;

        for (var x = 0; x < Dimension; x++)
        {
            var cell2 = GetCellAt(x, Dimension - 1);

            if (cell2.Generation == Generation.Current)
            {
                anyAtBottom = true;
                break;
            }
        }

        for (var x = 0; x < Dimension; x++)
        {
            var cell4 = GetCellAt(Dimension - 1, x);

            if (cell4.Generation == Generation.Current)
            {
                anyAtRight = true;
                break;
            }
        }

        if (anyAtBottom || anyAtRight)
        {
            PadBottomOrRight();
        }
    }

    private void PadTopOrLeft()
    {
        DummyArenaCells = new List<ICell>();

        foreach (var cell in ArenaCells)
        {
            DummyArenaCells.Add(cell);
        }

        ArenaCells = new List<ICell>();

        // Add top row of new size
        for (var x = 0; x <= Dimension; x++)
        {
            ArenaCells.Add(new Cell());
        }

        for (var y = 0; y < Dimension; y++)
        {
            ArenaCells.Add(new Cell());
            for (var x = 0; x < Dimension; x++)
            {
                ArenaCells.Add(DummyArenaCells[GetIndex(x, y)]);
            }
        }
        Dimension++;
    }

    private void PadBottomOrRight()
    {
        DummyArenaCells = new List<ICell>();

        foreach (var cell in ArenaCells)
        {
            DummyArenaCells.Add(cell);
        }

        ArenaCells = new List<ICell>();

        for (var y = 0; y < Dimension; y++)
        {
            for (var x = 0; x < Dimension; x++)
            {
                ArenaCells.Add(DummyArenaCells[GetIndex(x, y)]);
            }

            ArenaCells.Add(new Cell());
        }

        // Add bottom row of new size
        for (var x = 0; x <= Dimension; x++)
        {
            ArenaCells.Add(new Cell());
        }

        Dimension++;
    }
}