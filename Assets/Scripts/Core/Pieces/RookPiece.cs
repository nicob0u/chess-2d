using System.Collections.Generic;

public class RookPiece : PieceBase
{
    public RookPiece(PieceColor color) : base(color)
    {
    }

    public override List<Position> GetMoves(PieceBase[,] board)
    {
        int y = Position.Y;
        int x = Position.X;

        var moves = new List<Position>();

        // left
        for (int i = x + 1; i < 8; i++)
        {
            if ((board[i, y] != null && board[i, y].Color == Color) || board[i, y] is KingPiece) break;
            if (board[i, y] != null && board[i, y].Color != Color)
            {
                moves.Add(new Position(i, y));
                break;
            }

            var allowedMove = new Position(i, y);
            moves.Add(allowedMove);
        }

        // right
        for (int i = x - 1; i >= 0; i--)
        {
            if ((board[i, y] != null && board[i, y].Color == Color) || board[i, y] is KingPiece) break;
            if (board[i, y] != null && board[i, y].Color != Color)
            {
                moves.Add(new Position(i, y));
                break;
            }

            var allowedMove = new Position(i, y);
            moves.Add(allowedMove);
        }

        // up
        for (int j = y + 1; j < 8; j++)
        {
            if ((board[x, j] != null && board[x, j].Color == Color) || board[x, j] is KingPiece) break;
            if (board[x, j] != null && board[x, j].Color != Color)
            {
                moves.Add(new Position(x, j));
                break;
            }

            var allowedMove = new Position(x, j);
            moves.Add(allowedMove);
        }

        // down
        for (int j = y - 1; j >= 0; j--)
        {
            if ((board[x, j] != null && board[x, j].Color == Color) || board[x, j] is KingPiece) break;
            if (board[x, j] != null && board[x, j].Color != Color)
            {
                moves.Add(new Position(x, j));
                break;
            }

            var allowedMove = new Position(x, j);
            moves.Add(allowedMove);
        }


        return moves;
    }
}