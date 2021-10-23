using UnityEngine;
using System;
using System.IO;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FileManager fileManager;
    public UiManager uiManager;
    public VideoManager videoManager;
    public WebcamManager webcamManager;
    public ScreenshotHandler screenshotHandler;
    public PrinterHandler printerHandler;

    public enum GAMESTATE
    {
        INIT, IDLE, GAME, END
    }

    public static GAMESTATE STATE;

    private Coroutine wait;

    public bool printImage;





    ///
    /// variables to use in game
    ///
    private int videoToLaunch;
    public static string screenshotPath;
    private byte[] returnedBytesFromScreenshot;
    private int photoShootTrials = 0;




    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;

        STATE = GAMESTATE.INIT;
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
        if (Globals.scenarioIsDefined && Globals.squadraIsDefined)
        {
            uiManager.ShowPanelByType(Globals._SCENARIO, Globals._SQUADRA);
        }
        else
        {
            uiManager.ShowInitPanel();
        }
    }


    public void ShowPanel(GameObject panel, float skipTime = 60)
    {
        uiManager.ShowPanel(panel);

        if (STATE != GAMESTATE.INIT)
        {
            SkipAfterTime(skipTime);
        }
    }


    public void ShowPanelByType(Globals.Scenario scenario, Globals.Squadra squadra)
    {
        /// if inputs are different from the stored ones,
        /// (write them in the config.json)
        // if (scenario != Globals._SCENARIO || squadra != Globals._SQUADRA)
        // {

        STATE = GAMESTATE.IDLE;

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
        print("StartGame");

        // STATE = GAMESTATE.GAME;

        videoToLaunch = videoNumber;

        /// show instructions
        ShowPanel(uiManager.instructions);

        /// play webcam earlier
        webcamManager.Play();
    }



    //////////////////////////////////////////
    /// PHOTO
    //////////////////////////////////////////
    public void Session_PHOTO()
    {
        STATE = GAMESTATE.GAME;

        if (wait != null) StopCoroutine(wait);

        photoShootTrials++;

        string videoUrl = fileManager.GetFile(Globals._SCENARIO, Globals._SQUADRA, videoToLaunch);
        if (videoUrl != null)
        {
            /// play webcam again (if we are taking a 2nd photo)
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

                        /// pause webcam
                        webcamManager.Pause();

                        /// show panel to ask if satisfied
                        if (photoShootTrials < 2)
                        {
                            ShowPanel(uiManager.satisfied);
                        }
                        else
                        {
                            Session_PAYMENT();
                        }
                    });
                });
            });
        }
    }



    //////////////////////////////////////////
    /// PAYMENT
    //////////////////////////////////////////
    public void Session_PAYMENT()
    {
        if (wait != null) StopCoroutine(wait);

        /// TO DO......


        Session_PRINT();
    }





    //////////////////////////////////////////
    /// PRINT
    //////////////////////////////////////////
    public void Session_PRINT()
    {
        if (printImage)
            printerHandler.PrintBytes(returnedBytesFromScreenshot);

        Session_ASKFOREMAIL();
    }




    // //////////////////////////////////////////
    // /// Start final session
    // //////////////////////////////////////////
    // public void StartFinalSession()
    // {
    //     if (wait != null) StopCoroutine(wait);

    //     /// reset trials
    //     photoShootTrials = 0;

    //     /// stop webcam
    //     webcamManager.Stop();


    //     /// payment request
    //     ////
    //     ////




    //     /// print image
    //     if (printImage)
    //         printerHandler.PrintBytes(returnedBytesFromScreenshot);



    //     /////////////////////////////////////////////////////
    //     Session_ASKFOREMAIL();



    //     /// send email (if liked)
    //     uiManager.ShowEmail(screenshotPath, () =>
    //     {
    //         /// return to main UI
    //         ShowInit();

    //     });
    // }



    //////////////////////////////////////////
    /// ASK FOR EMAIL
    //////////////////////////////////////////
    public void Session_ASKFOREMAIL()
    {
        STATE = GAMESTATE.END;
        ShowPanel(uiManager.askForEmail);
    }



    //////////////////////////////////////////
    /// EMAIL
    //////////////////////////////////////////
    public void Session_EMAIL()
    {
        ShowPanel(uiManager.email, 180);
    }


    //////////////////////////////////////////
    /// END
    //////////////////////////////////////////
    public void Session_END()
    {
        SkipAfterTime(1);
    }




    public void ReturnToMain()
    {
        if (wait != null) StopCoroutine(wait);

        /// reset trials
        photoShootTrials = 0;

        /// stop webcam
        webcamManager.Stop();

        /// return to main UI
        ShowInit();
    }



    ///
    /// Skip after time...
    ///
    public void SkipAfterTime(float time)
    {
        print("SkipAfterTime: " + time);

        if (wait != null) StopCoroutine(wait);
        wait = StartCoroutine(SkipAfterTimeCoroutine(time));
    }
    private IEnumerator SkipAfterTimeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        if (STATE == GAMESTATE.END) uiManager.ShowPanel(uiManager.endGame);
        else uiManager.ShowPanel(uiManager.timeExpired);

        yield return new WaitForSeconds(5);
        ReturnToMain();

        wait = null;
    }
}
