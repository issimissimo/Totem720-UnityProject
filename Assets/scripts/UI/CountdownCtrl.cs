using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownCtrl : MonoBehaviour
{
    [SerializeField] GameObject countdown;
    [SerializeField] AnimationClip animationClip;
    
    void Start()
    {
        countdown.SetActive(false);
    }

    public void Play(double videoDuration)
    {
        StartCoroutine(_Play(videoDuration));
    }

    private IEnumerator _Play(double videoDuration)
    {
        /// wait for 3 sec from the end of the video
        yield return new WaitForSeconds((float)videoDuration - 3);

        countdown.SetActive(true);

        yield return new WaitForSeconds(animationClip.length);

        countdown.SetActive(false);
    }
}
