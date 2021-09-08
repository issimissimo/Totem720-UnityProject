using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmailCtrl : MonoBehaviour
{
    public OnScreenKeyboard oks;
    public GameObject panelEmail;
    public GameObject panelRequest;

    public GameObject panelSendemail;

    private Action _callback;

    void Start()
    {
        Hide();
    }

    public void Show(Action callback)
    {
        _callback = callback;

        panelEmail.SetActive(true);
        panelRequest.SetActive(true);
        panelSendemail.SetActive(false);
    }

    public void Hide()
    {
        panelEmail.SetActive(false);
    }

    public void Send()
    {
        /// here we have to send to open the keyboard.... !!!!!!!!!!!
        ///
        ///
        
        /// when is finished....
        // _callback();
    }

    public void Dismiss()
    {
        _callback();
    }


}
