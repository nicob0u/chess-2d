using System.Collections.Generic;
using System.Linq;
using Core.Pieces;
using UnityEditor;
using UnityEngine;

public class Board
{
    #region properties

    public Dictionary<Vector2Int, PieceBase> pieces = new Dictionary<Vector2Int, PieceBase>();
    Vector2Int logicalPiecePos;
    private int size;
    public bool wasMoveSuccessful;
    public PieceColor currentTurn;
    private int nextPieceId = 0;
    [HideInInspector] public List<PieceBase> capturedPieces = new List<PieceBase>();
    private bool isCheckmate = false;

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

                    logicalPiecePos = new Vector2Int(i, j);
                    PieceBase pawnPiece = new PieceBase(nextPieceId++, color, logicalPiecePos, new PawnPiece());
                    pieces[logicalPiecePos] = pawnPiece;
                }


                if ((j == 0 || j == 7) && (i == 0 || i == 7))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    logicalPiecePos = new Vector2Int(i, j);
                    PieceBase rookPiece = new PieceBase(nextPieceId++, color, logicalPiecePos, new RookPiece());
                    pieces[logicalPiecePos] = rookPiece;
                }

                if ((j == 0 || j == 7) && (i == 1 || i == 6))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    logicalPiecePos = new Vector2Int(i, j);
                    PieceBase knightPiece = new PieceBase(nextPieceId++, color, logicalPiecePos, new KnightPiece());
                    pieces[logicalPiecePos] = knightPiece;
                }

                if ((j == 0 || j == 7) && (i == 2 || i == 5))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    logicalPiecePos = new Vector2Int(i, j);
                    PieceBase bishopPiece = new PieceBase(nextPieceId++, color, logicalPiecePos, new BishopPiece());
                    pieces[logicalPiecePos] = bishopPiece;
                }

                if ((j == 0 || j == 7) && (i == 4))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    logicalPiecePos = new Vector2Int(i, j);
                    PieceBase queenPiece = new PieceBase(nextPieceId++, color, logicalPiecePos, new QueenPiece());
                    pieces[logicalPiecePos] = queenPiece;
                }

                if ((j == 0 || j == 7) && (i == 3))
                {
                    PieceColor color = (j == 0) ? PieceColor.White : PieceColor.Black;
                    logicalPiecePos = new Vector2Int(i, j);
                    PieceBase kingPiece = new PieceBase(nextPieceId++, color, logicalPiecePos, new KingPiece());
                    pieces[logicalPiecePos] = kingPiece;
                }
            }
        }
    }


    //GetLegalMoves
    public List<Vector2Int> GetLegalMovesFor(Vector2Int piecePosition)
    {
        List<Vector2Int> legalMoves = new List<Vector2Int>();
        var movesToRemove = new List<Vector2Int>();
        if (pieces.TryGetValue(piecePosition, out PieceBase piece))
        {
            legalMoves = piece.GetMoves(pieces, this);
            foreach (var move in legalMoves)
            {
                Debug.Log($"stepping over {move} rn");
                var result = SimulateMove(piecePosition, move);
                if (IsCheck(piece.Color, result))
                {
                    Debug.Log("this move can put your king in danger");
                    movesToRemove.Add(move);
                }
            }

            foreach (var removableMove in movesToRemove)
            {
                legalMoves.Remove(removableMove);
            }
        }

        return legalMoves;
    }

    bool IsCheckmate(PieceColor color)
    {
        foreach (var piece in pieces.Values.Where(p => p.Color == color && !p.IsCaptured))
        {
            var moves = GetLegalMovesFor(piece.Position);
            if (moves.Count > 0)
                return false;
        }

        return true;
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
        var result = SimulateMove(from, to);
        pieces.TryGetValue(from, out PieceBase piece);
        if (pieces.TryGetValue(to, out var target) && piece.Color != target.Color &&
            !target.Equals(default(PieceBase)))
            CapturePiece(to, pieces);


        pieces = result;
        if (IsCheckmate(piece.Color))
            Debug.Log("CHECKMATE BROTHER.");
        currentTurn = (currentTurn == PieceColor.White) ? PieceColor.Black : PieceColor.White;
    }

    //GetAllPiece
    public List<PieceBase> GetAllPieces()
    {
        List<PieceBase> piecesToSpawn = new List<PieceBase>();
        foreach (var kvp in pieces)
        {
            var piece = kvp.Value;
            piecesToSpawn.Add(piece);
        }

        return piecesToSpawn;
    }

    #endregion

    #region logic

    bool IsCheck(PieceColor color, Dictionary<Vector2Int, PieceBase> pieceDict)
    {
        var opponentColor = GetOppositeColor(color);
        var allowedEnemyMoves = new List<Vector2Int>();

        foreach (var piece in pieceDict.Values)
        {
            if (piece.Color == opponentColor && !piece.IsCaptured)
                allowedEnemyMoves.AddRange(piece.GetMoves(pieceDict, this));
        }

        var king = pieceDict.Values.FirstOrDefault(p =>
            p.GetLogicType() == typeof(KingPiece) && p.Color == color && !p.IsCaptured);

        return allowedEnemyMoves.Contains(king.Position);
    }

    public PieceColor GetOppositeColor(PieceColor color)
    {
        if (color == PieceColor.White)
            return PieceColor.Black;
        else
            return PieceColor.White;
    }

    void CapturePiece(Vector2Int to, Dictionary<Vector2Int, PieceBase> pieceDict)
    {
        if (!pieceDict.TryGetValue(to, out var piece)) return;

        piece.IsCaptured = true;
        piece.Position = new Vector2Int(-1, -1);
        capturedPieces.Add(piece);
    }


    Dictionary<Vector2Int, PieceBase> SimulateMove(Vector2Int from, Vector2Int to)
    {
        Dictionary<Vector2Int, PieceBase> newPiecesDict = new Dictionary<Vector2Int, PieceBase>();

        foreach (var kvp in pieces)
            newPiecesDict[kvp.Key] = kvp.Value;

        if (!pieces.TryGetValue(from, out var piece)) return null;
        Debug.Log(piece);

        if (currentTurn == piece.Color)
        {
            Debug.Log($"it is now {currentTurn}'s turn");
            var pieceToMove = newPiecesDict[from];
            newPiecesDict.Remove(from);
            pieceToMove.Position = to;
            newPiecesDict[to] = pieceToMove;
            wasMoveSuccessful = true;
        }

        else
        {
            Debug.Log("Not your turn");
        }


        Debug.Log(currentTurn);


        return newPiecesDict;
    }

    public bool IsEmpty(Dictionary<Vector2Int, PieceBase> piecesDict, Vector2Int movePos)
    {
        return !piecesDict.ContainsKey(movePos);
    }

    #endregion
}