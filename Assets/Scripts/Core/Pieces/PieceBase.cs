using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    White,
    Black
}

public struct Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    
}
public abstract class PieceBase
{
    public PieceColor Color { get; set; }
    public bool IsCaptured { get; set; }
    public Position Position { get; set; }
    public abstract List<Position> GetMoves(PieceBase[,] board);

    public PieceBase(PieceColor color)
    {
        Color = color;
    }
}