using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    GameObject[,] tileObjects;
    public GameObject LightTilePrefab;
    public GameObject DarkTilePrefab;
    GameObject tile;
    public Board board;
    private ClickHandler handler;
    public bool isHightlighted;

    void Awake()
    {
        board = new Board();
        board.Init();
    }
    void Start()
    {
;
        tileObjects = new GameObject[8, 8];
        CreateBoard();

        handler = FindFirstObjectByType<ClickHandler>();
    }

    void CreateBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector2 pos = new Vector2(x, y);
                if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0))
                {
                    tile = Instantiate(DarkTilePrefab, pos, Quaternion.identity);
                    tile.tag = "Tile";
                }
                else
                {
                    tile = Instantiate(LightTilePrefab, pos, Quaternion.identity);
                    tile.tag = "Tile";
                }

                tile.transform.SetParent(this.transform);
                tile.name = $"tile({x},{y})";

                tileObjects[x, y] = tile;
            }
        }
    }

    public void HighlightTiles(GameObject clickedObject, int x, int y)
    {
        if (tile == null)
            Debug.Log("Tile null");
        if (board == null)
            Debug.Log("Board null");

        var piece = clickedObject.GetComponent<PieceVisual>().corePiece;

        if (piece == null)
            Debug.Log("Piece null");

        List<Vector2Int> allowedMoves = piece.GetMoves(board.pieces, x, y);

        foreach (Vector2Int move in allowedMoves)
        {
            
            var sr = tileObjects[move.x, move.y].GetComponent<SpriteRenderer>();

            sr.color = Color.yellow;
        }
    }

    public void ClearHighlights()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                var sr = tileObjects[x, y].GetComponent<SpriteRenderer>();
                sr.color = Color.white;
            }
        }
    }
}