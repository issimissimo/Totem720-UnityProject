using UnityEngine;
using UnityEngine.UI;


public class ButtonBase : MonoBehaviour
{
    protected Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public virtual void Start()
    {
        button.onClick.AddListener(onClick);
    }


    public virtual void onClick()
    {
        ///to be extended
    }
}