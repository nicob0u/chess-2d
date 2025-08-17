using UnityEngine;
using System.Collections.Generic;

public class RookPiece : PieceBase
{
    public RookPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
        var moves = new List<Vector2Int>();

        // left
        for (int i = x + 1; i < 8; i++)
        {
            if (board[i, y] != null) break;

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }

        // right
        for (int i = x - 1; i >= 0; i--)
        {
            if (board[i, y] != null) break;

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }

        // up
        for (int j = y + 1; j < 8; j++)
        {
            if (board[x, j] != null) break;

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }

        // down
        for (int j = y - 1; j >= 0; j--)
        {
            if (board[x, j] != null) break;

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }


        return moves;
    }
}