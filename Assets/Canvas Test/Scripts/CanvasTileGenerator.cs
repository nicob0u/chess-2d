using UnityEngine;
using UnityEngine.Rendering;

public class CanvasTileGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    private Vector3 position;
    public RectTransform boardPanel;
    void Start()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                position = new Vector3(i, j, 0);
                var tile =  Instantiate(tilePrefab, position, Quaternion.identity);

            }
        }
        
    }
    
}
