using UnityEngine;
using System.Collections.Generic;

public class KnightPiece : PieceBase
{
    public KnightPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
        var moves = new List<Vector2Int>();

        for (int i = x - 2; i < x + 3; i++)
        {
            for (int j = y - 2; j < y + 3; j++)
            {
                if (i < 0 || i > 7 || j < 0 || j > 7) continue;
                if ((board[i, j] != null && board[i, j].Color == Color) || board[i, j] is KingPiece)
                    continue;


                if (i == x - 1 || i == x + 1)
                {
                    if (j == y - 1 || j == y + 1 || j == y) continue;
                    moves.Add(new Vector2Int(i, j));
                }

                if (i == x - 2 || i == x + 2)
                {
                    if (j == y - 2 || j == y + 2 || j == y) continue;
                    moves.Add(new Vector2Int(i, j));
                }
            }
        }

        return moves;
    }
}