using UnityEngine;

public class ButtonStartGame : ButtonBase
{
    [SerializeField] int videoNumber;

    public override void onClick()
    {
        base.onClick();
        GameManager.instance.StartGameSession(videoNumber);
    }
}
