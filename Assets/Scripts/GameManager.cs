using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    BoardVisuals boardVisuals;
    [FormerlySerializedAs("pieceVisuals")] public List<GameObject> pieceGameObjects;
    Board board;
    public Tiles tiles;
    public static GameObject clickedObject;
    PieceColor pieceColor;
    public Dictionary<PieceBase, GameObject> pieceToGameObject = new Dictionary<PieceBase, GameObject>();


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
        boardVisuals.SpawnPieces(board);
        tiles.CreateTiles();
        EnableWhitePiecesOnly();
    }


    void Update()
    {
        CapturePieceVisually();
    }

    void CapturePieceVisually()
    {
        if (board.capturedPieces.Count > 0)
        {
            foreach (PieceBase capturedPiece in board.capturedPieces)
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

        List<Vector2Int> allowedMoves =
            piece.GetMoves(board.pieces, pieceVisual.boardPosition.x, pieceVisual.boardPosition.y);

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int mouseGridPos = new Vector2Int(
            Mathf.FloorToInt(mouseWorldPos.x),
            Mathf.FloorToInt(mouseWorldPos.y)
        );


        if (!allowedMoves.Contains(mouseGridPos))
            return;
        // move from --> to
        board.MovePiece(pieceVisual.boardPosition, mouseGridPos);


        // move piece visually
        boardVisuals.ApplyVisualMovement(clickedObject, pieceVisual, mouseGridPos);
        EndTurn(pieceVisual.corePiece.Color);
        SetTurn(pieceVisual.gameObject);
    }

    public void AssignPrefabsToPieces(GameObject prefab, PieceBase corePiece, int x, int y, GameObject visualPiece,
        PieceVisual visual)
    {
        visual.corePiece = corePiece;
        board.pieces[x, y] = visual.corePiece;
        visual.boardPosition = new Vector2Int(x, y);

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
            Vector2Int visualPosition = visualPiece.boardPosition;
            tiles.HighlightTiles(newSelected, visualPosition.x, visualPosition.y, board);
        }
    }
}