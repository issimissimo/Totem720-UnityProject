using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    // [SerializeField] GameObject[] panels;

    private int panelNumber = 0;

    [Serializable]
    public class PanelList
    {
        public GameObject[] panels;
    }


    [SerializeField] GameObject mainPanel;


    [Serializable]
    public class SuperList
    {
        public PanelList[] panelLists;
    }

    [SerializeField] SuperList[] superlist;





    // void Start()
    // {
    //     for (int i = 0; i < panels.Length; i++)
    //     {
    //         if (i == 0) panels[i].SetActive(true);
    //         else panels[i].SetActive(false);
    //     }
    // }

    public void ShowPreviousPanel()
    {
        // if (panelNumber > 0)
        // {
        //     panels[panelNumber].SetActive(false);
        //     panelNumber--;
        //     panels[panelNumber].SetActive(true);
        // }
    }
}
