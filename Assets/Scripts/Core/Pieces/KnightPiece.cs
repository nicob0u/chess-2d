using UnityEngine;
using System.Collections.Generic;
public class KnightPiece : PieceBase
{
    public KnightPiece(PieceColor color) : base(color)
    {
    }
    public override List<Vector2Int> GetMoves(PieceBase[,] board, int x, int y)
    {
        
        var moves = new List<Vector2Int>();

        for (int j = y; j < y + 3; j++)
        {

            if (board[x, j] == null)
            {
                var allowedMove = new Vector2Int(x, j);
                Debug.Log($"highlighed moves are at {allowedMove}");
                moves.Add(allowedMove);

            }
            

        }
        return moves;
    }

}
