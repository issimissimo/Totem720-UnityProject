using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UiManager : MonoBehaviour
{
    public GraphicRaycaster ui_raycaster; /// Don't delete! It's used by buttons!!!
    [SerializeField] GameObject ui_panels_container;
    [SerializeField] CountdownCtrl countdownCtrl;
    [SerializeField] EmailCtrl emailCtrl;
    [SerializeField] EndGameCtrl endGameCtrl;
    private List<GameObject> ui_panels = new List<GameObject>();
    private GameObject main_panel;



    private void Awake()
    {
        foreach (Transform child in ui_panels_container.transform)
        {
            ui_panels.Add(child.gameObject);
        }
        main_panel = ui_panels[ui_panels.Count - 1];

        /// hide panels at start
        HideUiContainer();


    }

    private void HideAllUiPanels(Action callback = null)
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
        HideUiContainer();

        countdownCtrl.Play(videoDuration);
    }


    public void ShowEmail(string screenshot, Action callback)
    {

        Action _callback = callback;

        emailCtrl.Show(screenshot, () =>
        {
            // emailCtrl.Hide();

            endGameCtrl.Show(() =>
            {
                callback();
            });

        });

    }
}
