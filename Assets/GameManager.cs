﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string defVideoPath = "C:/Users/Daniele/Desktop/Video per Totem";

    [SerializeField] FileManager fileManager;
    [SerializeField] UiManager uiManager;

    void Start()
    {
        /// Check for video path exist
        if (!FileManager.CheckDirectory(defVideoPath, true))
            return;

        FileManager.defPath = defVideoPath;

        /// Check for Internet available
        StartCoroutine(InternetConnection.check());

    }


}
