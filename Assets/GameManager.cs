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

    private void Awake() {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    void Start()
    {
        // // /// TEST FOR ERROR MANAGER!!!!!!
        // ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "pluto");
        // ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "pippo");
        // // ////
        
        
        /// Check for video path exist
        FileManager.CheckDirectory(defVideoPath, ErrorManager.TYPE.WARNING);

        FileManager.defPath = defVideoPath;

        /// Check for Internet available
        InternetConnection.instance.Check(ErrorManager.TYPE.WARNING);

        /// show main UI
        GoToMainUi();
    }


    public void GoToMainUi(){
        uiManager.ShowMainPanel();
    }

    public void StartGameSession(Globals.Scenario scenario, Globals.Squadra squadra, int videoNumber){

        /// play webcam

        /// play video
        string videoUrl = fileManager.GetFile(scenario, squadra, videoNumber);
        videoManager.Play(videoUrl);
    }


    void StartPhotoSession(){

    }

    void StartMailSession(){

    }


}
