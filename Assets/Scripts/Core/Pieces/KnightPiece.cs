using System.Collections.Generic;
using Core.Pieces;
using UnityEngine;
using UnityEngine.UIElements;

public class KnightPiece : ILogic
{
    public  List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces, Board board, PieceBase piece)
    {
        int y = piece.Position.y;
        int x = piece.Position.x;

        Vector2Int movePos;

        var moves = new List<Vector2Int>();

        for (int i = x - 2; i < x + 3; i++)
        {
            for (int j = y - 2; j < y + 3; j++)
            {
                movePos = new Vector2Int(i, j);
                if (i < 0 || i > 7 || j < 0 || j > 7) continue;
                if (board.IsEmpty(pieces, movePos))
                {
                    if (i == x - 1 || i == x + 1)
                    {
                        if (j == y - 1 || j == y + 1 || j == y) continue;
                        moves.Add(movePos);
                    }

                    if (i == x - 2 || i == x + 2)
                    {
                        if (j == y - 2 || j == y + 2 || j == y) continue;
                        moves.Add(movePos);
                    }
                }

                if (!board.IsEmpty(pieces, movePos))
                {
                    if (pieces[movePos].Color != piece.Color)
                    {
                        if (i == x - 1 || i == x + 1)
                        {
                            if (j == y - 1 || j == y + 1 || j == y) continue;
                            moves.Add(movePos);
                        }

                        if (i == x - 2 || i == x + 2)
                        {
                            if (j == y - 2 || j == y + 2 || j == y) continue;
                            moves.Add(movePos);
                        }
                    }
                }
            }
        }

        return moves;
    }
}