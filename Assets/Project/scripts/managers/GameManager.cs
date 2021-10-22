using UnityEngine;
using System.IO;

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





    ///
    /// private variables to use in game
    ///
    private int videoToLaunch;
    private string screenshotPath;
    private byte[] returnedBytesFromScreenshot;
    private int photoShootTrials = 0;





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


    //////////////////////////////////////////
    /// Init
    //////////////////////////////////////////
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
        /// (write them in the config.json)
        // if (scenario != Globals._SCENARIO || squadra != Globals._SQUADRA)
        // {

        /// set the new scenario
        Globals._SCENARIO = scenario;

        /// if the previuos "squadra" was NOT Inter_Milan,
        /// set the new squadra
        if (Globals._SQUADRA != Globals.Squadra.Inter_Milan)
        {
            Globals._SQUADRA = squadra;
        }

        uiManager.ShowPanelByType(scenario, squadra);
    }


    //////////////////////////////////////////
    /// Start game
    //////////////////////////////////////////
    public void StartGame(int videoNumber)
    {
        STATE = GAMESTATE.GAME;

        videoToLaunch = videoNumber;

        /// play webcam earlier
        webcamManager.Play();

        /// show instructions
        uiManager.ShowPanel(uiManager.instructions);


        // StartPhotoSession(videoNumber);
    }





    //////////////////////////////////////////
    /// Start photo session
    //////////////////////////////////////////
    public void StartPhotoSession()
    {
        photoShootTrials ++;
        
        string videoUrl = fileManager.GetFile(Globals._SCENARIO, Globals._SQUADRA, videoToLaunch);
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
                    // /// pause webcam
                    // webcamManager.Pause();

                    /// take screenshot
                    System.DateTime now = System.DateTime.Now;
                    long fileCreationTime = now.ToFileTime();
                    screenshotPath = Path.Combine(Globals.screenshotFolder, fileCreationTime + ".jpg");

                    screenshotHandler.TakeScreenshot(Screen.width, Screen.height, screenshotPath, (_returnedBytesFromScreenshot) =>
                    {
                        returnedBytesFromScreenshot = _returnedBytesFromScreenshot;

                        // StartFinalSession(screenshotPath, returnedBytesFromScreenshot);

                        /// pause webcam
                        webcamManager.Pause();

                        /// show panel to ask if satisfied
                        if (photoShootTrials < 2)
                        {
                            AskForSatified();
                        }
                        else
                        {
                            StartFinalSession();
                        }


                    });
                });
            });
        }
    }



    //////////////////////////////////////////
    /// Ask for satisfied
    //////////////////////////////////////////
    public void AskForSatified()
    {
        uiManager.ShowPanel(uiManager.satisfied);
    }



    //////////////////////////////////////////
    /// Start final session
    //////////////////////////////////////////
    public void StartFinalSession()
    {
        /// reset trials
        photoShootTrials = 0;

        /// stop webcam
        webcamManager.Stop();


        /// payment request
        ////
        ////




        /// print image
        if (printImage)
            printerHandler.PrintBytes(returnedBytesFromScreenshot);

        /// send email (if liked)
        uiManager.ShowEmail(screenshotPath, () =>
        {
            /// return to main UI
            ShowInit();

        });
    }
}
