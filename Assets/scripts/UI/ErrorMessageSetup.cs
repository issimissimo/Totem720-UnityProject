using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageSetup : MonoBehaviour
{
    [SerializeField] Text errorText;
    [SerializeField] GameObject closeButton;
    [SerializeField] Color colorError;
    [SerializeField] Color colorWarning;
    [SerializeField] Color colorInfo;


    private int prefabNumber;

    public void Setup(ErrorManager.TYPE type, int _prefabNumber, string text)
    {
        /// set message
        errorText.text = text;

        /// set close button
        if (type == ErrorManager.TYPE.ERROR) closeButton.SetActive(false);

        /// set backgound color
        Image img = gameObject.GetComponent<Image>();
        Color color = colorError;
        if (type == ErrorManager.TYPE.ERROR) color = colorError;
        if (type == ErrorManager.TYPE.WARNING) color = colorWarning;
        if (type == ErrorManager.TYPE.INFO) color = colorInfo;
        img.color = color;

        prefabNumber = _prefabNumber;
    }

    public void Close()
    {
        ErrorManager.instance.CloseErrorPrefabPanel(prefabNumber);
    }
}
