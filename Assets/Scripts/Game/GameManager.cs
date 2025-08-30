using System.Collections.Generic;
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
        Debug.Log(pos);
        Debug.LogWarning("Click toggled.");

        var piece = board.GetPiece(pos);
        var nowSelected = piece;

        if (previouslySelected != null && previouslySelected == nowSelected)
        {
            DeselectPiece();
        }
        else if (piece == null)
        {
            if (previouslySelected != null)
            {
                List<Vector2Int> allowedMoves =
                    board.GetLegalMovesFor(previouslySelected.Position);
                if (allowedMoves.Contains(pos))
                {
                    PerformMove(previouslySelected.Position, pos, allowedMoves);
                }
                else
                {
                    Debug.LogWarning("Selected position is not in the list of allowed moves");
                    
                }
                DeselectPiece();
                // if (board.wasMoveSuccessful)
                // {
                //     Debug.Log(
                //         $"piece successfully moved from {piece.Position.x}, {piece.Position.y} to {pos.x}, {pos.y}");
                // }
            }
        }
        else
        {
            if (previouslySelected != null)
            {
                // Switch selection visually
            }

            SelectPiece(nowSelected);
        }
    }

    void SelectPiece(PieceBase nowSelected)
    {
        previouslySelected = nowSelected;
        Debug.Log($"Currently selected piece is {nowSelected}");
    }

    void DeselectPiece()
    {
        previouslySelected = null;
        Debug.Log($"Piece deselected");
    }


    // public void ApplyMovement()
    // {
    //     var pieceVisual = clickedObject.GetComponent<PieceVisualItem>();
    //
    //     // var piece = pieceVisual.corePiece;
    //
    //
    //     Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //     Vector2Int mouseGridPos = new Vector2Int(
    //         Mathf.FloorToInt(mouseWorldPos.x),
    //         Mathf.FloorToInt(mouseWorldPos.y)
    //     );
    //
    //     //convert mouse position to logical position
    //     Vector2Int targetPosition = new Vector2Int(mouseGridPos.x, mouseGridPos.y);
    //
    //
    //     // move from --> to
    //     foreach (PieceBase piece in allPieces)
    //     {
    //     }
    //
    //     PerformMove(pieceVisual.Position, targetPosition);
    //
    //
    //     // move piece visually
    // }

    // public void EndTurn(PieceColor color)
    // {
    //     if (color == PieceColor.White)
    //     {
    //         Debug.Log("White's turn is over, switching to black.");
    //         pieceColor = PieceColor.Black;
    //     }
    //     else
    //     {
    //         Debug.Log("Black's turn is over, switching to white.");
    //         pieceColor = PieceColor.White;
    //     }
    //
    //     clickedObject = null;
//    }
    //
    // public void SetTurn(GameObject pieceVisual)
    // {
    //     if (pieceColor == PieceColor.White)
    //     {
    //         Debug.Log(pieceGameObjects.Count);
    //
    //         foreach (GameObject piece in pieceGameObjects)
    //         {
    //             var pieceCollider = piece.GetComponent<BoxCollider2D>();
    //
    //             if (piece.GetComponent<PieceVisualItem>().corePiece.Color == PieceColor.Black)
    //                 pieceCollider.enabled = false;
    //
    //             else
    //                 pieceCollider.enabled = true;
    //         }
    //     }
    //     else
    //     {
    //         foreach (GameObject piece in pieceGameObjects)
    //         {
    //             var pieceCollider = piece.GetComponent<BoxCollider2D>();
    //
    //             if (piece.GetComponent<PieceVisualItem>().corePiece.Color == PieceColor.White)
    //                 pieceCollider.enabled = false;
    //             else
    //                 pieceCollider.enabled = true;
    //         }
    //     }
    // }

    // void EnableWhitePiecesOnly()
    // {
    //     foreach (GameObject piece in pieceGameObjects)
    //     {
    //         var pieceCollider = piece.GetComponent<BoxCollider2D>();
    //
    //         if (piece.GetComponent<PieceVisual>().corePiece.Color == PieceColor.Black)
    //             pieceCollider.enabled = false;
    //
    //         else
    //             pieceCollider.enabled = true;
    //     }
    // }


    // public void ToggleSelection(Vector2Int? piecePosition)
    // {
    //     Debug.Log("Toggle Selection");
    //     GameObject newSelected = null;
    //
    //     foreach (GameObject pieceGo in pieceGameObjects)
    //     {
    //         var piece = pieceGo.GetComponent<PieceVisualItem>().corePiece;
    //         if (piece.Position == piecePosition)
    //         {
    //             newSelected = pieceGo;
    //         }
    //     }
    //
    //     if (clickedObject == newSelected)
    //     {
    //         var sr = clickedObject.GetComponent<SpriteRenderer>();
    //         sr.color = Color.white;
    //         clickedObject = null;
    //         tiles.ClearHighlights();
    //     }
    //
    //     else
    //     {
    //         if (clickedObject != null)
    //         {
    //             clickedObject.GetComponent<SpriteRenderer>().color = Color.white;
    //         }
    //
    //         tiles.ClearHighlights();
    //
    //         clickedObject = newSelected;
    //         var sr = clickedObject.GetComponent<SpriteRenderer>();
    //         sr.color = Color.yellow;
    //
    //
    //         var visualPiece = newSelected.GetComponent<PieceVisualItem>();
    //         var allowedMoves = board.GetLegalMovesFor(visualPiece.corePiece.Position);
    //         tiles.HighlightTiles(allowedMoves);
    //     }
    // }


    public void PerformMove(Vector2Int currentPiecePos, Vector2Int targetPiecePos, List<Vector2Int> allowedMoves)
    {
        if (allowedMoves?.Count == null)
            Debug.Log("No moves allowed.");

        if (!allowedMoves.Contains(targetPiecePos))
        {
            Debug.Log($"Target position {targetPiecePos.x},{targetPiecePos.y} is invalid");
            board.wasMoveSuccessful = false;
            return;
        }

        board.MovePiece(currentPiecePos, targetPiecePos);
        foreach (PieceBase logicalPiece in allPieces)
        {
            if (logicalPiece.Position == targetPiecePos)
                boardVisuals.CapturePieceVisually(targetPiecePos, allPieces);
        }

        Debug.Log(
            $"Piece is moving from {currentPiecePos.x},{currentPiecePos.y} to {targetPiecePos.x},{targetPiecePos.y}");
        currentPiecePos = targetPiecePos;
        Debug.Log($"Piece is now located at {currentPiecePos.x},  {currentPiecePos.y}");
        board.wasMoveSuccessful = true;
    }
}