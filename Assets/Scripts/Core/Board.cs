using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board
{
    #region properties

    private Dictionary<Vector2Int, PieceBase> pieces = new Dictionary<Vector2Int, PieceBase>();
    Vector2Int logicalPiecePos;
    private int size;
    public bool wasMoveSuccessful;
    public PieceColor currentTurn;
    public PieceColor previousTurn;

    #endregion

    #region API

    //Initialize
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
                    logicalPiecePos = new Vector2Int(i, j);
                    pieces[logicalPiecePos] = pawnPiece;
                    pawnPiece.Position = new Vector2Int(i, j);
                    Debug.Log(
                        $"{pawnPiece} at {pawnPiece.Position.x}, {pawnPiece.Position.y} has been added to the list of pieces");
                }

                if ((j == 0 || j == 7) && (i == 0 || i == 7))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    RookPiece rookPiece = new RookPiece(color);
                    logicalPiecePos = new Vector2Int(i, j);
                    pieces[logicalPiecePos] = rookPiece;
                    rookPiece.Position = new Vector2Int(i, j);
                }

                if ((j == 0 || j == 7) && (i == 1 || i == 6))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    KnightPiece knightPiece = new KnightPiece(color);
                    logicalPiecePos = new Vector2Int(i, j);
                    pieces[logicalPiecePos] = knightPiece;
                    knightPiece.Position = new Vector2Int(i, j);
                }

                if ((j == 0 || j == 7) && (i == 2 || i == 5))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    BishopPiece bishopPiece = new BishopPiece(color);
                    logicalPiecePos = new Vector2Int(i, j);
                    pieces[logicalPiecePos] = bishopPiece;
                    bishopPiece.Position = new Vector2Int(i, j);
                }

                if ((j == 0 || j == 7) && (i == 4))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    QueenPiece queenPiece = new QueenPiece(color);
                    logicalPiecePos = new Vector2Int(i, j);
                    pieces[logicalPiecePos] = queenPiece;
                    queenPiece.Position = new Vector2Int(i, j);
                }

                if ((j == 0 || j == 7) && (i == 3))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    KingPiece kingPiece = new KingPiece(color);
                    logicalPiecePos = new Vector2Int(i, j);
                    pieces[logicalPiecePos] = kingPiece;
                    kingPiece.Position = new Vector2Int(i, j);
                }

                if (pieces == null)
                {
                    Debug.Log($" {pieces[logicalPiecePos]} is null");
                }
            }

            foreach (Vector2Int testPos in pieces.Keys)
            {
                Debug.Log($"{pieces[testPos]} at {testPos} ");
            }
        }

        // capturedPieces = new List<PieceBase>();
    }

    //GetAllMoves

    //GetLegalMoves
    public List<Vector2Int> GetLegalMovesFor(Vector2Int piecePosition)
    {
        List<Vector2Int> legalMoves = new List<Vector2Int>();
        
        foreach (var move in legalMoves)
        {
            Debug.Log($"Piece is allowed to move to {move}");
        }

        if (pieces.TryGetValue(piecePosition, out PieceBase piece))
        {
                legalMoves = piece.GetMoves(pieces);
        }

        return legalMoves;
    }

    //GetPiece
    public PieceBase GetPiece(Vector2Int position)
    {
        pieces.TryGetValue(position, out PieceBase piece);
        return piece;
    }

    //MovePiece
    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        var piece = GetPiece(from);
        if (piece == null) return;

        var target = GetPiece(to);
        if (target != null && piece.Color != target.Color)
            CapturePiece(to);
        piece.Position = to;
        pieces[to] = piece;
        pieces.Remove(from);
        wasMoveSuccessful = true;
        if (previousTurn == PieceColor.White)
        {
            currentTurn = PieceColor.Black;
        }
        else if (previousTurn == PieceColor.Black)
        {
            currentTurn = PieceColor.White;
        }

        previousTurn = currentTurn;
    }

    //GetAllPiece

    public List<PieceBase> GetAllPieces()
    {
        List<PieceBase> piecesToSpawn = new List<PieceBase>();
        foreach (var kvp in pieces)
            if (kvp.Value != null)
            {
                var piece = kvp.Value;
                piecesToSpawn.Add(piece);
            }

        return piecesToSpawn;
    }

    //GetTurn

    #endregion

    #region logic

    // private List<PieceBase> GetCapturedPieces()
    // {
    //     List<PieceBase> capturedPiecesList = new List<PieceBase>();
    //     foreach (PieceBase piece in capturedPieces)
    //     {
    //         capturedPiecesList.Add(piece);
    //     }
    //
    //     return capturedPiecesList;
    // }

    private void CapturePiece(Vector2Int to)
    {
        Vector2Int capturedPosition = new Vector2Int(-1, -1);
        pieces[to].IsCaptured = true;
        pieces[to].Position = capturedPosition;
        // capturedPieces.Add(pieces[to]);
        pieces[to] = null;
    }

    public List<Vector2Int> GetAllMoves(Vector2Int currentPosition)
    {
        List<Vector2Int> allAllowedMoves = new List<Vector2Int>();
        foreach (PieceBase piece in pieces.Values)
        {
            if (piece == null) continue;
            if (piece.IsCaptured) continue;
            allAllowedMoves.AddRange(piece.GetMoves(pieces));
        }

        return GetAllMoves(currentPosition);
    }

    #endregion
}