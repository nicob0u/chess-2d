using UnityEngine;
using System.Collections.Generic;

public class QueenPiece : PieceBase
{
    public QueenPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
        var moves = new List<Vector2Int>();
        // up right
        for (int i = x + 1; i < 8; i++)
        {
            for (int j = y + 1; j < 8; j++)

            {
                if (board[i, j] != null && board[i, j].Color != Color && !(board[i, j] is KingPiece))
                {
                    moves.Add(new Vector2Int(i, j));
                    break;
                }

                if (board[i, j] != null && board[i, j].Color == Color)
                    break;

                moves.Add(new Vector2Int(i, j));
            }
        }

        for (int i = x + 1; i < 8; i++)
        {
            for (int j = y - 1; j >= 0; j--)

            {
                if (board[i, j] != null && board[i, j].Color != Color && !(board[i, j] is KingPiece))
                {
                    moves.Add(new Vector2Int(i, j));
                    break;
                }

                if (board[i, j] != null && board[i, j].Color == Color)
                    break;
                moves.Add(new Vector2Int(i, j));
            }
        }

        for (int i = x - 1; i >= 0; i--)
        {
            for (int j = y - 1; j >= 0; j--)

            {
                if (board[i, j] != null && board[i, j].Color != Color && !(board[i, j] is KingPiece))
                {
                    moves.Add(new Vector2Int(i, j));
                    break;
                }

                if (board[i, j] != null && board[i, j].Color == Color)
                    break;
                moves.Add(new Vector2Int(i, j));
            }
        }

        for (int i = x - 1; i >= 0; i--)
        {
            for (int j = y + 1; j < 8; j++)

            {
                if (board[i, j] != null && board[i, j].Color != Color && !(board[i, j] is KingPiece))
                {
                    moves.Add(new Vector2Int(i, j));
                    break;
                }

                if (board[i, j] != null && board[i, j].Color == Color)
                    break;
                moves.Add(new Vector2Int(i, j));
            }
        }


        // straight paths

        for (int i = x + 1; i < 8; i++)
        {
            if (board[i, y] != null && board[i, y].Color != Color && !(board[i, y] is KingPiece))
            {
                moves.Add(new Vector2Int(i, y));
                break;
            }

            if (board[i, y] != null && board[i, y].Color == Color)
                break;

            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }


        for (int i = x - 1; i >= 0; i--)
        {
            if (board[i, y] != null && board[i, y].Color != Color && !(board[i, y] is KingPiece))
            {
                moves.Add(new Vector2Int(i, y));
                break;
            }

            if (board[i, y] != null && board[i, y].Color == Color)
                break;


            var allowedMove = new Vector2Int(i, y);
            moves.Add(allowedMove);
        }


        for (int j = y + 1; j < 8; j++)
        {
            if (board[x, j] != null && board[x, j].Color != Color && !(board[x, j] is KingPiece))
            {
                moves.Add(new Vector2Int(x, j));
                break;
            }

            if (board[x, j] != null && board[x, j].Color == Color)
                break;

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }


        for (int j = y - 1; j >= 0; j--)
        {
            if (board[x, j] != null && board[x, j].Color != Color && !(board[x, j] is KingPiece))
            {
                moves.Add(new Vector2Int(x, j));
                break;
            }

            if (board[x, j] != null && board[x, j].Color == Color)
                break;

            var allowedMove = new Vector2Int(x, j);
            moves.Add(allowedMove);
        }

        return moves;
    }
}