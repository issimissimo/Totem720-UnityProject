using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FileManager fileManager;
    public UiManager uiManager;
    public VideoManager videoManager;
    public WebcamManager webcamManager;
    public ScreenshotHandler screenshotHandler;
    public EmailHandler emailHandler;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    void Start()
    {
        /// Check for video path exist
        fileManager.CkeckDefaultPath();

        /// Check for config file exist
        fileManager.CheckConfigFile();

        /// Check for Internet available
        InternetConnection.instance.Check(ErrorManager.TYPE.WARNING);

        /// show main UI
        ShowMain();
    }

    // private void Update() {
    //     if ( Keyboard.current[Key.Space].wasPressedThisFrame){
    //         screenshotHandler.TakeScreenshot(1080, 1920, defVideoPath);
    //     }
    // }


    public void ShowMain()
    {
        /// hide UI panels
        uiManager.ShowUiContainer();

        if (Globals.scenarioIsDefined && Globals.squadraIsDefined)
        {
            uiManager.ShowPanelByType(Globals._SCENARIO, Globals._SQUADRA);
        }
        else
        {
            uiManager.ShowInitPanel();
        }
    }



    //////////////////////////////////////////
    /// Start game
    //////////////////////////////////////////
    public void StartGameSession(int videoNumber)
    {
        ///get video url
        string videoUrl = fileManager.GetFile(Globals._SCENARIO, Globals._SQUADRA, videoNumber);
        if (videoUrl != null)
        {
            /// play webcam
            webcamManager.Play();

            /// play video
            Debug.Log("LANCIO VIDEO: " + videoUrl);
            videoManager.Play(videoUrl, () =>
            {
                /// show game UI
                uiManager.ShowGame(videoManager.videoDuration);

                /// wait for end of video
                videoManager.WaitForEnd(() =>
                {
                    /// pause webcam
                    webcamManager.Pause();

                    /// take screenshot
                    screenshotHandler.TakeScreenshot(1080, 1920, FileManager.defPath, (screenshotFullName) =>
                    {
                        /// return to main UI
                        ShowMain();

                        emailHandler.Send(screenshotFullName);

                        Debug.Log(screenshotFullName);
                    });
                });
            });
        }
    }


}
