using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class EmailCtrl : MonoBehaviour
{
    public OnScreenKeyboard oks;
    public GameObject panelEmail;
    public GameObject panelRequest;
    public GameObject panelSendemail;
    public InputField emailInputField;
    public Button sendEmailButton;
    private CanvasGroup panelRequestCg;



    private Action _callback;
    private string _screenshot;


    private void Awake()
    {
        panelRequestCg = panelRequest.GetComponent<CanvasGroup>();
    }


    void Start()
    {
        Hide();
        emailInputField.onValueChanged.AddListener(ValidateEmail);
        sendEmailButton.onClick.AddListener(Send);
    }

    public void Show(string filePath, Action callback)
    {
        _screenshot = filePath;
        _callback = callback;

        panelEmail.SetActive(true);
        panelRequest.SetActive(true);
        panelSendemail.SetActive(false);
        // sendEmailButton.interactable = false;
        emailInputField.text = "";
    }

    public void Hide()
    {
        panelEmail.SetActive(false);
    }

    public void OpenForSend()
    {
        panelRequest.SetActive(false);
        panelSendemail.SetActive(true);

        /// when is finished
        // _callback();
    }

    public void Send()
    {
        string to = emailInputField.text;
        print(to);
        GameManager.instance.emailHandler.Send(_screenshot, to);
        Finished();
    }


    public void Dismiss()
    {
        Finished();
    }


    private void Finished()
    {
        Hide();
        _callback();
    }

    private void ValidateEmail(string st)
    {
        sendEmailButton.interactable = Utils.ValidateEmail(st) ? true : false;
    }


}
