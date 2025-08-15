using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
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
    BoardManager _boardManager;

    public int boardSize = 8;

    Board board;

    void Start()
    {
        board = new Board();
        board.Init();

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
            visual.corePiece = corePiece;
            board.pieces[x, y] = visual.corePiece;
            visual.boardPosition = new Vector2Int(x, y);

            visualPiece.tag = "Piece";
        }
    }

    public void HighlightPiece(GameObject clickedObject)
    {
        var sr = clickedObject.GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;
    }
}