using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    BoardVisuals boardVisuals;
    [FormerlySerializedAs("pieceVisuals")] public List<GameObject> pieceGameObjects;
    public Tiles tiles;
    public static GameObject clickedObject;
    PieceColor pieceColor;
    public Dictionary<PieceBase, GameObject> pieceToGameObject = new Dictionary<PieceBase, GameObject>();
    List<Vector2Int> legalMoves = new List<Vector2Int>();
    private Board board;
    List<PieceBase> allPieces;

    void Awake()
    {
        board = new Board();
        board.Init();
        boardVisuals = FindFirstObjectByType<BoardVisuals>();
        pieceGameObjects = new List<GameObject>();
        tiles = FindFirstObjectByType<Tiles>();
    }

    void Start()
    {
        allPieces = board.GetAllPieces();
        boardVisuals.SpawnPieces(allPieces);
        tiles.CreateTiles();
        EnableWhitePiecesOnly();
    }


    //
    // void EnPasssantVulnerable()
    // {
    //     chessGame.CheckForEnPassantVulnerability();
    // }
    void CapturePieceVisually(Vector2Int piecePos)
    {
        foreach (PieceBase logicalPiece in allPieces)
        {
            if (logicalPiece.Position == piecePos)
                if (pieceToGameObject.TryGetValue(logicalPiece, out GameObject pieceGo))
                {
                    if (pieceGo != null)
                    {
                        pieceGameObjects.Remove(pieceGo);
                        pieceToGameObject.Remove(logicalPiece);
                        Destroy(pieceGo);
                    }
                }
        }
    }

    public void ApplyMovement()
    {
        var pieceVisual = clickedObject.GetComponent<PieceVisual>();

        var piece = pieceVisual.corePiece;


        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int mouseGridPos = new Vector2Int(
            Mathf.FloorToInt(mouseWorldPos.x),
            Mathf.FloorToInt(mouseWorldPos.y)
        );

        //convert mouse position to logical position
        Vector2Int targetPosition = new Vector2Int(mouseGridPos.x, mouseGridPos.y);


        // move from --> to
        PerformMove(piece.Position, targetPosition);


        // move piece visually
        if (board.wasMoveSuccessful)
        {
            boardVisuals.ApplyVisualMovement(clickedObject, mouseGridPos);
            EndTurn(pieceVisual.corePiece.Color);
            SetTurn(pieceVisual.gameObject);
        }
    }

    public void AssignPrefabsToPieces(PieceBase corePiece, GameObject visualPiece,
        PieceVisual visual)
    {
        visual.corePiece = corePiece;
        visualPiece.tag = "Piece";
        pieceGameObjects.Add(visualPiece);
    }

    public void EndTurn(PieceColor color)
    {
        if (color == PieceColor.White)
        {
            Debug.Log("White's turn is over, switching to black.");
            pieceColor = PieceColor.Black;
        }
        else
        {
            Debug.Log("Black's turn is over, switching to white.");
            pieceColor = PieceColor.White;
        }

        clickedObject = null;
    }

    public void SetTurn(GameObject pieceVisual)
    {
        if (pieceColor == PieceColor.White)
        {
            Debug.Log(pieceGameObjects.Count);

            foreach (GameObject piece in pieceGameObjects)
            {
                var pieceCollider = piece.GetComponent<BoxCollider2D>();

                if (piece.GetComponent<PieceVisual>().corePiece.Color == PieceColor.Black)
                    pieceCollider.enabled = false;

                else
                    pieceCollider.enabled = true;
            }
        }
        else
        {
            foreach (GameObject piece in pieceGameObjects)
            {
                var pieceCollider = piece.GetComponent<BoxCollider2D>();

                if (piece.GetComponent<PieceVisual>().corePiece.Color == PieceColor.White)
                    pieceCollider.enabled = false;
                else
                    pieceCollider.enabled = true;
            }
        }
    }

    void EnableWhitePiecesOnly()
    {
        foreach (GameObject piece in pieceGameObjects)
        {
            var pieceCollider = piece.GetComponent<BoxCollider2D>();

            if (piece.GetComponent<PieceVisual>().corePiece.Color == PieceColor.Black)
                pieceCollider.enabled = false;

            else
                pieceCollider.enabled = true;
        }
    }

    public void ToggleSelection(Vector2Int piecePosition)
    {
        Debug.Log("Toggle Selection");
        GameObject newSelected = null;
        
        foreach (GameObject pieceGo in pieceGameObjects)
        {
            var piece =  pieceGo.GetComponent<PieceVisual>().corePiece;
            if (piece.Position == piecePosition)
            {
                newSelected = pieceGo;
            }
        }
        
        if (clickedObject == newSelected)
        {
            var sr = clickedObject.GetComponent<SpriteRenderer>();
            sr.color = Color.white;
            clickedObject = null;
            tiles.ClearHighlights();
        }

        else
        {
            if (clickedObject != null)
            {
                clickedObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            tiles.ClearHighlights();

            clickedObject = newSelected;
            var sr = clickedObject.GetComponent<SpriteRenderer>();
            sr.color = Color.yellow;


            var visualPiece = newSelected.GetComponent<PieceVisual>();
            var allowedMoves = board.GetLegalMovesFor(visualPiece.corePiece.Position);
            tiles.HighlightTiles(allowedMoves);
        }
    }


    public void PerformMove(Vector2Int currentPiecePos, Vector2Int targetPiecePos)
    {
        List<Vector2Int> allowedMoves =
            board.GetLegalMovesFor(currentPiecePos);

        if (allowedMoves.Count == 0)
            Debug.Log("Allowed moves are empty");

        if (!allowedMoves.Contains(targetPiecePos))
        {
            Debug.Log($"Target position {targetPiecePos.x},{targetPiecePos.y} is invalid");
            Debug.Log("Target position is invalid");
            board.wasMoveSuccessful = false;
            return;
        }

        board.MovePiece(currentPiecePos, targetPiecePos);
        foreach (PieceBase logicalPiece in allPieces)
        {
            if (logicalPiece.Position == targetPiecePos)
                CapturePieceVisually(targetPiecePos);
        }
        Debug.Log(
            $"Piece is moving from {currentPiecePos.x},{currentPiecePos.y} to {targetPiecePos.x},{targetPiecePos.y}");
        currentPiecePos = targetPiecePos;
        Debug.Log($"Piece is now located at {currentPiecePos.x},  {currentPiecePos.y}");
        board.wasMoveSuccessful = true;
    }
}