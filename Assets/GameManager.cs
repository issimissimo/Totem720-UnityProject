﻿using System.Collections;
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
        /// TEST FOR ERROR MANAGER!!!!!!
        ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "pippo");
        ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "pluto");
        ////
        
        
        /// Check for video path exist (blocking)
        if (!FileManager.CheckDirectory(defVideoPath, true))
            return;

        FileManager.defPath = defVideoPath;

        /// Check for Internet available (not blocking)
        InternetConnection.newCheck();

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
