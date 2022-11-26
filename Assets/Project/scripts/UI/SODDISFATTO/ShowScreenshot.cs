using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScreenshot : MonoBehaviour
{
    public Image screenshotImage;

    void OnEnable()
    {
        if (GameManager.screenshotPath == null) return;

        Sprite sprite = IMG2Sprite.instance.LoadNewSprite(GameManager.screenshotPath);
        screenshotImage.sprite = sprite;
    }
}
