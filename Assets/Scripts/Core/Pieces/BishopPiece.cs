using System.Collections.Generic;

public class BishopPiece : PieceBase
{
    public BishopPiece(PieceColor color) : base(color)
    {
    }

    public override List<Position> GetMoves(PieceBase[,] board)
    {   int y = Position.Y;
        int x = Position.X;

        var moves = new List<Position>();

        // right up
        int i = x + 1;
        int j = y + 1;
        while (i < 8 && j < 8)
        {
            if ((board[i, j] != null && board[i, j].Color == Color) || board[i, j] is KingPiece)
                break;
            if (board[i, j] != null && board[i, j].Color != Color)
            {
                moves.Add(new Position(i, j));
                break;
            }

            moves.Add(new Position(i, j));
            i++;
            j++;
        }

        // right down 
        i = x + 1;
        j = y - 1;
        while (i < 8 && j >= 0)
        {
            if ((board[i, j] != null && board[i, j].Color == Color) || board[i, j] is KingPiece)
                break;
            if (board[i, j] != null && board[i, j].Color != Color)
            {
                moves.Add(new Position(i, j));
                break;
            }

            moves.Add(new Position(i, j));
            i++;
            j--;
        }

        // left up
        i = x - 1;
        j = y + 1;
        while (i >= 0 && j < 8)
        {
            if ((board[i, j] != null && board[i, j].Color == Color) || board[i, j] is KingPiece)
                break;
            if (board[i, j] != null && board[i, j].Color != Color)
            {
                moves.Add(new Position(i, j));
                break;
            }

            moves.Add(new Position(i, j));
            i--;
            j++;
        }

        // left down
        i = x - 1;
        j = y - 1;
        while (i >= 0 && j >= 0)
        {
            if ((board[i, j] != null && board[i, j].Color == Color) || board[i, j] is KingPiece)
                break;
            if (board[i, j] != null && board[i, j].Color != Color)
            {
                moves.Add(new Position(i, j));
                break;
            }

            moves.Add(new Position(i, j));
            i--;
            j--;
        }

        return moves;
    }
}