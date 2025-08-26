using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public PieceBase[,] pieces;
    private int size;
    public List<PieceBase> capturedPieces;

    public Board(int size = 8)
    {
        this.size = size;
        pieces = new PieceBase[size, size];
    }

    public void Init()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (j == 1 || j == 6)
                {
                    PieceColor color = (j == 1) ? PieceColor.White : PieceColor.Black;

                    PawnPiece pawnPiece = new PawnPiece(color);
                    pieces[i, j] = pawnPiece;
                    pawnPiece.Position = new Position(i, j);
                    Debug.Log(
                        $"{pawnPiece} at {pawnPiece.Position.X}, {pawnPiece.Position.Y} has been added to the list of pieces");
                }

                if ((j == 0 || j == 7) && (i == 0 || i == 7))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new RookPiece(color);
                    pieces[i, j].Position = new Position(i, j);
                    Debug.Log(
                        $"{pieces[i, j]} at {pieces[i, j].Position.X}, {pieces[i, j].Position.Y} has been added to the list of pieces");
                }

                if ((j == 0 || j == 7) && (i == 1 || i == 6))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new KnightPiece(color);
                    pieces[i, j].Position = new Position(i, j);
                }

                if ((j == 0 || j == 7) && (i == 2 || i == 5))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new BishopPiece(color);
                    pieces[i, j].Position = new Position(i, j);
                }

                if ((j == 0 || j == 7) && (i == 4))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new QueenPiece(color);
                    pieces[i, j].Position = new Position(i, j);
                }

                if ((j == 0 || j == 7) && (i == 3))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new KingPiece(color);
                    pieces[i, j].Position = new Position(i, j);
                }

                if (pieces[i, j] == null)
                {
                    Debug.Log($" {pieces[i, j]} is null");
                }
            }
        }

        capturedPieces = new List<PieceBase>();
    }

    public void MovePiece(Position from, Position to)
    {
        var piece = pieces[from.X, from.Y];
        if (piece == null) return;

        if (pieces[to.X, to.Y] != null && pieces[from.X, from.Y].Color != pieces[to.X, to.Y].Color)
            CapturePiece(to);

        pieces[to.X, to.Y] = piece;
        pieces[from.X, from.Y] = null;
    }

    public bool IsEnPassantVulnerable(PieceBase piece, Position from)
    {
        if (piece is PawnPiece && Mathf.Abs(piece.Position.Y - from.Y) == 2 && (from.Y == 1 || from.Y == 6))
        {
            return true;
        }

        return false;
    }


    public void CapturePiece(Position to)
    {
        pieces[to.X, to.Y].IsCaptured = true;
        capturedPieces.Add(pieces[to.X, to.Y]);
        pieces[to.X, to.Y] = null;
    }
}