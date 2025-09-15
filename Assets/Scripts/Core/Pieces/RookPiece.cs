using System.Collections.Generic;
using Core.Pieces;
using UnityEngine;

public class RookPiece : ILogic
{
    public List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces, Board board, PieceBase piece)
    {
        int y = piece.Position.y;
        int x = piece.Position.x;
        Vector2Int movePos;

        var moves = new List<Vector2Int>();

        // right
        for (int i = x + 1; i < 8; i++)
        {
            movePos = new Vector2Int(i, y);
            if (!board.IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == piece.Color)
                    break;

                moves.Add(new Vector2Int(i, y));
                break;
            }

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }

        // left
        for (int i = x - 1; i >= 0; i--)
        {
            movePos = new Vector2Int(i, y);
            if (!board.IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == piece.Color)
                    break;

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
            if (!board.IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == piece.Color)
                    break;

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
            if (!board.IsEmpty(pieces, movePos))
            {
                if (pieces[movePos].Color == piece.Color)
                    break;

                moves.Add(new Vector2Int(x, j));
                break;
            }

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }


        return moves;
    }
}