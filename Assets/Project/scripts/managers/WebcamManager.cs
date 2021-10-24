using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamManager : MonoBehaviour
{
    public GameObject webcam;

    private WebCamTexture webcamTexture;

    void Start()
    {
        Application.targetFrameRate = 60;

        webcamTexture = new WebCamTexture();
        // Debug.Log(webcamTexture.deviceName);
        // Debug.Log(webcamTexture.width);
        // Debug.Log(webcamTexture.height);

        Renderer rend = webcam.GetComponent<Renderer>();
        rend.material.mainTexture = webcamTexture;

        webcamTexture.requestedHeight = 1080;
        webcamTexture.requestedWidth = 1920;

        // /// init webcam
        // Play();
        // yield return new WaitForSeconds(0.5f);
        // Pause();

        webcam.SetActive(false);
    }

    public void Play()
    {
        // webcamTexture.Play();
        // Debug.Log(webcamTexture.width + " - " + webcamTexture.height);
        webcam.SetActive(true);
        StartCoroutine(PlayCoroutine());
    }

    IEnumerator PlayCoroutine(){
        /// we have to wait a little becasue when the webcam
        /// start to play, it block the execution of the app...
        yield return new WaitForSeconds(0.25f);
        webcamTexture.Play();
    }

    public void Pause()
    {
        webcamTexture.Pause();
    }

    public void Stop()
    {
        webcamTexture.Stop();
        webcam.SetActive(false);
    }
}
