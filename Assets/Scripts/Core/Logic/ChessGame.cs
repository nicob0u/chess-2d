using System.Collections.Generic;
using UnityEngine;

public class ChessGame
{
    private Board board;
    public bool wasMoveSuccessful;

    public void InitializeBoard()
    {
        board = new Board();
        board.Init();
    }

    public List<Position> GetLegalMovesFor(PieceBase piece)
    {
        foreach (Position move in piece.GetMoves(board.pieces))
        {
            Debug.Log($"piece is allowed to move to {move.X},{move.Y}");
        }

        return piece.GetMoves(board.pieces);
    }


    public void PerformMove(PieceBase piece, Position targetPosition)
    {
        List<Position> allowedMoves =
            GetLegalMovesFor(piece);

        if (allowedMoves.Count == 0)
            Debug.Log("Allowed moves are empty");

        if (!allowedMoves.Contains(targetPosition))
        {
            Debug.Log($"Target position {targetPosition.X},{targetPosition.Y} is invalid");
            Debug.Log("Target position is invalid");
            wasMoveSuccessful = false;
            return;
        }

        board.MovePiece(piece.Position, targetPosition);
        Debug.Log(
            $"Piece is moving from {piece.Position.X},{piece.Position.Y} to {targetPosition.X},{targetPosition.Y}");
        piece.Position = targetPosition;
        Debug.Log($"Piece is now located at {piece.Position.X},  {piece.Position.Y}");
        wasMoveSuccessful = true;
    }


    public List<PieceBase> GetAllPieces()
    {
        List<PieceBase> piecesToSpawn = new List<PieceBase>();

        foreach (PieceBase piece in board.pieces)
            if (piece != null)
                piecesToSpawn.Add(piece);
        return piecesToSpawn;
    }

    public List<PieceBase> GetCapturedPieces()
    {
        List<PieceBase> capturedPiecesList = new List<PieceBase>();
        foreach (PieceBase piece in board.capturedPieces)
        {
            capturedPiecesList.Add(piece);
        }

        return capturedPiecesList;
    }
}