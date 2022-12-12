using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRemaingShoots : MonoBehaviour
{
    [SerializeField] Text text;

    void OnEnable()
    {
        if (GameManager.instance == null) return;
        text.text = (GameManager.instance.maxPhotoTrials - GameManager.instance.photoShootTrials).ToString();
    }
}
