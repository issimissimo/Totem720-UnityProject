using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FileManager fileManager;
    public UiManager uiManager;
    public VideoManager videoManager;
    public WebcamManager webcamManager;
    public ScreenshotHandler screenshotHandler;
    public EmailHandler emailHandler;
    public PrinterHandler printerHandler;

    public enum GAMESTATE
    {
        IDLE, GAME
    }

    public static GAMESTATE STATE;

    public bool printImage;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    void Start()
    {
        /// Check for config file exist
        fileManager.CheckConfigFile((result) =>
        {
            if (result)
            {
                /// Check for video path specified
                /// in the config file exist
                fileManager.CkeckDefaultPath();

                /// Check for Internet available
                InternetConnection.instance.Check(ErrorManager.TYPE.WARNING);

                /// show main UI
                ShowInit();
            }
        });
    }


    public void Quit()
    {
        Application.Quit();
    }


    public void ShowInit()
    {
        STATE = GAMESTATE.IDLE;

        if (Globals.scenarioIsDefined && Globals.squadraIsDefined)
        {
            uiManager.ShowPanelByType(Globals._SCENARIO, Globals._SQUADRA);
        }
        else
        {
            uiManager.ShowInitPanel();
        }
    }


    public void ShowPanelByType(Globals.Scenario scenario, Globals.Squadra squadra)
    {

        /// if inputs are different from the stored ones,
        /// write them in the config.json 
        if (scenario != Globals._SCENARIO || squadra != Globals._SQUADRA)
        {
            Globals._SCENARIO = scenario;
            Globals._SQUADRA = squadra;
            fileManager.UpdateConfigFile();
        }

        ShowInit();
    }


    //////////////////////////////////////////
    /// Start game
    //////////////////////////////////////////
    public void StartGame(int videoNumber)
    {
        STATE = GAMESTATE.GAME;

        StartPhotoSession(videoNumber);
    }


    //////////////////////////////////////////
    /// Start photo session
    //////////////////////////////////////////
    private void StartPhotoSession(int videoNumber)
    {
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
                    screenshotHandler.TakeScreenshot(Screen.width, Screen.height, (returnedBytesFromScreenshot) =>
                    {
                        StartFinalSession(returnedBytesFromScreenshot);
                    });
                });
            });
        }
    }

    //////////////////////////////////////////
    /// Start final session
    //////////////////////////////////////////
    private void StartFinalSession(byte[] returnedBytesFromScreenshot)
    {
        /// payment request


        /// print image
        if (printImage)
            printerHandler.PrintBytes(returnedBytesFromScreenshot);

        /// send email (if liked)
        uiManager.ShowEmail(() =>
        {
            /// return to main UI
            ShowInit();

        });
    }
}
