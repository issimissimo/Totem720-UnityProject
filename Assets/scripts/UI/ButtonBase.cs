using UnityEngine;
using UnityEngine.UI;


public class ButtonBase : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(onClick);
    }


    public virtual void onClick()
    {
        ///to be extended
    }
}