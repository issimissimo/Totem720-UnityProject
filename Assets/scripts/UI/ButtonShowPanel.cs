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
    }
}
