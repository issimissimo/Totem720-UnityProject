using UnityEngine;

public class FitToScreenSize : MonoBehaviour
{
    public bool swapHeightAndWidth = false;
    public bool flipVertical = false;
    public bool flipHorizontal = false;

    void Awake()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;

        if (!swapHeightAndWidth)
            transform.localScale = new Vector3(width, height, 1);
        else
            transform.localScale = new Vector3(height, width, 1);


        if (flipVertical)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
        }

        if (flipHorizontal)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
    }
}
