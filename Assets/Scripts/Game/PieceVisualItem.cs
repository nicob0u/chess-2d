using UnityEngine;
using UnityEngine.UI;

public class PieceVisualItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [HideInInspector] public int pieceVisualId = 0;
    public void Init(Vector3 pos,Sprite pieceImage)
    {
        transform.position = pos;
        image.sprite = pieceImage;
        image.raycastTarget = false;


    }
}