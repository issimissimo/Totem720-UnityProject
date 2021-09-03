using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    [HideInInspector] double videoDuration;

    public void Play(string fileUrl, Action callback = null)
    {
        StartCoroutine(_Play(fileUrl, callback));
    }

    public void WaitForEnd(Action callback)
    {
        StartCoroutine(_WaitForEnd(callback));
    }

    private IEnumerator _Play(string fileUrl, Action callback)
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
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
