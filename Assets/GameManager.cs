using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string defVideoPath = "C:/Users/Daniele/Desktop/Video per Totem";

    public static GameManager instance;

    public FileManager fileManager;
    public UiManager uiManager;
    public VideoManager videoManager;
    public WebcamManager webcamManager;
    public ScreenshotHandler screenshotHandler;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    void Start()
    {
        /// Check for video path exist
        bool dir = FileManager.CheckDirectory(defVideoPath, ErrorManager.TYPE.WARNING);

        FileManager.defPath = defVideoPath;

        /// Check for Internet available
        InternetConnection.instance.Check(ErrorManager.TYPE.WARNING);

        /// show main UI
        Init();
    }


    public void Init()
    {
        uiManager.ShowInitPanel();
    }

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
                /// hide UI panels
                uiManager.HideUiContainer();

                /// wait for end of video
                videoManager.WaitForEnd(() =>
                {
                    // webcamManager.Pause();

                    // screenshotHandler.TakeScreenshot();
                });
            });



        }

    }


    void StartPhotoSession()
    {

    }

    void StartMailSession()
    {

    }


}
