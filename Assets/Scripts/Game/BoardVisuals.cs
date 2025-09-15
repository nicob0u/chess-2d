using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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

    [HideInInspector] public Dictionary<int, int> pieceToVisualPiece = new Dictionary<int, int>();


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
        var pieceId = piece.PieceId;

        if (sprite != null)
        {
            Debug.Log($"Sprite {sprite} is present");
            Vector3 spritePosition = new Vector3(piece.Position.x, piece.Position.y, 0);

            PieceVisualItem visualPiece;
            if (pieceToVisualPiece.ContainsKey(pieceId))
            {
                PieceVisualItem visual = FindObjectsOfType<PieceVisualItem>()
                    .FirstOrDefault(v => v.pieceVisualId == pieceId);
                Destroy(visual.gameObject);
                pieceToVisualPiece.Remove(pieceId);
                Debug.Log($"{pieceId} has been removed");
            }

            visualPiece = Instantiate(pieceVisualPrefab, spritePosition, Quaternion.identity, this.transform);
            visualPiece.tag = "Piece";
            visualPiece.pieceVisualId = piece.PieceId;
            visualPiece.transform.SetAsLastSibling();
            pieceToVisualPiece[pieceId] = visualPiece.pieceVisualId;
            visualPiece.Init(spritePosition, sprite);


            Debug.LogWarning($"{piece} visual instantiated.");
        }
    }

    Sprite FindPieceSprite(PieceBase piece)
    {
        Sprite sprite = null;

        if (piece.GetLogicType() == typeof(PawnPiece))
        {
            sprite = (piece.Color == PieceColor.White) ? lightPawn : darkPawn;
            Debug.Log($"Sprite {sprite} is has been assigned to {piece}");
        }
        else if (piece.GetLogicType() == typeof(RookPiece))
            sprite = (piece.Color == PieceColor.White) ? lightRook : darkRook;
        else if (piece.GetLogicType() == typeof(KnightPiece))
            sprite = (piece.Color == PieceColor.White) ? lightKnight : darkKnight;
        else if (piece.GetLogicType() == typeof(BishopPiece))
            sprite = (piece.Color == PieceColor.White) ? lightBishop : darkBishop;
        else if (piece.GetLogicType() == typeof(QueenPiece))
            sprite = (piece.Color == PieceColor.White) ? lightQueen : darkQueen;
        else if (piece.GetLogicType() == typeof(KingPiece))
            sprite = (piece.Color == PieceColor.White) ? lightKing : darkKing;

        if (sprite != null)
        {
            return sprite;
        }
        else
        {
            Debug.Log("Sprite cannot be returned because it is null");
            return null;
        }
    }

    public void CapturePieceVisually(List<PieceBase> capturedPieces)
    {
        foreach (var capturedPiece in capturedPieces)
        {
            PieceVisualItem visual = FindObjectsOfType<PieceVisualItem>()
                .FirstOrDefault(v => v.pieceVisualId == capturedPiece.PieceId);
            if (visual != null)
                Destroy(visual.gameObject);
            pieceToVisualPiece.Remove(capturedPiece.PieceId);
        }
    }

    void HighlightPiece()
    {
        
    }

}