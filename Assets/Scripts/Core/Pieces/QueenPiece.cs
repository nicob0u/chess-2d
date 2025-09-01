using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : PieceBase
{
    public QueenPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces)
    {
        int y = Position.y;
        int x = Position.x;
        Vector2Int movePos;
        var moves = new List<Vector2Int>();

        // straight paths

        for (int i = x + 1; i < 8; i++)
        {
            movePos = new Vector2Int(i, y);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color != Color && !(pieces[movePos] is KingPiece))
                {
                    moves.Add(new Vector2Int(i, y));
                    break;
                }

                if (pieces[movePos].Color == Color)
                    break;
            }


            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }


        for (int i = x - 1; i >= 0; i--)
        {
            movePos = new Vector2Int(i, y);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color != Color && !(pieces[movePos] is KingPiece))
                {
                    moves.Add(new Vector2Int(i, y));
                    break;
                }

                if (pieces[movePos].Color == Color)
                    break;
            }

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }


        for (int j = y + 1; j < 8; j++)
        {
            movePos = new Vector2Int(x, j);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color != Color && !(pieces[movePos] is KingPiece))
                {
                    moves.Add(new Vector2Int(x, j));
                    break;
                }

                if (pieces[movePos].Color == Color)
                    break;
            }

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }


        for (int j = y - 1; j >= 0; j--)
        {
            movePos = new Vector2Int(x, j);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color != Color && !(pieces[movePos] is KingPiece))
                {
                    moves.Add(new Vector2Int(x, j));
                    break;
                }

                if (pieces[movePos].Color == Color)
                    break;
            }

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }

        // diagonal paths
        int k = x + 1;
        int l = y + 1;
        while (k < 8 && l < 8)
        {
            movePos = new Vector2Int(k, l);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece)
                    break;

                if (pieces[movePos].Color != Color)
                {
                    moves.Add(new Vector2Int(k, l));
                    break;
                }
            }


            moves.Add(new Vector2Int(k, l));
            k++;
            l++;
        }

        // right down 
        k = x + 1;
        l = y - 1;
        while (k < 8 && l >= 0)
        {
            movePos = new Vector2Int(k, l);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece)
                    break;

                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }
            else
                moves.Add(movePos);

            k++;
            l--;
        }

        // left up
        k = x - 1;
        l = y + 1;
        while (k >= 0 && l < 8)
        {
            movePos = new Vector2Int(k, l);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece)
                    break;

                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }
            else
                moves.Add(movePos);

            k--;
            l++;
        }

        // left down
        k = x - 1;
        l = y - 1;
        while (k >= 0 && l >= 0)
        {
            movePos = new Vector2Int(k, l);
            if (!IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == Color || pieces[movePos] is KingPiece)
                    break;

                if (pieces[movePos].Color != Color)
                {
                    moves.Add(movePos);
                    break;
                }
            }
            else
                moves.Add(movePos);

            k--;
            l--;
        }


        return moves;
    }
}