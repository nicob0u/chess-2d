using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Toggle : MonoBehaviour, IPointerClickHandler
    {
        private Image image;

        void Awake()
        {
            image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("fdkjdasklfjds");
            if (image.color == Color.red)
                image.color = Color.white;
            else
                image.color = Color.red;
        }
    }
}