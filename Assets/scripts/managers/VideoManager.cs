using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    public void Play(string fileUrl)
    {
        videoPlayer.url = fileUrl;
        videoPlayer.Play();
    }
}
