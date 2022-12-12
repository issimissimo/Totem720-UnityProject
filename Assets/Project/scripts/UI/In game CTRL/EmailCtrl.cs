using UnityEngine;
using UnityEngine.UI;

public class EmailCtrl : MonoBehaviour
{
    public GameObject form;
    public GameObject sending;
    public InputField emailInputField;
    public Button sendEmailButton;
    public EmailHandler emailHandler;


    void Start()
    {
        emailInputField.onValueChanged.AddListener(ValidateEmail);
    }

    void OnEnable()
    {
        form.SetActive(true);
        sending.SetActive(false);
        emailInputField.text = "";

        if (GameManager.STATE == GameManager.GAMESTATE.GAME)
            GameManager.instance.uiManager.SetForeground(form);
    }

    public void Send()
    {
        string to = emailInputField.text;
        print(to);
        emailHandler.Send(GameManager.screenshotPath, to);

        /// end
        form.SetActive(false);
        sending.SetActive(true);

        GameManager.instance.uiManager.SetForeground(sending);

        GameManager.instance.uiManager.backgroundVideo.SetActive(true);

        GameManager.instance.SkipAfterTime(10);
    }


    private void ValidateEmail(string st)
    {
        sendEmailButton.interactable = Utils.ValidateEmail(st) ? true : false;
    }


}
