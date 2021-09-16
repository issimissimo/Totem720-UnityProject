using UnityEngine;
using UnityEngine.UI;
using System;

public class ErrorMessageSetup : MonoBehaviour
{
    [SerializeField] Text errorText;
    [SerializeField] GameObject closeButton;
    [SerializeField] Text closeButtonText;
    [SerializeField] Color colorError;
    [SerializeField] Color colorWarning;
    [SerializeField] Color colorInfo;


    private int prefabNumber;
    private Action onButtonClickCallback;

    public void Setup(ErrorManager.TYPE type, int _prefabNumber, string text, string buttonText, Action buttonAction)
    {
        /// set message
        errorText.text = text;

        /// set close button
        if (type == ErrorManager.TYPE.ERROR && buttonText == null && buttonAction == null)
        {
            closeButton.SetActive(false);
        }
        else
        {
            closeButton.SetActive(true);
            if (buttonText != null) closeButtonText.text = buttonText;
            if (buttonAction != null)
            {
                onButtonClickCallback = buttonAction;
                closeButton.GetComponent<Button>().onClick.AddListener(OnButtonClick);
            }
        }

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

    private void OnButtonClick()
    {
        // Close();
        onButtonClickCallback();
    }
}
