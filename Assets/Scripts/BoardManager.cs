using UnityEngine;

public class BoardManager : MonoBehaviour
{
    GameObject[,] tileObjects;
    public GameObject LightTilePrefab;
    public GameObject DarkTilePrefab;
    GameObject tile;
    ClickHandler handler;
    public Piece[,] board = new Piece[8, 8];


    void Start()
    {
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

    public void HighlightTiles(int x, int y)
    {

        var sr = tileObjects[x,y].GetComponent<SpriteRenderer>();
        if (tile.CompareTag("Tile"))
        {
            sr.color = Color.yellow;
            Debug.Log($"Tile {x}, {y} highlighted.");
        }
    }
        


}
