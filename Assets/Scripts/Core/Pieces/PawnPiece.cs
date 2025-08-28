using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnPiece : PieceBase
{
    public bool IsEnPassantTarget = false;

    public PawnPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces)
    {
        var moves = new List<Vector2Int>();
        int y = Position.y;
        int x = Position.x;

        Vector2Int movePos;

        if (Color == PieceColor.White)
        {
            if (y == 1)
            {
                for (int j = y + 1; j < y + 3; j++)
                {
                    movePos = new Vector2Int(x, j);
                    if (pieces[movePos] != null) break;

                    if (pieces[movePos] == null)
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = true;
                    }
                }
            }
            else

            {
                for (int j = y + 1; j < y + 2; j++)
                {
                    movePos = new Vector2Int(x, j);
                    if (pieces[movePos] == null)
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = false;
                    }
                }
            }

            movePos = new Vector2Int(x - 1, y + 1);
            if (x - 1 >= 0 && y + 1 < 8 && pieces[movePos] != null &&
                pieces[movePos].Color == PieceColor.Black &&
                !(pieces.TryGetValue(movePos, out PieceBase piece) is KingPiece))
            {
                moves.Add(new Vector2Int(x - 1, y + 1));
                IsEnPassantTarget = false;
            }

            movePos = new Vector2Int(x - 1, y - 1);
            if (x + 1 < 8 && y + 1 < 8 && pieces[movePos] != null &&
                pieces[movePos].Color == PieceColor.Black &&
                !(pieces[movePos] is KingPiece))
            {
                moves.Add(new Vector2Int(x + 1, y + 1));
                IsEnPassantTarget = false;
            }
        }


        else if (Color == PieceColor.Black)
        {
            if (y == 6)
            {
                for (int j = y - 1; j > y - 3; j--)
                {
                    movePos = new Vector2Int(x, j);

                    if (pieces[movePos] != null) break;

                    if (pieces[movePos] == null)
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = true;
                    }
                }
            }
            else
            {
                for (int j = y - 1; j > y - 2; j--)
                {
                    movePos = new Vector2Int(x, j);
                    
                    if (pieces[movePos] == null)
                    {
                        var allowedMove = new Vector2Int(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = false;
                    }
                }
            }

            movePos = new Vector2Int(x - 1, y - 1);
            if (x - 1 >= 0 && y - 1 >= 0 && pieces[movePos] != null &&
                pieces[movePos].Color == PieceColor.White && !(pieces[movePos] is KingPiece))
            {
                moves.Add(new Vector2Int(x - 1, y - 1));
                IsEnPassantTarget = false;
            }

            
            movePos = new Vector2Int(x + 1, y - 1);
            if (x + 1 < 8 && y - 1 >= 0 && pieces[movePos] != null &&
                pieces[movePos].Color == PieceColor.White && !(pieces[movePos] is KingPiece))
            {
                moves.Add(new Vector2Int(x + 1, y - 1));
                IsEnPassantTarget = false;
            }
        }

        return moves;
    }
}