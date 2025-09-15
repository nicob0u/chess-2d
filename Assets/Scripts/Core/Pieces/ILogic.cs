using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Pieces
{
    public interface ILogic
    {
            List<Vector2Int> GetMoves(Dictionary<Vector2Int, PieceBase> pieces, Board board, PieceBase piece);
    }
}