using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WebcamManager : MonoBehaviour
{
    public GameObject webcam;

    private WebCamTexture webcamTexture;
    private Material webcamMat;


    void Awake()
    {
        webcamMat = webcam.GetComponent<Renderer>().material;
    }

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

        webcam.SetActive(false);
    }

    public void Play(Action OnWebcamready = null)
    {
        webcam.SetActive(true);
        StartCoroutine(PlayCoroutine(OnWebcamready));
    }

    IEnumerator PlayCoroutine(Action OnWebcamready)
    {
        webcamMat.SetFloat("_Opacity", 0f);

        /// we have to wait a little becasue when the webcam
        /// start to play, it block the execution of the app...
        yield return new WaitForSeconds(0.25f);
        webcamTexture.Play();

        while (!webcamTexture.isPlaying)
            yield return null;
        

        float timeToFade = 5f;
        float t = 0;
        while (t < timeToFade)
        {
            t += Time.deltaTime;
            float opacity = Mathf.Lerp(0, 1, t / timeToFade);
            webcamMat.SetFloat("_Opacity", opacity);
            yield return null;
        }

        if (OnWebcamready!= null) OnWebcamready();
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
