using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tiles : MonoBehaviour
{
    GameObject[,] tileObjects;
    public GameObject LightTilePrefab;
    public GameObject DarkTilePrefab;
    GameObject tile;

    void Start()
    {
        tileObjects = new GameObject[8, 8];
    }

    public void CreateTiles()
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


    public void HighlightTiles(List<Vector2Int> allowedMoves)
    {
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