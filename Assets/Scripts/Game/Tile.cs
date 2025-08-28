using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        GameManager gameManager;
        public Vector2Int tilePosition;

        void Start()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(tilePosition);
            gameManager.ToggleSelection(tilePosition);
        }
    }
}