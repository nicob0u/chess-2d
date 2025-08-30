using System.Collections.Generic;
using UnityEngine;

public class KingPiece : PieceBase
{
    public KingPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces)
    {
        int y = Position.y;
        int x = Position.x;
        var moves = new List<Vector2Int>();
        Vector2Int movePos = new Vector2Int(x, y);

        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                if (i < 0 || i > 7 || j < 0 || j > 7) continue;
                if (!IsEmpty(pieces, movePos) &&
                    (pieces[movePos] is KingPiece || pieces[movePos].Color == Color)) continue;
                moves.Add(new Vector2Int(i, j));
            }
        }

        return moves;
    }
}