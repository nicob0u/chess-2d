using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    White,
    Black
}


public abstract class PieceBase
{
    public PieceColor Color { get; set; }
    public bool IsCaptured { get; set; }
    public Vector2Int Position { get; set; }
    public abstract List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces);

    
    public PieceBase(PieceColor color)
    {
        Color = color;
    }

    protected bool IsEmpty(Dictionary<Vector2Int, PieceBase> pieces, Vector2Int movePos)
    {
        return (!pieces.TryGetValue(movePos, out var piece) || piece == null);
    }
}