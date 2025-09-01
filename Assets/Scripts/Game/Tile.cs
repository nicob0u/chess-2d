using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        Vector2Int tilePosition;



        public void Init(Vector2Int tilePosition)
        {
            this.tilePosition = tilePosition;
        }
        
        public void OnPointerClick([CanBeNull] PointerEventData eventData)
        {
            GameManager.instance.OnTileClicked(tilePosition);
            Debug.Log(name);
        }
    }
}