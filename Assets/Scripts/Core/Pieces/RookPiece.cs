using System.Collections.Generic;
using UnityEngine;

public class RookPiece : PieceBase
{
    public RookPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces)
    {
        int y = Position.x;
        int x = Position.y;
        Vector2Int movePos;

        var moves = new List<Vector2Int>();

        // left
        for (int i = x + 1; i < 8; i++)
        {
            movePos = new Vector2Int(i, y);
            if ((pieces[movePos] != null && pieces[movePos].Color == Color) || pieces[movePos] is KingPiece) break;
            if (pieces[movePos] != null && pieces[movePos].Color != Color)
            {
                moves.Add(new Vector2Int(i, y));
                break;
            }

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }

        // right
        for (int i = x - 1; i >= 0; i--)
        {
            movePos = new Vector2Int(i, y);
            if ((pieces[movePos] != null && pieces[movePos].Color == Color) || pieces[movePos] is KingPiece) break;
            if (pieces[movePos] != null && pieces[movePos].Color != Color)
            {
                moves.Add(new Vector2Int(i, y));
                break;
            }

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }

        // up
        for (int j = y + 1; j < 8; j++)
        {
            movePos = new Vector2Int(x, j);
            if ((pieces[movePos] != null && pieces[movePos].Color == Color) || pieces[movePos] is KingPiece) break;
            if (pieces[movePos] != null && pieces[movePos].Color != Color)
            {
                moves.Add(new Vector2Int(x, j));
                break;
            }

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }

        // down
        for (int j = y - 1; j >= 0; j--)
        {
            movePos = new Vector2Int(x, j);
            if ((pieces[movePos] != null && pieces[movePos].Color == Color) || pieces[movePos] is KingPiece) break;
            if (pieces[movePos] != null && pieces[movePos].Color != Color)
            {
                moves.Add(new Vector2Int(x, j));
                break;
            }

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }


        return moves;
    }
}