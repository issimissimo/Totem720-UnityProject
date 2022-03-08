using UnityEngine;
using UnityEngine.UI;

public class ButtonShowPanel : ButtonBase
{
    [SerializeField] GameObject panelToShow;
    [SerializeField] bool isEnabled = true;
    
    public override void Start(){
        base.Start();

        /// Set button enabled or not
        if (!isEnabled){
            Image img = GetComponent<Image>();
            img.color = new Color(0.5f, 0.5f, 0.5f, 1);
            button.interactable = false;
        }
    }
    
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
            GameManager.instance.ShowPanel(panelToShow);
        }
    }
}
