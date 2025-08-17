using UnityEngine;
using System.Collections.Generic;

public class BishopPiece : PieceBase
{
    public BishopPiece(PieceColor color) : base(color)
    {
    }

    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
      
        var moves = new List<Vector2Int>();
        
        // right up
        int i = x + 1;
        int j = y + 1;
        while (i < 8 && j < 8)
        {
            if (board[i, j] != null)
                break;

            moves.Add(new Vector2Int(i, j));
            i++;
            j++;
        }

        // right down 
         i = x + 1;
         j = y - 1;
        while (i < 8 && j >= 0)
        {
            if (board[i, j] != null)
                break;

            moves.Add(new Vector2Int(i, j));
            i++;
            j--;
        }

        // left up
        i = x - 1;
        j = y + 1;
        while (i >= 0 && j < 8)
        {
            if (board[i, j] != null)
                break;

            moves.Add(new Vector2Int(i, j));
            i--;
            j++;
        }

        // left down
        i = x - 1;
        j = y - 1;
        while (i >= 0 && j >= 0)
        {
            if (board[i, j] != null)
                break;

            moves.Add(new Vector2Int(i, j));
            i--;
            j--;
        }

        return moves;
    }
}