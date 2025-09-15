using System.Collections.Generic;
using System.Linq;
using Core.Pieces;
using UnityEngine;
using UnityEngine.UIElements;

public class PawnPiece : ILogic
{
    public List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces, Board board, PieceBase piece)
    {
        var moves = new List<Vector2Int>();
        int y = piece.Position.y;
        int x = piece.Position.x;

        Vector2Int movePos;
        
        
        if (piece.Color == PieceColor.White)
        {
            if (y == 1)
            {
                for (int j = y + 1; j < y + 3; j++)
                {
                    movePos = new Vector2Int(x, j);
                    if (!board.IsEmpty(pieces, movePos)) break;

                    if (board.IsEmpty(pieces, movePos))
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }
            else

            {
                for (int j = y + 1; j < y + 2; j++)
                {
                    movePos = new Vector2Int(x, j);
                    if (board.IsEmpty(pieces, movePos))
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }

            movePos = new Vector2Int(x - 1, y + 1);
            if (x - 1 >= 0 && y + 1 < 8 &&
                !board.IsEmpty(pieces, movePos) &&
                pieces[movePos].Color == PieceColor.Black)
            {
                moves.Add(new Vector2Int(x - 1, y + 1));
            }

            movePos = new Vector2Int(x + 1, y + 1);
            if (x + 1 < 8 && y + 1 < 8 && !board.IsEmpty(pieces, movePos) &&
                pieces[movePos].Color == PieceColor.Black)
            {
                moves.Add(new Vector2Int(x + 1, y + 1));
            }
        }


        else if (piece.Color == PieceColor.Black)
        {
            if (y == 6)
            {
                for (int j = y - 1; j > y - 3; j--)
                {
                    movePos = new Vector2Int(x, j);

                    if (!board.IsEmpty(pieces, movePos)) break;

                    if (board.IsEmpty(pieces, movePos))
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }
            else
            {
                for (int j = y - 1; j > y - 2; j--)
                {
                    movePos = new Vector2Int(x, j);

                    if (board.IsEmpty(pieces, movePos))
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                    }
                }
            }

            movePos = new Vector2Int(x - 1, y - 1);
            if (x - 1 >= 0 && y - 1 >= 0 &&
                !board.IsEmpty(pieces, movePos) && pieces[movePos].Color == PieceColor.White)
            {
                moves.Add(new Vector2Int(x - 1, y - 1));
            }


            movePos = new Vector2Int(x + 1, y - 1);
            if (x + 1 < 8 && y - 1 >= 0 && !board.IsEmpty(pieces, movePos) &&
                pieces[movePos].Color == PieceColor.White)
            {
                moves.Add(new Vector2Int(x + 1, y - 1));
            }
        }

        return moves;
    }
}