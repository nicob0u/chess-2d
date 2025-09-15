using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TileGenerator : MonoBehaviour
    {
        public Tile blackTile;
        public Tile whiteTile;
        private int boardSize = 8;
        Tile tile;
        private Tile[,] tileObjects = new Tile[8, 8];
        public Canvas canvas;

        void Awake()
        {
            tile = FindAnyObjectByType<Tile>();
        }

        public void Init()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Vector2 pos = new Vector2(i, j);
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        tile = Instantiate(blackTile, pos, Quaternion.identity);
                        tile.Init(new Vector2Int(i, j));
                    }
                    else
                    {
                        tile = Instantiate(whiteTile, pos, Quaternion.identity);
                        // tile.tag = "Tile";
                        tile.Init(new Vector2Int(i, j));
                    }

                    tile.transform.SetParent(this.transform);
                    tile.transform.SetAsFirstSibling();
                    tile.name = $"tile({i},{j})";

                    tileObjects[i, j] = tile;
                }
            }
        }

        public void HighlightMoves(List<Vector2Int> moves)
        {
            foreach (Vector2Int move in moves)
            {
                tileObjects[move.x, move.y].GetComponent<Image>().color = Color.yellow;
            }
        }

        public void ClearHighlights(List<Vector2Int> moves)
        {
            foreach (Vector2Int move in moves)
            {
                if (tileObjects[move.x, move.y].GetComponent<Image>().color == Color.yellow)
                    tileObjects[move.x, move.y].GetComponent<Image>().color = Color.white;
            }
        }
    }
}