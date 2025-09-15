using System.Collections.Generic;
using Core.Pieces;
using UnityEngine;
using UnityEngine.UIElements;

public class KingPiece : ILogic
{
    public List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces, Board board, PieceBase piece)
    {
        int y = piece.Position.y;
        int x = piece.Position.x;
        var moves = new List<Vector2Int>();

        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                Vector2Int movePos = new Vector2Int(i, j);
                if (i < 0 || i > 7 || j < 0 || j > 7) continue;
                if (!board.IsEmpty(pieces, movePos))
                    if (pieces[movePos].Color == piece.Color)
                        continue;
                moves.Add(new Vector2Int(i, j));
            }
        }

        return moves;
    }
}