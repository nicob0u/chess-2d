using UnityEngine;
using System.Collections.Generic;

public enum PieceColor
{
    White,
    Black
}

public abstract class PieceBase
{
    public PieceColor Color { get;  set; }
    public bool IsCaptured { get; set; }
    public abstract List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y);
    public PieceBase(PieceColor color)
    {
        Color = color;
    }

}