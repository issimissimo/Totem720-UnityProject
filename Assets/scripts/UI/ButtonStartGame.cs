using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonStartGame : ButtonBase
{
    // public Globals.Scenario scenario;
    // public Globals.Squadra squadra;
    [SerializeField] PanelType panelType;
    [SerializeField] int videoNumber;

    public override void onClick()
    {
        base.onClick();
        // GameManager.instance.uiManager.ShowPanel(panelToShow);
        Debug.Log("LANCIO: " + panelType.scenario.ToString() + " - " + panelType.squadra.ToString() + " - " + videoNumber);
    }
}
