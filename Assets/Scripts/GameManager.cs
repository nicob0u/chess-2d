using UnityEngine;

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

    public Vector2Int boardPosition;
    BoardManager boardManager;
    Piece[,] board;
    public int boardSize = 8;

    void Start()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        board = new Piece[boardSize, boardSize];

        SetUpPieces();

    }
    void SetUpPieces()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (y == 1 || y == 6)
                    SpawnPawn(x, y, y == 1 ? PieceColor.Light : PieceColor.Dark);

                if (y == 0 || y == 7)
                {
                    if (x == 0 || x == 7)
                        SpawnRook(x, y, y == 0 ? PieceColor.Light : PieceColor.Dark);

                    if (x == 1 || x == 6)
                        SpawnKnight(x, y, y == 0 ? PieceColor.Light : PieceColor.Dark);

                    if (x == 2 || x == 5)
                        SpawnBishop(x, y, y == 0 ? PieceColor.Light : PieceColor.Dark);

                    if (x == 3)
                        SpawnKing(x, y, y == 0 ? PieceColor.Light : PieceColor.Dark);

                    if (x == 4)
                        SpawnQueen(x, y, y == 0 ? PieceColor.Light : PieceColor.Dark);

                }


            }



        }

    }


    void SpawnPawn(int x, int y, PieceColor pieceColor)
    {
        GameObject pawnPrefab = pieceColor == PieceColor.Light ? lightPawnPrefab : darkPawnPrefab;
        Vector2 pos = new Vector2(x, y);
        boardPosition = new Vector2Int(x, y);
        GameObject pawnPiece = Instantiate(pawnPrefab, pos, Quaternion.identity);
        pawnPiece.tag = "Piece";
        Debug.Log($"{pieceColor} pawn spawned at [{x} , {y}]");

        Piece pieceComponent = pawnPiece.GetComponent<Piece>();
        pieceComponent.currentPos = new Vector2Int(x, y);
        boardManager.board[x, y] = pieceComponent;

    }
    void SpawnRook(int x, int y, PieceColor pieceColor)
    {
        GameObject rookPrefab = pieceColor == PieceColor.Light ? lightRookPrefab : darkRookPrefab;
        Vector2 pos = new Vector2(x, y);
        GameObject rookPiece = Instantiate(rookPrefab, pos, Quaternion.identity);
        rookPiece.tag = "Piece";
        Debug.Log($"{pieceColor} rook spawned at [{x} , {y}]");

    }
    void SpawnKnight(int x, int y, PieceColor pieceColor)
    {
        GameObject knightPrefab = pieceColor == PieceColor.Light ? lightKnightPrefab : darkKnightPrefab;
        Vector2 pos = new Vector2(x, y);
        GameObject knightPiece = Instantiate(knightPrefab, pos, Quaternion.identity);
        knightPiece.tag = "Piece";
        Debug.Log($"{pieceColor} knight spawned at [{x} , {y}]");

    }
    void SpawnBishop(int x, int y, PieceColor pieceColor)
    {
        GameObject bishopPrefab = pieceColor == PieceColor.Light ? lightBishopPrefab : darkBishopPrefab;
        Vector2 pos = new Vector2(x, y);
        GameObject bishopPiece = Instantiate(bishopPrefab, pos, Quaternion.identity);
        bishopPiece.tag = "Piece";
        Debug.Log($"{pieceColor} bishop spawned at [{x} , {y}]");

    }
    void SpawnQueen(int x, int y, PieceColor pieceColor)
    {
        GameObject queenPrefab = pieceColor == PieceColor.Light ? lightQueenPrefab : darkQueenPrefab;
        Vector2 pos = new Vector2(x, y);
        GameObject queenPiece = Instantiate(queenPrefab, pos, Quaternion.identity);
        queenPiece.tag = "Piece";
        Debug.Log($"{pieceColor} queen spawned at [{x} , {y}]");

    }
    void SpawnKing(int x, int y, PieceColor pieceColor)
    {
        GameObject kingPrefab = pieceColor == PieceColor.Light ? lightKingPrefab : darkKingPrefab;
        Vector2 pos = new Vector2(x, y);
        GameObject kingPiece = Instantiate(kingPrefab, pos, Quaternion.identity);
        kingPiece.tag = "Piece";
        Debug.Log($"{pieceColor} king spawned at [{x} , {y}]");

    }


    public void HighlightPiece(GameObject clickedObject)
    {
        var sr = clickedObject.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
    }

}
