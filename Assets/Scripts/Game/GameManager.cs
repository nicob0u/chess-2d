using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    BoardVisuals boardVisuals;
    PieceColor pieceColor;
    private Board board;
    List<PieceBase> allPieces;
    private TileGenerator tileGenerator;
    List<Vector2Int> allowedMoves = new List<Vector2Int>();
    PieceBase previouslySelected;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);


        board = new Board();
        board.Init();
        tileGenerator = FindFirstObjectByType<TileGenerator>();
        tileGenerator.Init();
        boardVisuals = FindFirstObjectByType<BoardVisuals>();
    }

    void Start()
    {
        allPieces = board.GetAllPieces();
        boardVisuals.Init(allPieces);
    }

    public void OnTileClicked(Vector2Int pos)
    {
        PieceBase nowSelected = board.GetPiece(pos);

        if (!previouslySelected.Equals(default(PieceBase)) && allowedMoves.Contains(pos))
        {
            PerformMove(pos, allowedMoves, previouslySelected);
            // board.GetGameState(previouslySelected, simulatedPieces);
            DeselectPiece();
            return;
        }


        if (!nowSelected.IsCaptured && nowSelected.Color == board.currentTurn)
        {
            SelectPiece(nowSelected);
            tileGenerator.ClearHighlights(allowedMoves);
            allowedMoves = board.GetLegalMovesFor(nowSelected.Position);
            tileGenerator.HighlightMoves(allowedMoves);
            return;
        }

        DeselectPiece();
    }


    void SelectPiece(PieceBase nowSelected)
    {
        previouslySelected = nowSelected;
    }

    void DeselectPiece()
    {
        previouslySelected = default;
        tileGenerator.ClearHighlights(allowedMoves);
    }

    public void PerformMove(Vector2Int targetPiecePos, List<Vector2Int> allowedMoves, PieceBase piece)
    {
        if (allowedMoves?.Count == null)
            Debug.Log("No moves allowed.");

        if (piece.Color != board.currentTurn)
        {
            Debug.Log("Not your turn");
        }

        else
        {
            board.MovePiece(piece.Position, targetPiecePos);
            if (boardVisuals.pieceToVisualPiece.TryGetValue(piece.PieceId, out var visualPieceId))
            {
                PieceVisualItem visual = FindObjectsOfType<PieceVisualItem>()
                    .FirstOrDefault(v => v.pieceVisualId == visualPieceId);
                visual.transform.position = new Vector3(targetPiecePos.x, targetPiecePos.y, 0);
            }
            else
            {
                Debug.LogWarning($"Visual not found for PieceId {piece.PieceId}");
            }

            boardVisuals.CapturePieceVisually(board.capturedPieces);
        }


        piece.Position = targetPiecePos;
        if (!allowedMoves.Contains(targetPiecePos))
        
            board.wasMoveSuccessful = false;
        
    }
}