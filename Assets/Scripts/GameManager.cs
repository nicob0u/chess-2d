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
    ChessGame chessGame;
    List<Position> legalMoves = new List<Position>();
    

    void Awake()
    {
        chessGame = new ChessGame();
        chessGame.InitializeBoard();
        boardVisuals = FindFirstObjectByType<BoardVisuals>();
        pieceGameObjects = new List<GameObject>();
        tiles = FindFirstObjectByType<Tiles>();
    }

    void Start()
    {
        boardVisuals.SpawnPieces(chessGame.GetAllPieces());
        tiles.CreateTiles();
        EnableWhitePiecesOnly();
    }


    void Update()
    {
        CapturePieceVisually();
    }

    void CapturePieceVisually()
    {
        var capturedPieces = chessGame.GetCapturedPieces();
        if (capturedPieces.Count > 0)
        {
            foreach (PieceBase capturedPiece in capturedPieces)
            {
                if (pieceToGameObject.TryGetValue(capturedPiece, out GameObject pieceGo))
                {
                    if (pieceGo != null)
                    {
                        pieceGameObjects.Remove(pieceGo);
                        pieceToGameObject.Remove(capturedPiece);
                        Destroy(pieceGo);
                    }
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
        Position targetPosition = new Position(mouseGridPos.x, mouseGridPos.y);

      
        // move from --> to
         chessGame.PerformMove(piece, targetPosition);


        // move piece visually
        if(chessGame.wasMoveSuccessful)
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

    public void ToggleSelection(GameObject newSelected)
    {
        Debug.Log("Toggle Selection");

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
            var allowedMoves = chessGame.GetLegalMovesFor(visualPiece.corePiece);
            tiles.HighlightTiles(allowedMoves);
        }
    }
}