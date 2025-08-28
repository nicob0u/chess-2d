using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : PieceBase
{
    public KnightPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces)
    {
        int y = Position.y;
        int x = Position.x;

        Vector2Int movePos;

        var moves = new List<Vector2Int>();

        for (int i = x - 2; i < x + 3; i++)
        {
            for (int j = y - 2; j < y + 3; j++)
            {
                movePos = new Vector2Int(i, j);
                if (i < 0 || i > 7 || j < 0 || j > 7) continue;
                if ((pieces[movePos] != null && pieces[movePos].Color == Color) || pieces[movePos] is KingPiece)
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