using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ScreenshotHandler : MonoBehaviour
{   
    private static ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;

    private string filePath;
    private Action<byte[]> returnBytesAfterScreenshot;

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
                // System.DateTime now = System.DateTime.Now;
                // long fileCreationTime = now.ToFileTime();
                // // string screenshotFullName = filePath + "/" + fileCreationTime + ".jpg";
                
                // string path = Path.Combine(Globals.screenshotFolder, Globals.screenshotName + fileCreationTime + "jpg");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.WriteAllBytes(filePath, byteArray);
                Debug.Log("Screenshot saved to: " + filePath);

                RenderTexture.ReleaseTemporary(renderTexture);
                myCamera.targetTexture = null;

                if (returnBytesAfterScreenshot != null)
                {
                    /// callback
                    returnBytesAfterScreenshot(byteArray);
                }
            }
            else
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "C'è stato un problema con lo screenshot");
            }
        }
    }

    public void TakeScreenshot(int width, int height, string path, Action<byte[]> callback = null)
    {
        filePath = path;
        returnBytesAfterScreenshot = callback;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }
}
