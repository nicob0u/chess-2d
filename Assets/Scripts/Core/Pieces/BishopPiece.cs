using System.Collections.Generic;
using UnityEngine;

public class BishopPiece : PieceBase
{
    public BishopPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces)
    {
        int y = Position.y;
        int x = Position.x;
        Vector2Int movePos;
        var moves = new List<Vector2Int>();

        // right up
        int i = x + 1;
        int j = y + 1;

        while (i < 8 && j < 8)
        {
            movePos = new Vector2Int(i, j);

            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece) break;
                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }

            moves.Add(movePos);
            i++;
            j++;
        }

        // right down 
        i = x + 1;
        j = y - 1;
        while (i < 8 && j >= 0)
        {
            movePos = new Vector2Int(i, j);

            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece) break;
                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }

            moves.Add(movePos);
            i++;
            j--;
        }

        // left up
        i = x - 1;
        j = y + 1;
        while (i >= 0 && j < 8)
        {
            movePos = new Vector2Int(i, j);

            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece) break;
                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }

            moves.Add(movePos);
            i--;
            j++;
        }

        // left down
        i = x - 1;
        j = y - 1;
        while (i >= 0 && j >= 0)
        {
            movePos = new Vector2Int(i, j);

            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece) break;
                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }

            moves.Add(movePos);
            i--;
            j--;
        }

        return moves;
    }
}