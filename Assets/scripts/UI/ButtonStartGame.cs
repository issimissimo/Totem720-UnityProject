using UnityEngine;

public class ButtonStartGame : ButtonBase
{
    [SerializeField] PanelType panelType;
    [SerializeField] int videoNumber;

    public override void onClick()
    {
        base.onClick();
        // Debug.Log("LANCIO: " + panelType.scenario.ToString() + " - " + panelType.squadra.ToString() + " - " + videoNumber);
        GameManager.instance.StartGameSession(panelType.scenario, panelType.squadra, videoNumber);
    }
}
