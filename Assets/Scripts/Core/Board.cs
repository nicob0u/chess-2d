using UnityEngine;

public class Board
{
    public PieceBase[,] pieces;
    private int size;
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

                    pieces[i, j] = new PawnPiece(color);
                }

                if ((j == 0 || j == 7) && (i == 0 || i == 7))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new RookPiece(color);
                }

                if ((j == 0 || j == 7) && (i == 1 || i == 6))
                {
                   
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new KnightPiece(color);
                }

                if ((j == 0 || j == 7) && (i == 2 || i == 5))
                {
                    
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new BishopPiece(color);
                    
                }

                if ((j == 0 || j == 7) && (i == 4))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new QueenPiece(color);
                }

                if ((j == 0 || j == 7) && (i == 3))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    pieces[i, j] = new KingPiece(color);
                }
            }
        }
    }

    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        var piece = pieces[from.x, from.y];
        if (piece == null) return;
        
       pieces[to.x, to.y] = piece;
       pieces[from.x, from.y] = null;
    }
}