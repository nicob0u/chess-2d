using System.Collections.Generic;

public class QueenPiece : PieceBase
{
    public QueenPiece(PieceColor color) : base(color)
    {
    }

    public override List<Position> GetMoves(PieceBase[,] board)
    {
        int y = Position.Y;
        int x = Position.X;

        var moves = new List<Position>();

        // straight paths

        for (int i = x + 1; i < 8; i++)
        {
            if (board[i, y] != null && board[i, y].Color != Color && !(board[i, y] is KingPiece))
            {
                moves.Add(new Position(i, y));
                break;
            }

            if (board[i, y] != null && board[i, y].Color == Color)
                break;

            var allowedMove = new Position(i, y);
            moves.Add(allowedMove);
        }


        for (int i = x - 1; i >= 0; i--)
        {
            if (board[i, y] != null && board[i, y].Color != Color && !(board[i, y] is KingPiece))
            {
                moves.Add(new Position(i, y));
                break;
            }

            if (board[i, y] != null && board[i, y].Color == Color)
                break;


            var allowedMove = new Position(i, y);
            moves.Add(allowedMove);
        }


        for (int j = y + 1; j < 8; j++)
        {
            if (board[x, j] != null && board[x, j].Color != Color && !(board[x, j] is KingPiece))
            {
                moves.Add(new Position(x, j));
                break;
            }

            if (board[x, j] != null && board[x, j].Color == Color)
                break;

            var allowedMove = new Position(x, j);
            moves.Add(allowedMove);
        }


        for (int j = y - 1; j >= 0; j--)
        {
            if (board[x, j] != null && board[x, j].Color != Color && !(board[x, j] is KingPiece))
            {
                moves.Add(new Position(x, j));
                break;
            }

            if (board[x, j] != null && board[x, j].Color == Color)
                break;

            var allowedMove = new Position(x, j);
            moves.Add(allowedMove);
        }

        // diagonal paths
        int k = x + 1;
        int l = y + 1;
        while (k < 8 && l < 8)
        {
            if ((board[k, l] != null && board[k, l].Color == Color) || board[k, l] is KingPiece)
                break;
            if (board[k, l] != null && board[k, l].Color != Color)
            {
                moves.Add(new Position(k, l));
                break;
            }

            moves.Add(new Position(k, l));
            k++;
            l++;
        }

        // right down 
        k = x + 1;
        l = y - 1;
        while (k < 8 && l >= 0)
        {
            if ((board[k, l] != null && board[k, l].Color == Color) || board[k, l] is KingPiece)
                break;
            if (board[k, l] != null && board[k, l].Color != Color)
            {
                moves.Add(new Position(k, l));
                break;
            }

            moves.Add(new Position(k, l));
            k++;
            l--;
        }

        // left up
        k = x - 1;
        l = y + 1;
        while (k >= 0 && l < 8)
        {
            if ((board[k, l] != null && board[k, l].Color == Color) || board[k, l] is KingPiece)
                break;
            if (board[k, l] != null && board[k, l].Color != Color)
            {
                moves.Add(new Position(k, l));
                break;
            }

            moves.Add(new Position(k, l));
            k--;
            l++;
        }

        // left down
        k = x - 1;
        l = y - 1;
        while (k >= 0 && l >= 0)
        {
            if ((board[k, l] != null && board[k, l].Color == Color) || board[k, l] is KingPiece)
                break;
            if (board[k, l] != null && board[k, l].Color != Color)
            {
                moves.Add(new Position(k, l));
                break;
            }

            moves.Add(new Position(k, l));
            k--;
            l--;
        }


        return moves;
    }
}