// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.InputSystem;
//
// public class ClickHandler : MonoBehaviour, IPointerClickHandler
// {
//     Controls controls;
//     private SpriteRenderer pieceSpriteRenderer;
//     private GameManager gameManager;
//
//     void Awake()
//     {
//         gameManager = FindFirstObjectByType<GameManager>();
//         controls = new Controls();
//     }
//
//     void OnEnable()
//     {
//         controls.Gameplay.Enable();
//         // controls.Gameplay.Click.performed += OnClick;
//     }
//
//     void OnDisable()
//     {
//         controls.Gameplay.Disable();
//         // controls.Gameplay.Click.performed -= OnClick;
//     }
//
//     // void OnClick(InputAction.CallbackContext context)
//     // {
//     //     // read value from mouse pointer position and convert to world point
//     //     Vector2 mousePos = Mouse.current.position.ReadValue();
//     //     Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
//     //
//     //     // cast a ray to get the collider for chosen object
//     //     RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
//     //     GameObject newSelected = hit.collider?.gameObject;
//     //     var piece = newSelected?.GetComponent<PieceVisual>().gameObject;
//     //     if (piece != null)
//     //     {
//     //         gameManager.SetTurn(piece);
//     //     }
//     //
//     //     if (newSelected != null && newSelected.CompareTag("Piece"))
//     //     {
//     //         gameManager.ToggleSelection(newSelected);
//     //     }
//     //
//     //     if (GameManager.clickedObject != null && hit.collider == null)
//     //         gameManager.ApplyMovement();
//     // }
//     public void OnPointerClick(PointerEventData eventData)
//     {
//         Vector2 mousePos = eventData.position;
//         Debug.Log($"{mousePos}");
//         
//     }
// }