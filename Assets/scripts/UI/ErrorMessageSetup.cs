using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageSetup : MonoBehaviour
{
    [SerializeField] Text errorText;
    [SerializeField] Image icon;
    [SerializeField] Sprite textureError;
    [SerializeField] Sprite textureWarning;
    [SerializeField] GameObject closeButton;

    private int prefabNumber;

    public void Setup(ErrorManager.TYPE type, int _prefabNumber, string text)
    {
        /// set message
        errorText.text = text;

        /// set icon
        Sprite tex = type == ErrorManager.TYPE.ERROR ? textureError : textureWarning;
        icon.sprite = tex;

        /// set close button
        if (type == ErrorManager.TYPE.ERROR) closeButton.SetActive(false);

        prefabNumber = _prefabNumber;
    }

    public void Close()
    {
        Debug.Log("chiudo numero: " + prefabNumber);
        ErrorManager.instance.CloseErrorPrefabPanel(prefabNumber);
    }
}
