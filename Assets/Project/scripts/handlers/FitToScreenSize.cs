using UnityEngine;

public class FitToScreenSize : MonoBehaviour
{
    public bool swapHeightAndWidth = false;

    void Start()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;

        if (!swapHeightAndWidth)
            transform.localScale = new Vector3(width, height, 1);
        else
            transform.localScale = new Vector3(height, width, 1);
    }
}
