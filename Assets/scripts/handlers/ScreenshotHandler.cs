using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;

    private string filePath;
    private Action<string> callbackAfterScreenshot;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToJPG(100);

            if (byteArray.Length > 0)
            {
                System.DateTime now = System.DateTime.Now;
                long fileCreationFileTime = now.ToFileTime();
                string screenshotFullName = filePath + "/" + fileCreationFileTime + ".jpg";

                System.IO.File.WriteAllBytes(screenshotFullName, byteArray);
                Debug.Log("Saved CameraScreenshot.png");

                RenderTexture.ReleaseTemporary(renderTexture);
                myCamera.targetTexture = null;

                // System.IO.File.WriteAllBytes(filePath + "/CameraScreenshot.jpg", byteArray);
                // Debug.Log("Saved CameraScreenshot.png");

                // RenderTexture.ReleaseTemporary(renderTexture);
                // myCamera.targetTexture = null;

                if (callbackAfterScreenshot != null)
                {
                    callbackAfterScreenshot(screenshotFullName);
                }
            }
            else
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "C'è stato un problema con lo screenshot");
            }
        }
    }

    public void TakeScreenshot(int width, int height, string path, Action<string> callback = null)
    {
        filePath = path;
        callbackAfterScreenshot = callback;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }
}
