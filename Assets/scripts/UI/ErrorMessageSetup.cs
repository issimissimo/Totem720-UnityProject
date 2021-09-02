using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageSetup : MonoBehaviour
{
    [SerializeField] Text errorText;
    [SerializeField] GameObject closeButton;
    [SerializeField] Color colorError;
    [SerializeField] Color colorWarning;


    private int prefabNumber;

    public void Setup(ErrorManager.TYPE type, int _prefabNumber, string text)
    {
        /// set message
        errorText.text = text;

        /// set close button
        if (type == ErrorManager.TYPE.ERROR) closeButton.SetActive(false);

        /// set backgound color
        Image img = gameObject.GetComponent<Image>();
        Color clr = type == ErrorManager.TYPE.ERROR ? colorError : colorWarning;
        img.color = clr;

        prefabNumber = _prefabNumber;
    }

    public void Close()
    {
        ErrorManager.instance.CloseErrorPrefabPanel(prefabNumber);
    }
}
