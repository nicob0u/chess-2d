using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    Controls controls;
    BoardManager boardManager;
    GameManager gameManager;
    public bool isPieceClicked = false;
    public GameObject clickedObject;

    void Awake()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        controls = new Controls();
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
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 boardPos = new Vector2(Mathf.Floor(worldPos.x), Mathf.Floor(worldPos.y));

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null)

            clickedObject = hit.collider.gameObject;


        if (clickedObject != null && clickedObject.CompareTag("Piece"))
        {
            clickedObject.layer = LayerMask.NameToLayer("Currently Selected");
            var visualPiece = clickedObject.GetComponent<PieceVisual>();
            Vector2Int visualPosition = visualPiece.boardPosition;
            boardManager.HighlightTiles(clickedObject, visualPosition.x, visualPosition.y);
            Debug.Log($"tryna highlight {visualPosition.x}, {visualPosition.y}");
        }

        if (clickedObject != null && clickedObject.layer == LayerMask.NameToLayer("Currently Selected"))
        {
            gameManager.HighlightPiece(clickedObject);
        }
    }
}