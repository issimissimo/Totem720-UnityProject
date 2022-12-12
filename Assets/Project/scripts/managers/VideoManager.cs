using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    [HideInInspector] public double videoDuration;


    private Material videoMat;

    void Awake()
    {
        videoMat = videoPlayer.GetComponent<Renderer>().material;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Play(string fileUrl, bool loop = false, Action OnTimeReached = null, Action OnEnd = null)
    {
        StopAllCoroutines();
        StartCoroutine(_Play(fileUrl, loop, OnTimeReached, OnEnd));
    }

    public void Stop()
    {
        if (videoPlayer && videoPlayer.isPlaying){
            videoPlayer.Stop();
            videoMat.SetFloat("_Opacity", 0f);
        }
    }

    public void Pause()
    {
        if (videoPlayer)
            videoPlayer.Pause();
    }


    private IEnumerator _Play(string fileUrl, bool loop, Action OnTimeReached, Action OnEnd)
    {
        videoMat.SetFloat("_Opacity", 0f);
        
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        videoPlayer.isLooping = loop;

        try
        {
            videoPlayer.url = fileUrl;
        }
        catch (Exception e)
        {
            ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, e.ToString());
        }

        videoPlayer.url = fileUrl;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoDuration = videoPlayer.length;

        videoPlayer.frame = 0;
        videoPlayer.Play();

        while (videoPlayer.frame <= 1)
            yield return null;
        
        videoMat.SetFloat("_Opacity", 1f);

        
        while (videoPlayer.frame < GameManager.instance.timeToTakePhoto * videoPlayer.frameRate)
        {
            yield return null;
        }
        if (OnTimeReached != null) OnTimeReached();


        while (videoPlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        if (OnEnd != null) OnEnd();
    }
}
