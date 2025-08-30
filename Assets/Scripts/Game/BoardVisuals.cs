using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BoardVisuals : MonoBehaviour
{
    [SerializeField] private PieceVisualItem pieceVisualPrefab;
    [SerializeField] Sprite lightPawn;
    [SerializeField] Sprite darkPawn;
    [SerializeField] Sprite lightRook;
    [SerializeField] Sprite darkRook;
    [SerializeField] Sprite lightKnight;
    [SerializeField] Sprite darkKnight;
    [SerializeField] Sprite lightBishop;
    [SerializeField] Sprite darkBishop;
    [SerializeField] Sprite lightQueen;
    [SerializeField] Sprite darkQueen;
    [SerializeField] Sprite lightKing;
    [SerializeField] Sprite darkKing;

    PieceBase _corePiece;

    [HideInInspector]
    public Dictionary<PieceBase, PieceVisualItem> pieceToVisualPiece = new Dictionary<PieceBase, PieceVisualItem>();

    void Start()
    {
    }

    public void Init(List<PieceBase> pieces)
    {
        if (pieces != null)
            Debug.Log(pieces.Count);
        else
            Debug.LogWarning("pieces is null");

        foreach (PieceBase piece in pieces)
        {
            SpawnPiece(piece);
            Debug.Log($"{piece} has been spawned");
        }
    }

    void SpawnPiece(PieceBase piece)
    {
        var sprite = FindPieceSprite(piece);

        if (sprite != null)
        {
            Debug.Log($"Sprite {sprite} is present");
            Vector3 spritePosition = new Vector3(piece.Position.x, piece.Position.y, 0);

            var visualPiece = Instantiate(pieceVisualPrefab, spritePosition, Quaternion.identity, this.transform);
            visualPiece.Init(spritePosition, sprite);
            pieceToVisualPiece[piece] = visualPiece;
            visualPiece.tag = "Piece";

            visualPiece.transform.SetAsLastSibling();
        }
        else Debug.Log("No sprites found.");
    }

    Sprite FindPieceSprite(PieceBase piece)
    {
        Sprite sprite = null;

        if (piece is PawnPiece)
        {
            sprite = (piece.Color == PieceColor.White) ? lightPawn : darkPawn;
            Debug.Log($"Sprite {sprite} is has been assigned to {piece}");
        }
        else if (piece is RookPiece)
            sprite = (piece.Color == PieceColor.White) ? lightRook : darkRook;
        else if (piece is KnightPiece)
            sprite = (piece.Color == PieceColor.White) ? lightKnight : darkKnight;
        else if (piece is BishopPiece)
            sprite = (piece.Color == PieceColor.White) ? lightBishop : darkBishop;
        else if (piece is QueenPiece)
            sprite = (piece.Color == PieceColor.White) ? lightQueen : darkQueen;
        else if (piece is KingPiece)
            sprite = (piece.Color == PieceColor.White) ? lightKing : darkKing;

        if (sprite != null)
        {
            return sprite;
        }
        else
        {
            Debug.Log("Sprite cannout be returned because it is null");
            return null;
        }
    }

    public void ApplyVisualMovement(GameObject clickedObject, Vector2Int mouseGridPos)
    {
        // move element visually
        clickedObject.transform.position =
            new Vector3(mouseGridPos.x, mouseGridPos.y, clickedObject.transform.position.z);

        // clear previous position visually
        var sr = clickedObject.GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.white;

        clickedObject = null;
        // GameManager.instance.tiles.ClearHighlights();
    }

    public void CapturePieceVisually(Vector2Int piecePos, List<PieceBase> allPieces)
    {
        foreach (PieceBase logicalPiece in allPieces)
        {
            if (logicalPiece.Position == piecePos)
                if (pieceToVisualPiece.TryGetValue(logicalPiece, out PieceVisualItem pieceVisualItem))
                {
                    if (pieceVisualItem != null)
                    {
                        // pieceGameObjects.Remove(pieceGo);
                        pieceToVisualPiece.Remove(logicalPiece);
                        Destroy(pieceVisualItem);
                    }
                }
        }
    }
}