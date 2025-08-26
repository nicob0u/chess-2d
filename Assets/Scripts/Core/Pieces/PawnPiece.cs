using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnPiece : PieceBase
{
    public bool IsEnPassantTarget = false;
    public List<Position> WhiteEnPassantTargets = new List<Position>();
    public List<Position> BlackEnPassantTargets = new List<Position>();

    public PawnPiece(PieceColor color) : base(color)
    {
    }

    public override List<Position> GetMoves(PieceBase[,] board)
    {
        var moves = new List<Position>();
        int y = Position.Y;
        int x = Position.X;


        if (Color == PieceColor.White)
        {
            if (y == 1)
            {
                for (int j = y + 1; j < y + 3; j++)
                {
                    if (board[x, j] != null) break;

                    if (board[x, j] == null)
                    {
                        var allowedMove = new Position(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = true;
                    }
                }
            }
            else
            {
                for (int j = y + 1; j < y + 2; j++)
                {
                    if (board[x, j] == null)
                    {
                        var allowedMove = new Position(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = false;
                    }
                }
            }

            if (x - 1 >= 0 && y + 1 < 8 && board[x - 1, y + 1] != null &&
                board[x - 1, y + 1].Color == PieceColor.Black &&
                !(board[x - 1, y + 1] is KingPiece))
            {
                moves.Add(new Position(x - 1, y + 1));
                IsEnPassantTarget = false;
            }

            if (x + 1 < 8 && y + 1 < 8 && board[x + 1, y + 1] != null &&
                board[x + 1, y + 1].Color == PieceColor.Black &&
                !(board[x + 1, y + 1] is KingPiece))
            {
                moves.Add(new Position(x + 1, y + 1));
                IsEnPassantTarget = false;
            }
        }


        else if (Color == PieceColor.Black)
        {
            if (y == 6)
            {
                for (int j = y - 1; j > y - 3; j--)
                {
                    if (board[x, j] != null) break;

                    if (board[x, j] == null)
                    {
                        var allowedMove = new Position(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = true;
                    }
                }
            }
            else
            {
                for (int j = y - 1; j > y - 2; j--)
                {
                    if (board[x, j] == null)
                    {
                        var allowedMove = new Position(x, j);
                        moves.Add(allowedMove);
                        IsEnPassantTarget = false;
                    }
                }
            }

            if (x - 1 >= 0 && y - 1 >= 0 && board[x - 1, y - 1] != null &&
                board[x - 1, y - 1].Color == PieceColor.White && !(board[x - 1, y - 1] is KingPiece))
            {
                moves.Add(new Position(x - 1, y - 1));
                IsEnPassantTarget = false;
            }

            if (x + 1 < 8 && y - 1 >= 0 && board[x + 1, y - 1] != null &&
                board[x + 1, y - 1].Color == PieceColor.White && !(board[x + 1, y - 1] is KingPiece))
            {
                moves.Add(new Position(x + 1, y - 1));
                IsEnPassantTarget = false;
            }
        }

        // GetEnPassantMoves(board[x, y]);
        // foreach (Position move in BlackEnPassantTargets)
        // {
        //     Debug.Log($"En passant warning at {move.X}, {move.Y}");
        // }
        //
        // foreach (Position move in WhiteEnPassantTargets)
        // {
        //     Debug.Log($"En passant warning at {move.X}, {move.Y}");
        // }
        //
        //
        // if (board[x, y] != null && board[x, y].Color == PieceColor.White)
        //     moves.AddRange(BlackEnPassantTargets);
        // else if (board[x, y] != null && board[x, y].Color == PieceColor.Black)
        //     moves.AddRange(WhiteEnPassantTargets);
        // if (WhiteEnPassantTargets.Count == 0)
        // {
        //     Debug.Log("No white en passant");
        // }

        return moves;
    }

    // public void GetEnPassantMoves(PieceBase piece)
    // {
    //     if (piece.Color == PieceColor.Black && IsEnPassantTarget)
    //     {
    //         BlackEnPassantTargets.Add(new Position(piece.Position.X, piece.Position.Y + 1));
    //     }
    //     else if (piece.Color == PieceColor.White && IsEnPassantTarget)
    //     {
    //         WhiteEnPassantTargets.Add(new Position(piece.Position.X, piece.Position.Y - 1));
    //     }
    // }
}