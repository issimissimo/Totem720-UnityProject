using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    [HideInInspector] public double videoDuration;

    // public event Action OnTimeReached;
    // public event Action OnEnd;

    public void Play(string fileUrl, bool loop = false, Action OnTimeReached = null, Action OnEnd = null)
    {
        StopAllCoroutines();
        StartCoroutine(_Play(fileUrl, loop, OnTimeReached, OnEnd));
    }

    public void Stop()
    {
        if (videoPlayer && videoPlayer.isPlaying)
            videoPlayer.Stop();
    }

    public void Pause()
    {
        if (videoPlayer)
            videoPlayer.Pause();
    }


    // public void WaitForEnd(Action callback)
    // {
    //     StartCoroutine(_WaitForEnd(callback));
    // }

    private IEnumerator _Play(string fileUrl, bool loop, Action OnTimeReached, Action OnEnd)
    {
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


    // private IEnumerator _WaitForTimeReached()
    // {
    //     while (videoPlayer.frame < GameManager.instance.timeToTakePhoto * videoPlayer.frameRate)
    //     {
    //         yield return null;
    //     }
    //     if (OnTimeReached != null) OnTimeReached();
    // }


    // private IEnumerator _WaitForEnd(Action callback)
    // {
    //     while (videoPlayer.isPlaying)
    //     {
    //         yield return new WaitForEndOfFrame();
    //     }

    //     callback();
    // }
}
