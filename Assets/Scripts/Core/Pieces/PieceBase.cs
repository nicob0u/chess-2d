using System;
using System.Collections.Generic;
using Core.Pieces;
using UnityEngine;

public enum PieceColor
{
    White,
    Black
}


public struct PieceBase
{
    public int PieceId { get; set; }
    public PieceColor Color { get; set; }
    public bool IsCaptured { get; set; }
    public Vector2Int Position { get; set; }

    private readonly ILogic _logic;

    public List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces, Board board)
    => _logic.GetMoves(pieces, board, this);

    public Type GetLogicType() => _logic.GetType();
    public PieceBase(int id, PieceColor color, Vector2Int pos, ILogic logic)
    {
        Color = color;
        PieceId = id;
        IsCaptured = false;
        Position = pos;
        _logic = logic;
    }
    
    
}