using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BoardVisuals : MonoBehaviour
{
    public GameObject lightPawnPrefab;
    public GameObject darkPawnPrefab;
    public GameObject lightRookPrefab;
    public GameObject darkRookPrefab;
    public GameObject lightKnightPrefab;
    public GameObject darkKnightPrefab;
    public GameObject lightBishopPrefab;
    public GameObject darkBishopPrefab;
    public GameObject lightQueenPrefab;
    public GameObject darkQueenPrefab;
    public GameObject lightKingPrefab;
    public GameObject darkKingPrefab;

    PieceBase _corePiece;
    public int boardSize = 8;


    GameManager gameManager;


    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void SpawnPieces(List<PieceBase> pieces)
    {
        // for (int i = 0; i < boardSize; i++)
        // {
        //     for (int j = 0; j < boardSize; j++)
        //     {
        //         _corePiece = board.pieces[i, j];
        //         if (_corePiece != null)
        //         {
        //             SpawnPiece(i, j, _corePiece);
        //         }
        //     }
        // }
        foreach (PieceBase piece in pieces)
        {
            SpawnPiece(piece);
        }
    }

    void SpawnPiece(PieceBase piece)
    {
        GameObject prefab = null;

        if (piece is PawnPiece)
            prefab = (piece.Color == PieceColor.White) ? lightPawnPrefab : darkPawnPrefab;
        else if (piece is RookPiece)
            prefab = (piece.Color == PieceColor.White) ? lightRookPrefab : darkRookPrefab;
        else if (piece is KnightPiece)
            prefab = (piece.Color == PieceColor.White) ? lightKnightPrefab : darkKnightPrefab;
        else if (piece is BishopPiece)
            prefab = (piece.Color == PieceColor.White) ? lightBishopPrefab : darkBishopPrefab;
        else if (piece is QueenPiece)
            prefab = (piece.Color == PieceColor.White) ? lightQueenPrefab : darkQueenPrefab;
        else if (piece is KingPiece)
            prefab = (piece.Color == PieceColor.White) ? lightKingPrefab : darkKingPrefab;


        if (prefab != null)
        {
            
            Vector2 prefabPos = new Vector2(piece.Position.x, piece.Position.y);
            Vector2 prefabPosition = new Vector2(piece.Position.x, piece.Position.y);
            
            GameObject visualPiece = Instantiate(prefab, prefabPosition, Quaternion.identity);
            var visual = visualPiece.GetComponent<PieceVisual>();
            gameManager.AssignPrefabsToPieces(piece, visualPiece, visual);
            gameManager.pieceToGameObject[piece] = visualPiece;
            
            
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
        gameManager.tiles.ClearHighlights();
    }
}