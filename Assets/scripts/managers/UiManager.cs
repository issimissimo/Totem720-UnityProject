using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    // [SerializeField] GameObject[] panels;
    [SerializeField] Transform Ui_panel_container;
    private List<GameObject> Ui_panels = new List<GameObject>();
    private GameObject main_panel;


    private void Awake()
    {
        foreach (Transform child in Ui_panel_container)
        {
            Ui_panels.Add(child.gameObject);
        }
        main_panel = Ui_panels[Ui_panels.Count - 1];
    }

    private void HideAllUiPanels(Action callback = null)
    {
        foreach (GameObject go in Ui_panels)
        {
            go.SetActive(false);
        }
        if (callback != null) callback();
    }


    public void ShowPanel(GameObject panel)
    {
        HideAllUiPanels(() =>
        {
            panel.SetActive(true);
        });
    }


    public void ShowMainPanel()
    {
        ShowPanel(main_panel);
    }



    // private int panelNumber = 0;

    // [Serializable]
    // public class PanelList
    // {
    //     public GameObject[] panels;
    // }


    // [SerializeField] GameObject mainPanel;


    // [Serializable]
    // public class SuperList
    // {
    //     public PanelList[] panelLists;
    // }

    // [SerializeField] SuperList[] superlist;





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
