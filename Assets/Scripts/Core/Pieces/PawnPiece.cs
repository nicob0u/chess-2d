using UnityEngine;
using System.Collections.Generic;

public class PawnPiece : PieceBase
{
    public PawnPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
        var moves = new List<Vector2Int>();

        if (Color == PieceColor.White)
        {
            if (y == 1)
            {
                for (int j = y + 1; j < y + 3; j++)
                {
                    if (board[x, j] == null)
                    {
                        if (board[x, j] != null) break;
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }
            else
            {
                for (int j = y + 1; j < y + 2; j++)
                {
                    if (board[x, j] == null)
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }

            if (x - 1 >= 0 && y + 1 < 8 && board[x - 1, y + 1] != null && board[x - 1, y + 1].Color == PieceColor.Black )
                moves.Add(new Vector2Int(x - 1, y + 1));
            if (x + 1 < 8 && y + 1 < 8 && board[x + 1, y + 1] != null && board[x + 1, y + 1].Color == PieceColor.Black )
                moves.Add(new Vector2Int(x + 1, y + 1));
        }


        else if (Color == PieceColor.Black)
        {
            if (y == 6)
            {
                for (int j = y - 1; j > y - 3; j--)
                {
                    if (board[x, j] == null)
                    {
                        if (board[x, j] != null) break;
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }
            else
            {
                for (int j = y - 1; j > y - 2; j--)
                {
                    if (board[x, j] == null)
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }

            if (x - 1 >= 0 && y - 1 >= 0 && board[x - 1, y - 1] != null)
                moves.Add(new Vector2Int(x - 1, y - 1));
            if (x + 1 < 8 && y - 1 >= 0  && board[x + 1, y - 1] != null)
                moves.Add(new Vector2Int(x + 1, y - 1));
        }


        return moves;
    }
    
    
    
    
}