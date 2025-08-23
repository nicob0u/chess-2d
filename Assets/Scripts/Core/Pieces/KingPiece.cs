using UnityEngine;
using System.Collections.Generic;

public class KingPiece : PieceBase
{
    public KingPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
        var moves = new List<Vector2Int>();

        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                if (i < 0 || i > 7 || j < 0 || j > 7) continue;
                if (board[i,j] != null && (board[i, j] is KingPiece || board[i, j].Color == Color)) continue;
                moves.Add(new Vector2Int(i, j));
            }
        }

        return moves;
    }
}