using UnityEngine;

public class ButtonShowPanel : ButtonBase
{
    [SerializeField] GameObject panelToShow;
    public override void onClick()
    {
        base.onClick();

        /// If this button open "squadra" panel...
        PanelType panelType = panelToShow.GetComponent<PanelType>();
        if (panelType != null){
            GameManager.instance.ShowPanelByType(panelType.scenario, panelType.squadra);
        }
        
        /// If this button open "scenario" panel...
        else{
            GameManager.instance.uiManager.ShowPanel(panelToShow);
        }
    }
}
