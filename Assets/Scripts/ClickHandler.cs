using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    Controls controls;
    BoardManager boardManager;
    GameManager gameManager;
    public bool isSelected = false;
    public static GameObject clickedObject;
    private SpriteRenderer pieceSpriteRenderer;
    Board board;


    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        controls = new Controls();
    }

    void Start()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        board = boardManager.board;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Click.performed += OnClick;
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
        controls.Gameplay.Click.performed -= OnClick;
    }

    void OnClick(InputAction.CallbackContext context)
    {
        // read value from mouse pointer position and convert to world point
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // cast a ray to get the collider for chosen object
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        GameObject newSelected = hit.collider?.gameObject;
        if (newSelected != null && newSelected.CompareTag("Piece"))
        {
            ToggleSelection(newSelected);
            return;
        }

        if (clickedObject == null)
            return;
        ApplyVisualMovement();
    }

    void ToggleSelection(GameObject newSelected)
    {
        pieceSpriteRenderer = newSelected.GetComponent<SpriteRenderer>();

        if (clickedObject == newSelected)
        {
            var sr = clickedObject.GetComponent<SpriteRenderer>();
            sr.color = Color.white;
            clickedObject = null;
            boardManager.ClearHighlights();
        }

        else
        {
            if (clickedObject != null)
            {
                clickedObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            boardManager.ClearHighlights();

            clickedObject = newSelected;
            var sr = clickedObject.GetComponent<SpriteRenderer>();
            sr.color = Color.yellow;

            var visualPiece = newSelected.GetComponent<PieceVisual>();
            Vector2Int visualPosition = visualPiece.boardPosition;
            boardManager.HighlightTiles(newSelected, visualPosition.x, visualPosition.y);
        }
    }

    void ApplyVisualMovement()
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
        boardManager.board.MovePiece(pieceVisual.boardPosition, mouseGridPos);

        // move element visually
        clickedObject.transform.position =
            new Vector3(mouseGridPos.x, mouseGridPos.y, clickedObject.transform.position.z);
        pieceVisual.boardPosition = mouseGridPos;
        // clear previous position visually
        var sr = clickedObject.GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.white;

        clickedObject = null;
        boardManager.ClearHighlights();
    }
}