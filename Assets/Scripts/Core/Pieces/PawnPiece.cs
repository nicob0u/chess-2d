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

        for (int j = y + 1; j < y + 3; j++)
        {

            if (board[x, j] == null)
            {
                var allowedMove = new Vector2Int(x, j);
                moves.Add(allowedMove);
            }
            

        }
        return moves;
    }
    
    
}
