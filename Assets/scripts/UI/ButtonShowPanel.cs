using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShowPanel : ButtonBase
{
    [SerializeField] GameObject panelToShow;
    public override void onClick()
    {
        base.onClick();
        GameManager.instance.uiManager.ShowPanel(panelToShow);

        PanelType panelType = panelToShow.GetComponent<PanelType>();
        if (panelType != null){
            Globals._SCENARIO = panelType.scenario;
            Globals._SQUADRA = panelType.squadra;
        }
    }
}
