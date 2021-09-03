using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.speed = 0;
        animator.playbackTime = 0;
    }

    public void Play(double videoDuration)
    {
        StartCoroutine(_Play(videoDuration));
    }

    private IEnumerator _Play(double videoDuration)
    {
        /// wait for 3 sec from the end of the video
        yield return new WaitForSeconds((float)videoDuration - 3);

        animator.playbackTime = 0;
        animator.speed = 1;
    }
}
