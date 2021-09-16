using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamManager : MonoBehaviour
{
    public GameObject webcam;
    
    private WebCamTexture webcamTexture;

    IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        
        webcamTexture = new WebCamTexture();
        Debug.Log(webcamTexture.deviceName);
        Debug.Log(webcamTexture.width);
        Debug.Log(webcamTexture.height);
        
        Renderer rend = webcam.GetComponent<Renderer>();
        rend.material.mainTexture = webcamTexture;

        webcamTexture.requestedHeight = 1080;
        webcamTexture.requestedWidth = 1920;

        // /// init webcam
        Play();
        yield return new WaitForSeconds(0.5f);
        Pause();
    }

    public void Play(){
        webcamTexture.Play();
        Debug.Log(webcamTexture.width + " - " + webcamTexture.height);
    }

     public void Pause(){
        webcamTexture.Pause();
    }

    public void Stop(){
        webcamTexture.Stop();
    }
}
