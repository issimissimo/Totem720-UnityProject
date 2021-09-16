using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndGameCtrl : MonoBehaviour
{
    public GameObject panelEndgame;
    void Start()
    {
        panelEndgame.SetActive(false);
    }

    public void Show(Action callback)
    {
        StartCoroutine(_Show(callback));
    }


    private IEnumerator _Show(Action callback)
    {
        panelEndgame.SetActive(true);

        yield return new WaitForSeconds(3);

        panelEndgame.SetActive(false);

        callback();
    }
}
