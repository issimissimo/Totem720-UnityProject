using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ButtonShowPanel : ButtonBase
{
    [SerializeField] GameObject panelToShow;
    [SerializeField] bool isEnabled = true;

    private CanvasGroup cv;
    

    public override void Awake() {
        base.Awake();
        cv = GetComponent<CanvasGroup>();
    }

    public override void Start(){
        base.Start();

        /// Set button enabled or not
        if (!isEnabled){
            // Image img = GetComponent<Image>();
            // img.color = new Color(0.5f, 0.5f, 0.5f, 1);
            // button.interactable = false;

            cv.alpha = 0.3f;
            cv.interactable = false;
        }
        else{
            cv.alpha =1;
            cv.interactable = true;
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
