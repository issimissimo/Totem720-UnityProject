using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject ui_panels_container;

    public GameObject foreground;
    public GameObject backgroundVideo;
    public GameObject instructions;
    public GameObject photo;
    public GameObject satisfied;
    public GameObject timeExpired;
    public GameObject payment;
    public GameObject askForEmail;
    public GameObject email;
    public GameObject endGame;



    private List<GameObject> ui_panels = new List<GameObject>();
    private GameObject main_panel;
    private Renderer backgroundMaterial;


    private void Awake()
    {
        foreach (Transform child in ui_panels_container.transform)
        {
            ui_panels.Add(child.gameObject);
        }
        main_panel = ui_panels[ui_panels.Count - 1];

        /// hide panels at start
        HideUiContainer();

        foreground.SetActive(false);
    }


    public void HideAllUiPanels(Action callback = null)
    {
        foreach (GameObject go in ui_panels)
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

            /// Show or hide the UI Video
            backgroundVideo.SetActive(panel.GetComponent<PlayVideoOnPanelEnabled>() != null ? true : false);

            /// Set the foreground
            if (panel.GetComponent<ForegroundSelection>() != null)
            {
                if (!foreground.activeSelf)
                    foreground.SetActive(true);

                Sprite foregroundSprite = null;
                if (Globals._SQUADRA == Globals.Squadra.Milan) foregroundSprite = panel.GetComponent<ForegroundSelection>().foregroundMilan;
                if (Globals._SQUADRA == Globals.Squadra.Inter) foregroundSprite = panel.GetComponent<ForegroundSelection>().foregroundInter;
                if (Globals._SQUADRA == Globals.Squadra.Inter_Milan) foregroundSprite = panel.GetComponent<ForegroundSelection>().foregroundInterMilan;
                if (foregroundSprite == null) Debug.LogError("Foreground not specified!");
                foreground.GetComponent<Image>().sprite = foregroundSprite;
            }
        });
    }


    public void ShowPanelByType(Globals.Scenario scenario, Globals.Squadra squadra)
    {
        ShowUiContainer();

        foreach (GameObject panel in ui_panels)
        {
            PanelType panelType = panel.GetComponent<PanelType>();
            if (panelType != null)
            {
                if (panelType.scenario == scenario && panelType.squadra == squadra)
                {
                    ShowPanel(panel);
                }
            }
        }
    }


    /// Show the very 1st panel (scelta SCENARIO)
    public void ShowInitPanel()
    {
        ShowUiContainer();
        ShowPanel(main_panel);
    }


    /// Hide the container for all the UI panels
    public void HideUiContainer()
    {
        ui_panels_container.SetActive(false);
    }

    /// Show the container for all the UI panels
    public void ShowUiContainer()
    {
        ui_panels_container.SetActive(true);
    }


    public void ShowGame(double videoDuration)
    {
        // HideUiContainer();
        HideAllUiPanels();
    }

}
