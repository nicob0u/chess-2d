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

    public void SpawnPieces(Board board)
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                _corePiece = board.pieces[i, j];
                if (_corePiece != null)
                {
                    SpawnPiece(i, j, _corePiece);
                }
            }
        }
    }

    void SpawnPiece(int x, int y, PieceBase corePiece)
    {
        GameObject prefab = null;

        if (corePiece is PawnPiece)
            prefab = (corePiece.Color == PieceColor.White) ? lightPawnPrefab : darkPawnPrefab;
        else if (corePiece is RookPiece)
            prefab = (corePiece.Color == PieceColor.White) ? lightRookPrefab : darkRookPrefab;
        else if (corePiece is KnightPiece)
            prefab = (corePiece.Color == PieceColor.White) ? lightKnightPrefab : darkKnightPrefab;
        else if (corePiece is BishopPiece)
            prefab = (corePiece.Color == PieceColor.White) ? lightBishopPrefab : darkBishopPrefab;
        else if (corePiece is QueenPiece)
            prefab = (corePiece.Color == PieceColor.White) ? lightQueenPrefab : darkQueenPrefab;
        else if (corePiece is KingPiece)
            prefab = (corePiece.Color == PieceColor.White) ? lightKingPrefab : darkKingPrefab;


        if (prefab != null)
        {
            Vector2 pos = new Vector2(x, y);
            GameObject visualPiece = Instantiate(prefab, pos, Quaternion.identity);
            var visual = visualPiece.GetComponent<PieceVisual>();
            gameManager.AssignPrefabsToPieces(prefab, corePiece, x, y, visualPiece, visual);
        }
    }

    
    public void ApplyVisualMovement(GameObject clickedObject, PieceVisual pieceVisual, Vector2Int mouseGridPos)
    { 
        // move element visually
        clickedObject.transform.position =
            new Vector3(mouseGridPos.x, mouseGridPos.y, clickedObject.transform.position.z);
        pieceVisual.boardPosition = mouseGridPos;
        // clear previous position visually
        var sr = clickedObject.GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.white;

        clickedObject = null;
        gameManager.tiles.ClearHighlights();
    }
}