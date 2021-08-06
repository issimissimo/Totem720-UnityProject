using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager instance;
    [SerializeField] GameObject panelError;
    [SerializeField] Text textError;
    [SerializeField] GameObject closeButton;

    private void Awake()
    {
        instance = this;
        panelError.SetActive(false);
    }

    public void ShowError(string msg, bool isBlocking = false)
    {
        Debug.LogError(msg);
        panelError.SetActive(true);
        textError.text = msg;
        if (!isBlocking) closeButton.SetActive(true);
        else closeButton.SetActive(false);
    }

    public void ClosePanel()
    {
        panelError.SetActive(false);
    }

}
