using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    [HideInInspector] public double videoDuration;

    public void Play(string fileUrl, bool loop = false, Action callback = null)
    {
        StartCoroutine(_Play(fileUrl, loop, callback));
    }

    public void Stop()
    {
        if (videoPlayer)
            videoPlayer.Stop();
    }

    public void WaitForEnd(Action callback)
    {
        StartCoroutine(_WaitForEnd(callback));
    }

    private IEnumerator _Play(string fileUrl, bool loop, Action callback)
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
            print("AAAAAAAAAAAAAAAAAAAAA!!!!!!");
            ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, e.ToString());
        }

        videoPlayer.url = fileUrl;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForEndOfFrame();
        }

        videoDuration = videoPlayer.length;

        videoPlayer.frame = 0;
        videoPlayer.Play();

        // yield return new WaitForSeconds(0.1f);

        if (callback != null) callback();
    }


    private IEnumerator _WaitForEnd(Action callback)
    {
        while (videoPlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        callback();
    }
}
