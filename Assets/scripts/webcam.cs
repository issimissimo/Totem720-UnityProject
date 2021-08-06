using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webcam : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    void Start()
    {
        Application.targetFrameRate = 60;
        
        webcamTexture = new WebCamTexture();
        Debug.Log(webcamTexture.deviceName);
        Debug.Log(webcamTexture.width);
        Debug.Log(webcamTexture.height);
        
        Renderer rend = GetComponent<Renderer>();
        rend.material.mainTexture = webcamTexture;

        // webcamTexture.Play();
        webcamTexture.requestedHeight = 1080;
        webcamTexture.requestedWidth = 1920;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        webcamTexture.Play();
        Debug.Log(webcamTexture.width + " - " + webcamTexture.height);
    }

    public void Stop(){
        webcamTexture.Stop();
    }
}
