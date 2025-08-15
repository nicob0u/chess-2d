using UnityEngine;
using System.Collections.Generic;


public enum PieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}
public enum PieceColor
{
    Light,
    Dark
}

public class Piece : MonoBehaviour
{
    public PieceType pieceType;
    public PieceColor pieceColor;
    [HideInInspector]
    public Vector2Int currentPos;

    GameManager gameManager;
    BoardManager boardManager;

    void Start()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        gameManager = FindFirstObjectByType<GameManager>();

    }

    public void GetMoves()
    {

        switch (pieceType)
        {
            case PieceType.Pawn:
                GetPawnMoves(boardManager.board, gameManager.boardPosition.x, gameManager.boardPosition.y);
                break;



        }

    }

    //public Piece GetPiecePos(int x, int y)
    //{
    //    if (x < 0 || x >= 8 || y < 0 || y >= 8)
    //        return null;

    //    return board[x, y];
    //}


    public List<Vector2Int> GetPawnMoves(Piece[,] board, int x, int y)
    {
        var moves = new List<Vector2Int>();

        for (int i = x + 1; i < 8; i++)
        {

                if (board[i, y] == null)
                {
                    var allowedMove = new Vector2Int(i, y);
                    boardManager.HighlightTiles(allowedMove.x, allowedMove.y);
                    moves.Add(allowedMove);

                }
            

        }
        return moves;

    }


}
