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

    // [FormerlySerializedAs("pieceVisuals")] public List<GameObject> pieceGameObjects;
    // public Tiles tiles;
    public static GameObject clickedObject;
    PieceColor pieceColor;
    List<Vector2Int> legalMoves = new List<Vector2Int>();
    private Board board;
    List<PieceBase> allPieces;
    private TileGenerator tileGenerator;

    public PieceBase previouslySelected;

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
        // EnableWhitePiecesOnly();
    }

    public void OnTileClicked(Vector2Int pos)
    {
        var piece = board.GetPiece(pos);
        var nowSelected = piece;

        if (previouslySelected != null && previouslySelected == nowSelected)
        {
            DeselectPiece();
        }
        else if (piece == null)
        {
            if (previouslySelected != null && board.currentTurn == previouslySelected.Color)
            {
                Debug.Log($"Something was previously selected and the current turn is {board.currentTurn}");
                List<Vector2Int> allowedMoves =
                    board.GetLegalMovesFor(previouslySelected.Position);
                foreach (var move in allowedMoves)
                {
                    Debug.Log($"piece is allowed to move to {move}");
                }
                
                if (allowedMoves.Contains(pos))
                {
                    PerformMove(pos, allowedMoves, previouslySelected);
                }
                else
                {
                    Debug.LogWarning("Not allowed");
                }

                DeselectPiece();
            }
        }
        else
        {
            if (previouslySelected != null && piece.Color != previouslySelected.Color)
            {
                Debug.Log(
                    "something was previously selected and the current turn is {piece.Color} man i dont know what the fuck im doing here");
                List<Vector2Int> allowedMoves =
                    board.GetLegalMovesFor(previouslySelected.Position);
                if (allowedMoves.Contains(pos))
                {
                    PerformMove(pos, allowedMoves, previouslySelected);
                }
                else
                {
                    Debug.Log(
                        "move is not allowed for some reason did i not include this in the logic. man fuck this shit");
                }
            }
            else if (previouslySelected != null && board.currentTurn == piece.Color)
            {
                Debug.Log("same team man stop");
            }

            SelectPiece(nowSelected);
        }
    }

    void SelectPiece(PieceBase nowSelected)
    {
        previouslySelected = nowSelected;
        Debug.Log($"{nowSelected} is now selected");
    }

    void DeselectPiece()
    {
        previouslySelected = null;
        Debug.Log($"Piece deselected");
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
                Debug.Log($"Found visual for PieceId {piece.PieceId}: {visualPieceId}");
                PieceVisualItem visual = FindObjectsOfType<PieceVisualItem>()
                    .FirstOrDefault(v => v.pieceVisualId == visualPieceId);
                visual.transform.position = new Vector3(targetPiecePos.x, targetPiecePos.y, 0);
            }
            else
            {
                Debug.LogWarning($"Visual not found for PieceId {piece.PieceId}");
            }

            // var capturedPiece = board.GetPiece(targetPiecePos);

                boardVisuals.CapturePieceVisually(board.capturedPieces);
        }


        Debug.Log(
            $"Piece is moving from {piece.Position.x},{piece.Position.y} to {targetPiecePos.x},{targetPiecePos.y}");
        piece.Position = targetPiecePos;
        Debug.Log($"Piece is now located at {piece.Position.x},  {piece.Position.y}");

        // var piece = board.GetPiece(currentPiecePos);
    
    if (!allowedMoves.Contains(targetPiecePos))
    {
        Debug.Log($"Target position {targetPiecePos.x},{targetPiecePos.y} is invalid");
        board.wasMoveSuccessful = false;
    }
}

}